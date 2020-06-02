using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using classGeometry;

namespace demoGeometry
{
    public partial class FormMain : Form
    {
        Graphics g;
        Polygon polygon = new classGeometry.Polygon();
        public FormMain()
        {
            InitializeComponent();
            g = this.CreateGraphics();
        }

        private void buttonBuildHull_Click(object sender, EventArgs e)
        {
            MessageBox.Show(polygon.ToString());
            Polygon newPolygon = polygon.СonvexHull();
            MessageBox.Show(newPolygon.ToString());
            for (int i = 0; i < newPolygon.Count - 1; i++)
                g.DrawLine(Pens.Red, newPolygon.GetPoint(i), newPolygon.GetPoint(i + 1));
            g.DrawLine(Pens.Red, newPolygon.GetPoint(newPolygon.Count - 1), newPolygon.GetPoint(0));
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            polygon.Add(new classGeometry.Point(e.X, e.Y));
            g.FillEllipse(Brushes.Blue, e.X - 5, e.Y - 5, 10, 10);
        }
    }
}
