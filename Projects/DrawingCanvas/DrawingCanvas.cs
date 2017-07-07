using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Entropy
{
    public partial class DrawingCanvas : UserControl
    {
        private List<Vector> positions = new List<Vector>();
        private List<Brush> brushes = new List<Brush>();
        private float diameter;
        private bool display;
        private float radius;
        private float scaleFactor;

        public bool Display { set { display = value; if (!display) this.Invalidate(); } }
        public double Radius { set { radius = (float)value; diameter = radius * 2f; } }
        public double ScaleFactor { get { return (double)scaleFactor; } set { scaleFactor = (float)value; } }
        public List<Vector> Positions { set { positions = value; } }
        public List<Brush> Brushes { set { brushes = value; } }

        public DrawingCanvas()
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
            if (!display) return;

            Graphics g = e.Graphics;
            for (int i = 0; i < positions.Count; i++)
//                g.DrawEllipse(Pens.Black, ((float)position.x - radius) * scaleFactor, ((float)position.y - radius) * scaleFactor, diameter * scaleFactor, diameter * scaleFactor);
                g.FillEllipse(brushes[i], ((float)positions[i].x - radius) * scaleFactor, ((float)positions[i].y - radius) * scaleFactor, diameter * scaleFactor, diameter * scaleFactor);
        }
    }
}