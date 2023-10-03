using System.Data;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private DrawingState _drawingState;
        DrawingObject _drawingObject = new();
        Pen _pen;
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = Canvas.CreateGraphics();
            _pen = new Pen(Color.Black, 1);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _drawingState = DrawingState.FREE_DROWING;
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
            if (_drawingState == DrawingState.FREE_DROWING)
            {
                MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
                Point p = new Point(mouseEventArgs.X, mouseEventArgs.Y);
                FreeDraw(p);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _drawingState = DrawingState.NODROWING;
            g.Clear(Color.White);
            _drawingObject.Clear();
        }

        enum DrawingState { NODROWING, FREE_DROWING, DROWING_DOT }
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