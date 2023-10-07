using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Numerics;
using static System.Windows.Forms.LinkLabel;

namespace WinFormsApp1
{
    using static System.Drawing.Drawing2D.Matrix;
    using static System.Net.Mime.MediaTypeNames;

    public partial class Form1 : Form
    {
        private DrawingState _drawingState;
        DrawingObject _drawingObject = new();
        Pen _pen;
        private Graphics g;
        Point helper;

        public Form1()
        {
            InitializeComponent();
            g = Canvas.CreateGraphics();
            _pen = new Pen(Color.Black, 1);
            dx.Text = "10";
            dy.Text = "10";
            kx.Text = "0,5";
            ky.Text = "0,5";
            degree.Text = "90";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _drawingState = DrawingState.FREE_DRAWING;
        }
        private void FreeDraw(PointF p)
        {
            if (_drawingObject.IsPoligon())
            {
                _drawingObject.Clear();
                g.Clear(Color.White);
            }
            if (_drawingObject.IsClosable(p))
            {
                PointF prev = _drawingObject.LastDot;
                _drawingObject.Close();
                g.DrawLine(_pen, prev, _drawingObject.Lines.First.Value.a);
            }
            else
            {
                if (_drawingObject.IsEmpty())
                {
                    g.DrawRectangle(_pen, p.X, p.Y, 1, 1);
                }
                else
                {
                    PointF prev = _drawingObject.LastDot;
                    g.DrawLine(_pen, prev, p);
                }
                _drawingObject.AddDot(p);
            }
        }

        private void Canvas_Click(object sender, EventArgs e)
        {
            if (_drawingState == DrawingState.FREE_DRAWING)
            {
                MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
                Point p = new Point(mouseEventArgs.X, mouseEventArgs.Y);
                FreeDraw(p);
            }
            else if (_drawingState == DrawingState.DRAWING_DOT)
            {
                MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
                helper = new Point(mouseEventArgs.X, mouseEventArgs.Y);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _drawingState = DrawingState.NODRAWING;
            g.Clear(Color.White);
            _drawingObject.Clear();
            helper = Point.Empty;
        }

        private void Button_Move_Click(object sender, EventArgs e)
        {
            if (!_drawingObject.IsPoligon())
            {
                g.Clear(Color.White);
                return;
            }

            float dist_x = float.Parse(dx.Text);
            float dist_y = float.Parse(dy.Text);
            List<float[,]> dot_list = new List<float[,]>();
            List<Point> dot_list_new = new List<Point>();

            //формируем матрицу-строку координат и матрицу преобразования
            for (int i = 0; i < _drawingObject.Lines.Count; i++)
                dot_list.Add(new float[1, 3] { { _drawingObject.Lines.ElementAt(i).a.X, _drawingObject.Lines.ElementAt(i).a.Y, 1 } });
            dot_list.Add(new float[1, 3] { { _drawingObject.Lines.Last().b.X, _drawingObject.Lines.Last().b.Y, 1 } });

            float[,] move_matrix = new float[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { dist_x, dist_y, 1 } };
            float temp = 0;
            float[,] m = new float[3, 1];

            //перемножаем с матрицей
            for (int p = 0; p < dot_list.Count; p++)
            {
                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            temp += dot_list.ElementAt(p)[i, k] * move_matrix[k, j];
                        }
                        m[j, 0] = temp;
                    }
                }
                dot_list_new.Add(new Point((int)m[0, 0], (int)m[1, 0]));
            }

            g.Clear(Color.White);
            Point prev = dot_list_new.First();
            for (int i = 0; i < dot_list_new.Count; i++)
            {
                g.DrawLine(_pen, prev, dot_list_new.ElementAt(i));
                prev = dot_list_new.ElementAt(i);
            }
        }

        private void Button_Turn_Click(object sender, EventArgs e)
        {
            if (!_drawingObject.IsPoligon())
            {
                g.Clear(Color.White);
                return;
            }
            float deg = float.Parse(degree.Text) % 360;
            List<float[,]> dot_list = new List<float[,]>();
            List<Point> dot_list_new = new List<Point>();

            float dist_x = 0;
            float dist_y = 0;
            //формируем матрицу-строку координат и матрицу преобразования
            for (int i = 0; i < _drawingObject.Lines.Count; i++)
            {
                dot_list.Add(new float[1, 3] { { _drawingObject.Lines.ElementAt(i).a.X, _drawingObject.Lines.ElementAt(i).a.Y, 1 } });
                dist_x += _drawingObject.Lines.ElementAt(i).a.X;
                dist_y += _drawingObject.Lines.ElementAt(i).a.Y;
            }
            dist_x = helper.IsEmpty ? dist_x / dot_list.Count : helper.X;
            dist_y = helper.IsEmpty ? dist_y / dot_list.Count : helper.Y;
            dot_list.Add(new float[1, 3] { { _drawingObject.Lines.Last().b.X, _drawingObject.Lines.Last().b.Y, 1 } });

            float[,] deg_matrix = new float[3, 3] { {  (float)Math.Cos(deg), (float)Math.Sin(deg), 0 },
                                                    { -(float)Math.Sin(deg), (float)Math.Cos(deg), 0 },
            { (float)(-dist_x*Math.Cos(deg) + dist_y*Math.Sin(deg) + dist_x), (float)(-dist_x*Math.Sin(deg) - dist_y*Math.Cos(deg) + dist_y), 1 } };
            float temp = 0;
            float[,] m = new float[3, 1];

            //перемножаем с матрицей
            for (int p = 0; p < dot_list.Count; p++)
            {
                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            temp += dot_list.ElementAt(p)[i, k] * deg_matrix[k, j];
                        }
                        m[j, 0] = temp;
                    }
                }
                dot_list_new.Add(new Point((int)m[0, 0], (int)m[1, 0]));
            }

            g.Clear(Color.White);
            Point prev = dot_list_new.First();
            for (int i = 0; i < dot_list_new.Count; i++)
            {
                g.DrawLine(_pen, prev, dot_list_new.ElementAt(i));
                prev = dot_list_new.ElementAt(i);
            }
        }

        private void Button_Scale_Click(object sender, EventArgs e)
        {
            if (!_drawingObject.IsPoligon())
            {
                g.Clear(Color.White);
                return;
            }
            List<float[,]> dot_list = new List<float[,]>();
            List<Point> dot_list_new = new List<Point>();

            float dist_x = 0;
            float dist_y = 0;

            float k_x = float.Parse(kx.Text);
            float k_y = float.Parse(ky.Text);
            //формируем матрицу-строку координат и матрицу преобразования
            for (int i = 0; i < _drawingObject.Lines.Count; i++)
            {
                dot_list.Add(new float[1, 3] { { _drawingObject.Lines.ElementAt(i).a.X, _drawingObject.Lines.ElementAt(i).a.Y, 1 } });
                dist_x += _drawingObject.Lines.ElementAt(i).a.X;
                dist_y += _drawingObject.Lines.ElementAt(i).a.Y;
            }
            dist_x = helper.IsEmpty ? dist_x / dot_list.Count : helper.X;
            dist_y = helper.IsEmpty ? dist_y / dot_list.Count : helper.Y;
            dot_list.Add(new float[1, 3] { { _drawingObject.Lines.Last().b.X, _drawingObject.Lines.Last().b.Y, 1 } });

            float[,] deg_matrix = new float[3, 3] { {  k_x, 0, 0 },
                                                    {  0, k_y, 0 },
            { -k_x*dist_x + dist_x, -k_y*dist_y + dist_y, 1 } };
            float temp = 0;
            float[,] m = new float[3, 1];

            //перемножаем с матрицей
            for (int p = 0; p < dot_list.Count; p++)
            {
                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            temp += dot_list.ElementAt(p)[i, k] * deg_matrix[k, j];
                        }
                        m[j, 0] = temp;
                    }
                }
                dot_list_new.Add(new Point((int)m[0, 0], (int)m[1, 0]));
            }

            g.Clear(Color.White);
            Point prev = dot_list_new.First();
            for (int i = 0; i < dot_list_new.Count; i++)
            {
                g.DrawLine(_pen, prev, dot_list_new.ElementAt(i));
                prev = dot_list_new.ElementAt(i);
            }
        }

        private void Button_SetPoint_Click(object sender, EventArgs e)
        {
            _drawingState = DrawingState.DRAWING_DOT;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!_drawingObject.IsPoligon())
            {
                g.Clear(Color.White);
                return;
            }
            float deg = 90;
            List<float[,]> dot_list = new List<float[,]>();
            List<Point> dot_list_new = new List<Point>();

            float dist_x = 0;
            float dist_y = 0;
            //формируем матрицу-строку координат и матрицу преобразования
            for (int i = 0; i < _drawingObject.Lines.Count; i++)
            {
                dot_list.Add(new float[1, 3] { { _drawingObject.Lines.ElementAt(i).a.X, _drawingObject.Lines.ElementAt(i).a.Y, 1 } });
                dist_x += _drawingObject.Lines.ElementAt(i).a.X;
                dist_y += _drawingObject.Lines.ElementAt(i).a.Y;
            }
            dist_x = helper.IsEmpty ? dist_x / dot_list.Count : helper.X;
            dist_y = helper.IsEmpty ? dist_y / dot_list.Count : helper.Y;
            dot_list.Add(new float[1, 3] { { _drawingObject.Lines.Last().b.X, _drawingObject.Lines.Last().b.Y, 1 } });

            float[,] deg_matrix = new float[3, 3] { {  (float)Math.Cos(deg), (float)Math.Sin(deg), 0 },
                                                    { -(float)Math.Sin(deg), (float)Math.Cos(deg), 0 },
            { (float)(-dist_x*Math.Cos(deg) + dist_y*Math.Sin(deg) + dist_x), (float)(-dist_x*Math.Sin(deg) - dist_y*Math.Cos(deg) + dist_y), 1 } };
            float temp = 0;
            float[,] m = new float[3, 1];

            //перемножаем с матрицей
            for (int p = 0; p < dot_list.Count; p++)
            {
                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            temp += dot_list.ElementAt(p)[i, k] * deg_matrix[k, j];
                        }
                        m[j, 0] = temp;
                    }
                }
                dot_list_new.Add(new Point((int)m[0, 0], (int)m[1, 0]));
            }

            g.Clear(Color.White);
            Point prev = dot_list_new.First();
            for (int i = 0; i < dot_list_new.Count; i++)
            {
                g.DrawLine(_pen, prev, dot_list_new.ElementAt(i));
                prev = dot_list_new.ElementAt(i);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        enum DrawingState { NODRAWING, FREE_DRAWING, DRAWING_DOT }
    }

    class DrawingObject
    {
        public const int DIST_FOR_CONNECTING = 10;
        public PointF LastDot;
        public LinkedList<Line> Lines = new();
        public DrawingObject()
        {
            LastDot = Point.Empty;
        }
        public void AddDot(PointF p)
        {
            if (!LastDot.Equals(PointF.Empty))
            {
                Lines.AddLast(new Line(LastDot, p));
            }
            LastDot = p;
        }



        public bool IsEmpty() => LastDot.Equals(PointF.Empty);

        public bool IsDot() => Lines.Count == 0 && !LastDot.Equals(PointF.Empty);

        public bool IsLine() => Lines.Count == 1;

        public bool IsCurve() => Lines.Count > 0 && !Lines.First.Value.a.Equals(Lines.Last.Value.b);

        public bool IsPoligon() => Lines.Count > 0 && Lines.First.Value.a.Equals(Lines.Last.Value.b);

        public bool IsClosable(PointF p) => Lines.Count > 0 && DIST_FOR_CONNECTING >= Math.Sqrt(Math.Pow(p.X - Lines.First.Value.a.X, 2) +
            Math.Pow(p.Y - Lines.First.Value.a.Y, 2));
        public bool Close()
        {
            if (IsEmpty() || IsDot() || IsLine()) return false;
            AddDot(Lines.First.Value.a);
            return true;
        }

        public void Clear()
        {
            LastDot = PointF.Empty;
            Lines.Clear();
        }
    }

    class Line
    {
        public PointF a;
        public PointF b;
        public Line(PointF a, PointF b) { this.a = a; this.b = b; }
    }

}