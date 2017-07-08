using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Entropy
{
    public partial class Histogram : UserControl
    {
        private List<int> bins = new List<int>();
        private bool display;
        private bool horizontal = true;
        private int max = 1;

        public List<int> Bins { set { bins = value; } }
        public bool Display { set { display = value; if (!display) this.Invalidate(); } }
        public bool Horizontal { get { return horizontal; } set { horizontal = value; } }
        public int Max { set { max = value; } }

        public Histogram()
        {
            InitializeComponent();

            this.Paint += new PaintEventHandler(this.PaintEventHandler);
        }

        public void Draw()
        {
            this.Invalidate();
        }

        private void PaintEventHandler(object sender, PaintEventArgs e)
        {
            if ((bins.Count == 0) || !display) return;

            Graphics g = e.Graphics;

            if (horizontal)
            {
                float width = (float)this.DisplayRectangle.Width;
                float height = (float)this.DisplayRectangle.Height;
                float binWidth = width / bins.Count;
                float binHeight;
                for (int i = 0; i < bins.Count; i++)
                {
                    binHeight = ((float)bins[i] / (float)max) * height;
                    g.FillRectangle(Brushes.Black, i * binWidth, 0f, binWidth, binHeight);
                }
            }
            else
            {
                float width = (float)this.DisplayRectangle.Width;
                float height = (float)this.DisplayRectangle.Height;
                float binHeight = height / bins.Count;
                float binWidth;
                for (int i = 0; i < bins.Count; i++)
                {
                    binWidth = ((float)bins[i] / (float)max) * width;
                    g.FillRectangle(Brushes.Black, width - binWidth, i * binHeight, binWidth, binHeight);
                }
            }
        }
    }
}