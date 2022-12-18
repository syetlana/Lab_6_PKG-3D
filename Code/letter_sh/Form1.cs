using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace letter_sh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(0.9f, 0.9f, 0.9f, 1);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 m;

            m =
                Matrix4.LookAt(0, 1, 3, 0, 0, 0, 0, 1, 0) *
                Matrix4.CreatePerspectiveFieldOfView((float)(60 * Math.PI / 180.0), 1, 0.1f, 10);
            GL.LoadMatrix(ref m);
        }

        Vector3[] vert = new Vector3[] {       
            // Размер 6,826171875 на 8,736328125
            // Start point
            new Vector3( 1.447265625f, 0f, 0 ),
            // PolyLineSegment
            new Vector3( 6.826171875f, 0f, 0 ),
            new Vector3( 6.826171875f, 8.58984375f, 0 ),
            new Vector3( 5.689453125f, 8.58984375f, 0 ),
            new Vector3( 5.689453125f, 1.013671875f, 0 ),
            new Vector3( 2.583984375f, 1.013671875f, 0 ),
            new Vector3( 2.583984375f, 5.4375f, 0 ),
            // PolyBezierSegment
            new Vector3( 2.583984375f, 6.3359375f, 0 ),
            new Vector3( 2.5419921875f, 6.99414074420929f, 0 ),
            new Vector3( 2.4580078125f, 7.412109375f, 0 ),
            new Vector3( 2.3740234375f, 7.830078125f, 0 ),
            new Vector3( 2.197265625f, 8.15527346730232f, 0 ),
            new Vector3( 1.927734375f, 8.3876953125f, 0 ),
            new Vector3( 1.65820300579071f, 8.62011718004942f, 0 ),
            new Vector3( 1.31640613079071f, 8.736328125f, 0 ),
            new Vector3( 0.90234375f, 8.736328125f, 0 ),
            new Vector3( 0.65234375f, 8.736328125f, 0 ),
            new Vector3( 0.351562470197678f, 8.68945311754942f, 0 ),
            new Vector3( 0f, 8.595703125f, 0 ),
            // LineSegment
            new Vector3( 0.193359375f, 7.587890625f, 0 ),
            // PolyBezierSegment
            new Vector3( 0.380859345197678f, 7.66601568460464f, 0 ),
            new Vector3( 0.541015625f, 7.70507818460464f, 0 ),
            new Vector3( 0.673828125f, 7.705078125f, 0 ),
            new Vector3( 0.931640625f, 7.70507818460464f, 0 ),
            new Vector3( 1.125f, 7.60839849710464f, 0 ),
            new Vector3( 1.25390625f, 7.4150390625f, 0 ),
            new Vector3( 1.38281238079071f, 7.2216796875f, 0 ),
            new Vector3( 1.44726550579071f, 6.765625f, 0 ),
            new Vector3( 1.447265625f, 6.046875f, 0 ),
            new Vector3( 1.447265625f, 0f, 0 ),
        };

        float ra = 0, rb = 135;

        void drawAxes()
        {
            // Рисуем оси
            GL.Begin(PrimitiveType.Lines);

            GL.Color3(1.0f, 0, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(15, 0, 0);

            GL.Color3(0, 1.0f, 0);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 15, 0);

            GL.Color3(0, 0, 1.0f);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 15);

            GL.End();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);

            Matrix4 m = 
                Matrix4.CreateTranslation( -3.4f, -4.4f, 0 ) *
                Matrix4.CreateRotationX(rb) *
                Matrix4.CreateRotationY(ra) * 
                Matrix4.CreateScale( 0.2f, 0.2f, 0.2f );

            GL.LoadMatrix(ref m);

            drawAxes();

            GL.Color3(1.0f, 0.2f, 0);

            GL.Begin(PrimitiveType.Lines);

            for (int i = 0; i < vert.Length - 1; i++)
            {
                GL.Vertex3(vert[i].X, vert[i].Y, 0);
                GL.Vertex3(vert[i+1].X, vert[i+1].Y, 0);

                GL.Vertex3(vert[i].X, vert[i].Y, 3);
                GL.Vertex3(vert[i + 1].X, vert[i + 1].Y, 3);

                GL.Vertex3(vert[i].X, vert[i].Y, 0);
                GL.Vertex3(vert[i].X, vert[i].Y, 3);
            }

            GL.End();

            glControl1.SwapBuffers();
        }

        int oldmx, oldmy;

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ra += (e.X - oldmx) / 100.0f;
                rb += (e.Y - oldmy) / 100.0f;

                glControl1.Invalidate();
            }

            oldmx = e.X;
            oldmy = e.Y;
        }
    }
}
