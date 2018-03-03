#region Todo
/*
 * Add user manual
 * Add "start in corner"?
 * Add saved state editor?
*/
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Entropy
{
    public partial class Form1 : Form
    {
        #region Fields
        private List<Vector> positions = new List<Vector>();
        private List<Vector> velocities = new List<Vector>();
        private List<Color> colors = new List<Color>();
        private List<Brush> brushes = new List<Brush>();
        private int nrParticles;
        private double radius = 0.5;
        private double diameter;
        private double diameterSquared;
        private double minX, maxX;
        private double minY, maxY;
        private double twiceMinX, twiceMaxX;
        private double twiceMinY, twiceMaxY;
        private int maxInitVx;
        private int maxInitVy;
        private Thread runThread;
        private List<int>[,] grid;
        private int gridX;
        private int gridY;
        private Random rnd;
        private int seed = 0;
        private List<int> histX = new List<int>();
        private List<int> histY = new List<int>();
        private int iterations = 0;
        private double minAvgDistance = double.MaxValue;
        private bool running = false;
        private bool done = false;
        private bool continuation = false;
        private bool change = false;
        private const string stateFileExtension = ".es";
        private const string minFileExtension = ".em";
        private const string fileBaseName = "entropy";
        private readonly string sMinFilePath = null;
        private const string dateTimeFormat = "yyyy'-'MM'-'dd, hh'h' mm'm' ss's' tt";
        private Properties.Settings settings = Properties.Settings.Default;
        private bool respondToChange = false;
        private string dataFolder = null;
        #endregion
        #region Constructor
        public Form1()
        {
            InitializeComponent();

            var assembly = Assembly.GetExecutingAssembly();
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            var companyName = versionInfo.CompanyName;
            var productName = versionInfo.ProductName;
            dataFolder = new CommonApplicationData(companyName, productName, true).ToString();

            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            btnLoad.Enabled = Directory.GetFiles(dataFolder).Length > 0;

            sMinFilePath= Path.Combine(dataFolder, fileBaseName + minFileExtension);

            for (int i = 0; i < 100; i++)
                cmbSeed.Items.Add(i.ToString());

            cmbGrid.SelectedItem = settings.Grid.ToString();
            cmbParticles.SelectedItem = settings.NrParticles.ToString();
            cmbMaxInitVx.SelectedItem = settings.MaxInitVx.ToString();
            cmbMaxInitVy.SelectedItem = settings.MaxInitVy.ToString();
            cmbSeed.SelectedItem = settings.Seed.ToString();

            gridX = gridY = settings.Grid;
            nrParticles = settings.NrParticles;
            maxInitVx = settings.MaxInitVx;
            maxInitVy = settings.MaxInitVy;
            seed = settings.Seed;

            diameter = 2 * radius;
            diameterSquared = diameter * diameter;

            minX = radius;
            minY = radius;
            maxX = gridX - radius;
            maxY = gridY - radius;

            twiceMinX = 2.0 * minX;
            twiceMaxX = 2.0 * maxX;
            twiceMinY = 2.0 * minY;
            twiceMaxY = 2.0 * maxY;

            canvas.Radius = radius;
            canvas.Positions = positions;
            canvas.Brushes = brushes;
            canvas.ScaleFactor = (double)canvas.DisplayRectangle.Width / (double)gridX;

            histogramX.Bins = histX;
            histogramX.Max = gridY / 2;
            histogramY.Bins = histY;
            histogramY.Max = gridX / 2;

            respondToChange = true;
        }
        #endregion
        #region Methods
        private void CheckWallBounce(int k)
        {
            double x = positions[k].x;
            if (x <= minX)
            {
                positions[k].x = twiceMinX - x;
                velocities[k].x = -velocities[k].x;
            }
            else if (x >= maxX)
            {
                positions[k].x = twiceMaxX - x;
                velocities[k].x = -velocities[k].x;
            }

            double y = positions[k].y;
            if (y <= minY)
            {
                positions[k].y = twiceMinY - y;
                velocities[k].y = -velocities[k].y;
            }
            else if (y >= maxY)
            {
                positions[k].y = twiceMaxY - y;
                velocities[k].y = -velocities[k].y;
            }
        }

        // Cf. http://www.phy.ntnu.edu.tw/ntnujava/index.php?topic=4.0
        private void Collide(int k, int n, double dx, double dy, double dSquared)
        {
            // Calculate distance between ball centers
            double d = Math.Sqrt(dSquared); // d <= r1 + r2

            // Calculate component of velocity in the direction of (dx, dy)
            double vp1 = (velocities[k].x * dx + velocities[k].y * dy) / d;
            double vp2 = (velocities[n].x * dx + velocities[n].y * dy) / d;

            // Collision should have occurred at t - dt, when d = r1 + r2 (= diameter when r1 = r2)
            double dt = (diameter - d) / Math.Abs(vp1 - vp2);

            // Move balls backward
            positions[k].x -= velocities[k].x * dt;
            positions[k].y -= velocities[k].y * dt;
            positions[n].x -= velocities[n].x * dt;
            positions[n].y -= velocities[n].y * dt;

            // Unit vector in the direction of the collision
            double ax = dx / d;
            double ay = dy / d;

            // Project velocities to these axes
            double va1 = velocities[k].x * ax + velocities[k].y * ay;
            double vb1 = -velocities[k].x * ay + velocities[k].y * ax;
            double va2 = velocities[n].x * ax + velocities[n].y * ay;
            double vb2 = -velocities[n].x * ay + velocities[n].y * ax;

            // New velocities in these axes (after collision)
            double vaP1 = va2;
            double vaP2 = va1;

            // Undo projection
            velocities[k].x = vaP1 * ax - vb1 * ay;
            velocities[k].y = vaP1 * ay + vb1 * ax;
            velocities[n].x = vaP2 * ax - vb2 * ay;
            velocities[n].y = vaP2 * ay + vb2 * ax;

            // Move balls forward
            positions[k].x += velocities[k].x * dt;
            positions[k].y += velocities[k].y * dt;
            positions[n].x += velocities[n].x * dt;
            positions[n].y += velocities[n].y * dt;
        }

        private void CreateHistograms()
        {
            int count;
            histX.Clear();
            for (int i = 0; i < gridX; i++)
            {
                count = 0;
                for (int j = 0; j < gridY; j++)
                    count += grid[i, j].Count;
                histX.Add(count);
            }
            histY.Clear();
            for (int j = 0; j < gridY; j++)
            {
                count = 0;
                for (int i = 0; i < gridX; i++)
                    count += grid[i, j].Count;
                histY.Add(count);
            }
        }

        private void Initialize()
        {
            iterations = 0;
            minAvgDistance = double.MaxValue;

            UpdateLabels();

            grid = new List<int>[gridX, gridY];
            for (int i = 0; i < gridX; i++)
                for (int j = 0; j < gridY; j++)
                    grid[i, j] = new List<int>();

            rnd = new Random(seed);

            positions.Clear();
            velocities.Clear();
            colors.Clear();
            for (int k = 0; k < nrParticles; k++)
            {
                positions.Add(new Vector(rnd.Next((int)(minX + radius), (int)(maxX - radius)), rnd.Next((int)(minY + radius), (int)(maxY + radius))));
                velocities.Add(new Vector(rnd.Next(-maxInitVx, maxInitVx) / 1000.0, rnd.Next(-maxInitVy, maxInitVy) / 1000.0));
                grid[(int)positions[k].x, (int)positions[k].y].Add(k);
                colors.Add(Color.FromArgb(rnd.Next(0,255), rnd.Next(0,255), rnd.Next(0,255)));
                brushes.Add(new SolidBrush(colors[k]));
            }
        }

        private void Reset()
        {
            btnReset.Enabled = false;
            btnLoadMin.Enabled = false;

            canvas.Display = false;
            histogramX.Display = false;
            histogramY.Display = false;
            btnSave.Enabled = false;

            maxX = gridX - radius;
            maxY = gridY - radius;

            twiceMaxX = 2.0 * maxX;
            twiceMaxY = 2.0 * maxY;

            canvas.ScaleFactor = (double)canvas.DisplayRectangle.Width / (double)gridX;

            histogramX.Max = gridY / 2;
            histogramY.Max = gridX / 2;

            iterations = 0;
            minAvgDistance = 0.0;
            UpdateLabels();
            
            continuation = false;
        }

        private void Run()
        {
            int I, J;
            int imin, imax;
            int jmin, jmax;
            double dx, dy, dSquared;

            var start = DateTime.Now;
            running = true;
            done = false;
            while (running)
            {
                // Clear grid
                for (int i = 0; i < gridX; i++)
                    for (int j = 0; j < gridY; j++)
                        grid[i, j] = new List<int>();

                // Apply linear motion
                for (int k = 0; k < positions.Count; k++)
                {
                    positions[k].x += velocities[k].x;
                    positions[k].y += velocities[k].y;
                    CheckWallBounce(k);
                    grid[(int)positions[k].x, (int)positions[k].y].Add(k);
                }

                // Handle particle collisions
                for (int k = 0; k < positions.Count; k++)
                {
                    I = (int)positions[k].x;
                    J = (int)positions[k].y;
                    if (grid[I, J].Count > 1)
                    {
                        foreach (int n in grid[I, J])
                            if (n != k)
                            {
                                dx = positions[k].x - positions[n].x;
                                dy = positions[k].y - positions[n].y;
                                dSquared = dx * dx + dy * dy;
                                if (dSquared <= diameterSquared)
                                {
                                    Collide(k, n, dx, dy, dSquared);
                                    CheckWallBounce(k);
                                    CheckWallBounce(n);
                                    grid[I, J].Remove(k);
                                    grid[I, J].Remove(n);
                                    grid[(int)positions[k].x, (int)positions[k].y].Add(k);
                                    grid[(int)positions[n].x, (int)positions[n].y].Add(n);
                                    break; // Just handle single collision
                                }
                            }
                    }
                    else
                    {
                        imin = I - 1; if (imin < 0) imin = 0;
                        imax = I + 1; if (imax == gridX) imax = gridX - 1;
                        jmin = J - 1; if (jmin < 0) jmin = 0;
                        jmax = J + 1; if (jmax == gridY) jmax = gridY - 1;

                        for (int i = imin; i <= imax; i++)
                            for (int j = jmin; j <= jmax; j++)
                                if ((i != I) || (j != J))
                                    foreach (int n in grid[i, j])
                                    {
                                        dx = positions[k].x - positions[n].x;
                                        dy = positions[k].y - positions[n].y;
                                        dSquared = dx * dx + dy * dy;
                                        if (dSquared <= diameterSquared)
                                        {
                                            Collide(k, n, dx, dy, dSquared);
                                            CheckWallBounce(k);
                                            CheckWallBounce(n);
                                            grid[I, J].Remove(k);
                                            grid[i, j].Remove(n);
                                            grid[(int)positions[k].x, (int)positions[k].y].Add(k);
                                            grid[(int)positions[n].x, (int)positions[n].y].Add(n);
                                            i = imax; j = jmax;
                                            break; // Just handle single collision
                                        }
                                    }
                    }
                }

                // Create histograms
                CreateHistograms();

                // Calculate average distance
                var sum = 0.0;
                for (int i = 0; i < nrParticles; i++)
                    for (var j = i + 1; j < nrParticles; j++)
                    {
                        var p1 = positions[i];
                        var p2 = positions[j];
                        sum += Math.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y));
                    }
                var avgDistance = 2 * sum / (nrParticles * (nrParticles - 1));

                // Update minimum
                if (avgDistance < minAvgDistance)
                {
                    minAvgDistance = avgDistance;
                    Save(sMinFilePath);
                }

                iterations++;
                if ((DateTime.Now - start).TotalSeconds >= 1.0)
                {
                    UpdateLabels();
                    start = DateTime.Now;
                }
                if (chkDisplayMotion.Checked)
                {
                    canvas.Draw();
                    histogramX.Draw();
                    histogramY.Draw();
                    Thread.Sleep(33);
                }
            }
            done = true;
            change = true;
        }

        private void Save(string fName)
        {
            using (StreamWriter w = new StreamWriter(fName, false))
            {
                w.WriteLine("Particles: " + nrParticles.ToString());
                w.WriteLine("Grid: " + gridX.ToString() + "x" + gridY.ToString());
                w.WriteLine("Max. init. velocity: " + maxInitVx.ToString() + "," + maxInitVx.ToString());
                w.WriteLine("Seed: " + seed.ToString());
                w.WriteLine("Iteration: " + iterations.ToString());
                w.WriteLine("Min. avg distance: " + minAvgDistance.ToString());
                w.WriteLine();
                w.WriteLine("Positions");
                for (int i = 0; i < positions.Count; i++)
                    w.WriteLine(positions[i].x.ToString() + "," + positions[i].y.ToString());
                w.WriteLine();
                w.WriteLine("Velocities");
                for (int i = 0; i < velocities.Count; i++)
                    w.WriteLine(velocities[i].x.ToString() + "," + velocities[i].y.ToString());
                w.WriteLine();
                w.WriteLine("Colors");
                for (int i = 0; i < colors.Count; i++)
                    w.WriteLine(colors[i].R.ToString() + "," + colors[i].G.ToString() + "," + colors[i].B.ToString());
            }
        }

        private void UpdateLabels()
        {
            this.Invoke((Action)(() =>
            {
                lblIterations.Text = iterations.ToString("#,0");
                lblEntropy.Text = (iterations > 0) ? Math.Log(minAvgDistance).ToString("0.000") : "---";
            }));
        }
        #endregion
        #region Event handlers
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if ((runThread != null) && (runThread.IsAlive))
                runThread.Abort();
            base.OnClosing(e);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var filePath = "";
            if (sender == btnLoadMin)
                filePath = sMinFilePath;
            else
            {
                var fileSelector = new OpenFileDialog();
                fileSelector.Filter = "Entropy state files (*.es) | *.es";
                fileSelector.Multiselect = false;
                fileSelector.InitialDirectory = dataFolder;
                var result = fileSelector.ShowDialog();
                if ((result != DialogResult.OK) || string.IsNullOrEmpty(fileSelector.FileName))
                    return;
                filePath = fileSelector.FileName;
            }

            var sep0 = new string[] { ": " };
            var sep1 = new char[] { 'x' };
            var sep2 = new char[] { ',' };
            using (StreamReader r = new StreamReader(filePath))
            {
                nrParticles = int.Parse(r.ReadLine().Split(sep0, StringSplitOptions.None)[1]);

                var s = r.ReadLine().Split(sep0, StringSplitOptions.None)[1];
                gridX = int.Parse(s.Split(sep1)[0]);
                gridY = int.Parse(s.Split(sep1)[1]);

                s = r.ReadLine().Split(sep0, StringSplitOptions.None)[1];
                maxInitVx = int.Parse(s.Split(sep2)[0]);
                maxInitVy = int.Parse(s.Split(sep2)[1]);

                r.ReadLine();
                iterations = int.Parse(r.ReadLine().Split(sep0, StringSplitOptions.None)[1]);
                minAvgDistance = double.Parse(r.ReadLine().Split(sep0, StringSplitOptions.None)[1]);

                UpdateLabels();
                grid = new List<int>[gridX, gridY];
                for (int i = 0; i < gridX; i++)
                    for (int j = 0; j < gridY; j++)
                        grid[i, j] = new List<int>();

                rnd = new Random(seed);

                settings.Grid = gridX;
                settings.NrParticles = nrParticles;
                settings.MaxInitVx = maxInitVx;
                settings.MaxInitVy = maxInitVy;
                settings.Seed = seed;
                settings.Save();

                respondToChange = false;
                cmbGrid.SelectedItem = gridX.ToString();
                cmbParticles.SelectedItem = nrParticles.ToString();
                cmbMaxInitVx.SelectedItem = maxInitVx.ToString();
                cmbMaxInitVy.SelectedItem = maxInitVy.ToString();
                cmbSeed.SelectedItem = seed.ToString();
                respondToChange = true;

                positions.Clear();
                r.ReadLine();
                r.ReadLine();
                for (int k = 0; k < nrParticles; k++)
                {
                    s = r.ReadLine();
                    positions.Add(new Vector(double.Parse(s.Split(sep2)[0]), double.Parse(s.Split(sep2)[1])));
                    grid[(int)positions[k].x, (int)positions[k].y].Add(k);
                }

                velocities.Clear();
                r.ReadLine();
                r.ReadLine();
                for (int k = 0; k < nrParticles; k++)
                {
                    s = r.ReadLine();
                    velocities.Add(new Vector(double.Parse(s.Split(sep2)[0]), double.Parse(s.Split(sep2)[1])));
                }

                colors.Clear();
                brushes.Clear();
                r.ReadLine();
                r.ReadLine();
                for (int k = 0; k < nrParticles; k++)
                {
                    s = r.ReadLine();
                    colors.Add(Color.FromArgb(int.Parse(s.Split(sep2)[0]), int.Parse(s.Split(sep2)[1]), int.Parse(s.Split(sep2)[2])));
                    brushes.Add(new SolidBrush(colors[k]));
                }
            }

            maxX = gridX - radius;
            maxY = gridY - radius;

            twiceMaxX = 2.0 * maxX;
            twiceMaxY = 2.0 * maxY;

            canvas.ScaleFactor = (double)canvas.DisplayRectangle.Width / (double)gridX;
            canvas.Display = true;
            canvas.Draw();

            CreateHistograms();
            histogramX.Bins = histX;
            histogramX.Max = gridY / 2;
            histogramY.Bins = histY;
            histogramY.Max = gridX / 2;
            histogramX.Display = true;
            histogramY.Display = true;
            histogramX.Draw();
            histogramY.Draw();

            chkDisplayMotion.Enabled = true;
            cmbSeed.Enabled = true;
            btnStart.Enabled = true;

            continuation = true;
            change = true;

            if (sender == btnLoad)
            {
                if (File.Exists(sMinFilePath))
                    File.Delete(sMinFilePath);
                filePath = filePath.Replace(stateFileExtension, minFileExtension);
                if (File.Exists(filePath))
                    File.Copy(filePath, sMinFilePath);
                btnLoadMin.Enabled = File.Exists(sMinFilePath);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var timeStr = DateTime.Now.ToString(dateTimeFormat).ToLower();
            var filePath = Path.Combine(dataFolder, fileBaseName + " " + timeStr + stateFileExtension);
            Save(filePath);
            if (File.Exists(sMinFilePath))
                File.Copy(sMinFilePath, filePath.Replace(stateFileExtension, minFileExtension));
            change = false;
            btnSave.Enabled = false;
            btnLoad.Enabled = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                if (!continuation)
                    Initialize();
                btnStart.Text = "Stop";
                btnLoadMin.Enabled = false;
                btnSave.Enabled = false;
                btnLoad.Enabled = false;
                btnReset.Enabled = false;
                chkDisplayMotion.Enabled = false;
                cmbGrid.Enabled = false;
                cmbMaxInitVx.Enabled = false;
                cmbMaxInitVy.Enabled = false;
                cmbParticles.Enabled = false;
                cmbSeed.Enabled = false;

                canvas.Display = chkDisplayMotion.Checked;
                histogramX.Display = chkDisplayMotion.Checked;
                histogramY.Display = chkDisplayMotion.Checked;

                if (File.Exists(sMinFilePath))
                    File.Delete(sMinFilePath);

                runThread = new Thread(new ThreadStart(Run));
                runThread.Start();
            }
            else
            {
                running = false;
                while (!done)
                    Thread.Sleep(33);
                runThread.Abort();
                btnStart.Text = "Start";
                btnLoadMin.Enabled = true;
                btnSave.Enabled = true;
                btnLoad.Enabled = File.Exists("state.txt");
                btnReset.Enabled = true;
                chkDisplayMotion.Enabled = true;
                cmbGrid.Enabled = true;
                cmbMaxInitVx.Enabled = true;
                cmbMaxInitVy.Enabled = true;
                cmbParticles.Enabled = true;
                cmbSeed.Enabled = true;
                continuation = true;
            }
        }

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!respondToChange) return;

            gridX = gridY = settings.Grid = int.Parse(cmbGrid.SelectedItem as string);
            nrParticles = settings.NrParticles = int.Parse(cmbParticles.SelectedItem as string);
            maxInitVx = settings.MaxInitVx = int.Parse(cmbMaxInitVx.SelectedItem as string);
            maxInitVy = settings.MaxInitVy = int.Parse(cmbMaxInitVy.SelectedItem as string);
            seed = settings.Seed = int.Parse(cmbSeed.SelectedItem as string);
            settings.Save();

            if (sender != cmbSeed)
                Reset();
        }
    }
    #endregion
}