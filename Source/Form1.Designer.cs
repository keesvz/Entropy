namespace Entropy
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblIterations = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoadMin = new System.Windows.Forms.Button();
            this.chkDisplayMotion = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.histogramY = new Entropy.Histogram();
            this.histogramX = new Entropy.Histogram();
            this.canvas = new Entropy.DrawingCanvas();
            this.cmbSeed = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbGrid = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbParticles = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbMaxInitVx = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbMaxInitVy = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblEntropy = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblIterations
            // 
            this.lblIterations.AutoSize = true;
            this.lblIterations.Location = new System.Drawing.Point(598, 305);
            this.lblIterations.Name = "lblIterations";
            this.lblIterations.Size = new System.Drawing.Size(16, 13);
            this.lblIterations.TabIndex = 2;
            this.lblIterations.Text = "---";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(520, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Random seed:";
            // 
            // btnLoadMin
            // 
            this.btnLoadMin.Location = new System.Drawing.Point(670, 323);
            this.btnLoadMin.Name = "btnLoadMin";
            this.btnLoadMin.Size = new System.Drawing.Size(57, 24);
            this.btnLoadMin.TabIndex = 3;
            this.btnLoadMin.Text = "Load";
            this.btnLoadMin.UseVisualStyleBackColor = true;
            this.btnLoadMin.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // chkDisplayMotion
            // 
            this.chkDisplayMotion.AutoSize = true;
            this.chkDisplayMotion.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDisplayMotion.Checked = true;
            this.chkDisplayMotion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayMotion.Location = new System.Drawing.Point(520, 217);
            this.chkDisplayMotion.Name = "chkDisplayMotion";
            this.chkDisplayMotion.Size = new System.Drawing.Size(94, 17);
            this.chkDisplayMotion.TabIndex = 4;
            this.chkDisplayMotion.Text = "Display motion";
            this.chkDisplayMotion.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(601, 246);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(57, 24);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // histogramY
            // 
            this.histogramY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.histogramY.Horizontal = false;
            this.histogramY.Location = new System.Drawing.Point(26, 43);
            this.histogramY.Name = "histogramY";
            this.histogramY.Size = new System.Drawing.Size(75, 400);
            this.histogramY.TabIndex = 1;
            // 
            // histogramX
            // 
            this.histogramX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.histogramX.Horizontal = true;
            this.histogramX.Location = new System.Drawing.Point(100, 442);
            this.histogramX.Name = "histogramX";
            this.histogramX.Size = new System.Drawing.Size(400, 75);
            this.histogramX.TabIndex = 1;
            // 
            // canvas
            // 
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.canvas.Location = new System.Drawing.Point(100, 43);
            this.canvas.Name = "canvas";
            this.canvas.ScaleFactor = 0D;
            this.canvas.Size = new System.Drawing.Size(400, 400);
            this.canvas.TabIndex = 0;
            // 
            // cmbSeed
            // 
            this.cmbSeed.FormattingEnabled = true;
            this.cmbSeed.Location = new System.Drawing.Point(600, 181);
            this.cmbSeed.Name = "cmbSeed";
            this.cmbSeed.Size = new System.Drawing.Size(47, 21);
            this.cmbSeed.TabIndex = 5;
            this.cmbSeed.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(520, 305);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Iterations:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(520, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Grid:";
            // 
            // cmbGrid
            // 
            this.cmbGrid.FormattingEnabled = true;
            this.cmbGrid.Items.AddRange(new object[] {
            "10",
            "15",
            "25",
            "50",
            "75",
            "100",
            "150",
            "250",
            "500",
            "750",
            "1000"});
            this.cmbGrid.Location = new System.Drawing.Point(600, 59);
            this.cmbGrid.Name = "cmbGrid";
            this.cmbGrid.Size = new System.Drawing.Size(47, 21);
            this.cmbGrid.TabIndex = 5;
            this.cmbGrid.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(520, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Particles:";
            // 
            // cmbParticles
            // 
            this.cmbParticles.FormattingEnabled = true;
            this.cmbParticles.Items.AddRange(new object[] {
            "10",
            "15",
            "25",
            "50",
            "75",
            "100",
            "150",
            "250",
            "500",
            "750",
            "1000",
            "1500",
            "2500",
            "5000",
            "7500",
            "10000"});
            this.cmbParticles.Location = new System.Drawing.Point(600, 90);
            this.cmbParticles.Name = "cmbParticles";
            this.cmbParticles.Size = new System.Drawing.Size(47, 21);
            this.cmbParticles.TabIndex = 5;
            this.cmbParticles.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(520, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Max init Vx:";
            // 
            // cmbMaxInitVx
            // 
            this.cmbMaxInitVx.FormattingEnabled = true;
            this.cmbMaxInitVx.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "25",
            "50",
            "75",
            "100",
            "150",
            "200",
            "250",
            "500",
            "750",
            "1000"});
            this.cmbMaxInitVx.Location = new System.Drawing.Point(600, 120);
            this.cmbMaxInitVx.Name = "cmbMaxInitVx";
            this.cmbMaxInitVx.Size = new System.Drawing.Size(47, 21);
            this.cmbMaxInitVx.TabIndex = 5;
            this.cmbMaxInitVx.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(520, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Max init Vy:";
            // 
            // cmbMaxInitVy
            // 
            this.cmbMaxInitVy.FormattingEnabled = true;
            this.cmbMaxInitVy.Items.AddRange(new object[] {
            "10",
            "15",
            "20",
            "25",
            "50",
            "75",
            "100",
            "150",
            "200",
            "250",
            "500",
            "750",
            "1000"});
            this.cmbMaxInitVy.Location = new System.Drawing.Point(600, 150);
            this.cmbMaxInitVy.Name = "cmbMaxInitVy";
            this.cmbMaxInitVy.Size = new System.Drawing.Size(47, 21);
            this.cmbMaxInitVy.TabIndex = 5;
            this.cmbMaxInitVy.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(670, 246);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(57, 24);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(601, 272);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(57, 24);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(670, 272);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(57, 24);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblEntropy
            // 
            this.lblEntropy.AutoSize = true;
            this.lblEntropy.Location = new System.Drawing.Point(598, 329);
            this.lblEntropy.Name = "lblEntropy";
            this.lblEntropy.Size = new System.Drawing.Size(16, 13);
            this.lblEntropy.TabIndex = 2;
            this.lblEntropy.Text = "---";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(520, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "S(min):";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 531);
            this.Controls.Add(this.cmbParticles);
            this.Controls.Add(this.cmbGrid);
            this.Controls.Add(this.cmbMaxInitVy);
            this.Controls.Add(this.cmbMaxInitVx);
            this.Controls.Add(this.cmbSeed);
            this.Controls.Add(this.chkDisplayMotion);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnLoadMin);
            this.Controls.Add(this.lblEntropy);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIterations);
            this.Controls.Add(this.histogramY);
            this.Controls.Add(this.histogramX);
            this.Controls.Add(this.canvas);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Entropy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DrawingCanvas canvas;
        private Histogram histogramX;
        private Histogram histogramY;
        private System.Windows.Forms.Label lblIterations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoadMin;
        private System.Windows.Forms.CheckBox chkDisplayMotion;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cmbSeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbGrid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbParticles;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbMaxInitVx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbMaxInitVy;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblEntropy;
        private System.Windows.Forms.Label label1;
    }
}

