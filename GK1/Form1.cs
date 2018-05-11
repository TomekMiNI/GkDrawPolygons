using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK1
{
    public partial class Form1 : Form
    {
        private bool _startPaint = false;
        private bool _isPolygon = false;
        private bool _timeToSwapV = false;
        private bool _timeToSwapP = false;
        private bool _timeToSwapE = false;
        private int _xstart, _xend, _ystart, _yend;
        private int _selectedV = -1;
        private int _selectedE = -1;
        private Point centerPoint;
        private List<Point> _segment = new List<Point>();
        private List<Point> shadow = new List<Point>();
        private List<List<Point>> _listOfSegments = new List<List<Point>>();
        private List<bool> _isVertical = new List<bool>();
        private List<bool> _isHorizontal = new List<bool>();
        private List<int> _hasAngle = new List<int>();
        private List<Tuple<double, double>> _factors = new List<Tuple<double, double>>();
        private Tuple<double, double> _factortOfSegment;
        public Form1()
        {
            InitializeComponent();
        }

        private void refreshPolygon(object sender, PaintEventArgs e)
        {
            if(_isPolygon)
                e.Graphics.DrawEllipse(new Pen(Color.Gray), new Rectangle(centerPoint.X - 3, centerPoint.Y - 3, 7, 7));
            foreach(var el in shadow)
                e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(el.X, el.Y, 1, 1));
            if (_segment.Count > 0)
                e.Graphics.FillRectangle(Brushes.Blue, new Rectangle(_segment.First().X - 3, _segment.First().Y - 3, 7, 7));
            foreach (var el in _segment)
                e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(el.X, el.Y, 1, 1));
            int i = 0;
            foreach (var el in _listOfSegments)
            {
                if(_isHorizontal[i])
                {
                    Point middlePoint = new Point((int)(((double)el.First().X + (double)el.Last().X) / 2) + 3, (int)(((double)el.First().Y + (double)el.Last().Y) / 2) + 3);
                    e.Graphics.DrawImage(GK1.Properties.Resources.horimg, middlePoint);
                }
                else if(_isVertical[i])
                {
                    Point middlePoint = new Point((int)(((double)el.First().X + (double)el.Last().X) / 2) + 3, (int)(((double)el.First().Y + (double)el.Last().Y) / 2) + 3);
                    e.Graphics.DrawImage(GK1.Properties.Resources.verimg, middlePoint);
                }
                else if(_hasAngle[i]!=-1)
                {
                    e.Graphics.DrawImage(GK1.Properties.Resources.angleimg, new Point(el.First().X + 3, el.First().Y + 3));
                }
                i++;
                e.Graphics.FillRectangle(Brushes.Blue, new Rectangle(el.First().X - 3, el.First().Y - 3, 7, 7));
                foreach (var el2 in el)
                    e.Graphics.DrawRectangle(new Pen(Color.Black), new Rectangle(el2.X, el2.Y, 1, 1));
            }
            if(_selectedV!=-1)
                e.Graphics.FillRectangle(Brushes.Yellow, new Rectangle(_listOfSegments[_selectedV].First().X - 3, _listOfSegments[_selectedV].First().Y - 3, 7, 7));
            if(_selectedE!=-1)
            {
                e.Graphics.FillRectangle(Brushes.Silver, new Rectangle(_listOfSegments[_selectedE].First().X - 3, _listOfSegments[_selectedE].First().Y - 3, 7, 7));
                e.Graphics.FillRectangle(Brushes.Silver, new Rectangle(_listOfSegments[(_selectedE+1)%_listOfSegments.Count].First().X - 3, _listOfSegments[(_selectedE + 1) % _listOfSegments.Count].First().Y - 3, 7, 7));
            }
        }

        private void swapEdge(MouseEventArgs e)
        {

        }

        private int getY(int i, int x)
        {
            return (int)(_factors[i].Item1 * (double)x + _factors[i].Item2);
        }
        private int getX(int i, int y)
        {
            if (_factors[i].Item1 == Double.MaxValue)
                return (int)_factors[i].Item2;
            if (_factors[i].Item1 == 0) //pozioma, ale nie sztywno pozioma!!!
            {
                return _listOfSegments[i].First().X;
            }
            return (int)(((double)y - _factors[i].Item2) / _factors[i].Item1);
        }
        private void updateCenter()
        {
            int sumX = 0;
            int sumY = 0;
            foreach(var el in _listOfSegments)
            {
                sumX += el.First().X;
                sumY += el.First().Y;
            }
            centerPoint = new Point(sumX / _listOfSegments.Count, sumY / _listOfSegments.Count);
        }
        
        private void swapPolygon(MouseEventArgs e)
        {
            _segment.Clear();
            int diffX = e.Location.X - centerPoint.X;
            int diffY = e.Location.Y - centerPoint.Y;
            for(int j=0;j<_listOfSegments.Count;j++)
                for(int i=0;i< _listOfSegments[j].Count;i++)
                {
                    _listOfSegments[j][i] = new Point(_listOfSegments[j][i].X + diffX, _listOfSegments[j][i].Y + diffY);
                }
            
        }
        private void swapVerticle(MouseEventArgs e)
        {
            int n = _listOfSegments.Count;
            
            Point target = e.Location;
            int prev = (_selectedV + n - 1) % n;
            Point prevStart = _listOfSegments[prev].First();

            if (_isVertical[prev] || _isHorizontal[prev] || _hasAngle[prev]!=-1)
            {
                _segment.Clear();
                //correct prev and segment prev-1
                if (_isVertical[prev])
                {
                    prevStart = new Point(target.X, getY((prev + n - 1) % n, target.X));
                }
                else if(_isHorizontal[prev])
                {
                    prevStart = new Point(getX((prev + n - 1) % n, target.Y), target.Y);
                }
                else
                {
                    double newB;
                    if (_factors[prev].Item1 != Double.MaxValue)
                        newB = (double)target.Y - _factors[prev].Item1 * (double)target.X;
                    else
                        newB = target.X;
                    prevStart = crossedPoint(_factors[prev].Item1, newB, _factors[(prev + n - 1) % n].Item1, _factors[(prev + n - 1) % n].Item2);
                }
                //correct prev-1
                midPointLine(_listOfSegments[(prev + n - 1) % n].First().X, _listOfSegments[(prev + n - 1) % n].First().Y, prevStart.X, prevStart.Y);
                _listOfSegments[(prev + n - 1) % n] = new List<Point>(_segment);
                

            }

            //refine first edge
            _segment.Clear();
            _xend = target.X;
            _yend = target.Y;
            _xstart = prevStart.X;
            _ystart = prevStart.Y;
            midPointLine(_xstart, _ystart, _xend, _yend);
            _listOfSegments[prev] = new List<Point>(_segment);
            _factors[prev] = new Tuple<double, double>(_factortOfSegment.Item1,_factortOfSegment.Item2);

            if(_hasAngle[_selectedV]!=-1)
            {
                setAngle(_hasAngle[_selectedV]);
                return;
            }
            //refine second edge
            _segment.Clear();
            int next = (_selectedV + 1) % n;
            Point nextStart = _listOfSegments[next].First();
            bool correctNext = false;
            if(_isVertical[_selectedV] || _isHorizontal[_selectedV])
            {
                //correct nextStart
                correctNext = true;
                if(_isHorizontal[_selectedV])
                {
                    nextStart = new Point(getX(next, _yend), _yend);
                }
                else
                {
                    nextStart = new Point(_xend, getY(next, _xend));
                }
            }
            _xstart = nextStart.X;
            _ystart = nextStart.Y;
            midPointLine(_xend, _yend, _xstart, _ystart);
            _listOfSegments[_selectedV] = new List<Point>(_segment);
            _factors[_selectedV] = new Tuple<double,double>(_factortOfSegment.Item1,_factortOfSegment.Item2);
            if(correctNext)
            {
                _segment.Clear();
                midPointLine(nextStart.X, nextStart.Y, _listOfSegments[next].Last().X, _listOfSegments[next].Last().Y);
                _listOfSegments[next] = new List<Point>(_segment);
            }
            if (_hasAngle[(_selectedV + 1) % _listOfSegments.Count]!=-1)
            {
                _selectedV = (_selectedV + 1) % _listOfSegments.Count;
                setAngle(_hasAngle[(_selectedV + 1) % _listOfSegments.Count]);
            }
            _segment.Clear();
        }

        private void drawing(object sender, MouseEventArgs e)
        {
            if (_startPaint)
            {
                _xend = e.Location.X;
                _yend = e.Location.Y;

                pic_box.Invalidate();
                _segment.Clear();
                midPointLine(_xstart, _ystart, _xend, _yend);
            }
            else if(_isPolygon)
            {
                if (_timeToSwapV)
                {
                    swapVerticle(e);
                }
                else if (_timeToSwapP)
                {
                    swapPolygon(e);
                }
                else if (_timeToSwapE)
                {
                    swapEdge(e);
                }
                updateCenter();
                pic_box.Invalidate();
            }
        }
        

        private void stopSwaping(object sender, MouseEventArgs e)
        {
            if (_timeToSwapV)
            {
                shadow.Clear();
                pic_box.Invalidate();
                _timeToSwapV = false;
            }
            else if(_timeToSwapP)
            {
                _timeToSwapP = false;
            }
        }

        private bool isVerticalOnThisEdge(Point _check, int i)
        {
            Point _p1 = _listOfSegments[i].First();
            Point _p2 = _listOfSegments[i].Last();
            if (_check.X <= Math.Max(_p1.X, _p2.X) && _check.X >= Math.Min(_p1.X, _p2.X) && _check.Y <= Math.Max(_p1.Y, _p2.Y) && _check.Y >= Math.Min(_p1.Y, _p2.Y))
            {
                if ((_check.Y - _p2.Y) == 0 || (_check.Y - _p1.Y) == 0 || (_check.X - _p2.X) == 0 || (_check.X - _p1.X) == 0)
                {
                    if (Math.Abs(_check.Y - _p1.Y) == 0 && Math.Abs(_p2.Y - _check.Y) == 0)
                    {
                        if (_check.X <= Math.Max(_p1.X, _p2.X) && _check.X >= Math.Min(_p1.X, _p2.X))
                        {
                            return true;
                        }
                        return false;
                    }
                    if (Math.Abs(_check.X - _p1.X) == 0 && Math.Abs(_p2.X - _check.X) == 0)
                    {
                        if (_check.Y <= Math.Max(_p1.Y, _p2.Y) && _check.Y >= Math.Min(_p1.Y, _p2.Y))
                        {
                            return true;
                        }
                        return false;
                    }
                }
                //double tg1 = (double)(((double)_p1.Y - (double)_check.Y) / ((double)_p1.X - (double)_check.X));
                //double tg2 = (double)(((double)_check.Y - (double)_p2.Y) / ((double)_check.X - (double)_p2.X));
                //if (Math.Abs(tg1 - tg2) <= 0.1)
                //{
                //    return true;
                //}
                foreach (var el in _listOfSegments[i])
                    if (_check == el)
                        return true;
            }
            return false;
        }

        private void addVerticle(object sender, MouseEventArgs e)
        {
            if (_isPolygon)
            {
                for (int i = 0; i < _listOfSegments.Count; i++)
                    if (isVerticalOnThisEdge(e.Location, i))
                    {
                        _segment.Clear();
                        midPointLine(_listOfSegments[i].First().X, _listOfSegments[i].First().Y, e.Location.X, e.Location.Y);
                        _listOfSegments[i] = new List<Point>(_segment);
                        _factors[i] = new Tuple<double, double>(_factortOfSegment.Item1, _factortOfSegment.Item2);

                        _segment.Clear();
                        midPointLine(e.Location.X, e.Location.Y, _listOfSegments[(i + 1) % _listOfSegments.Count].First().X, _listOfSegments[(i + 1) % _listOfSegments.Count].First().Y);
                        _listOfSegments.Insert(i + 1, new List<Point>(_segment));
                        _factors.Insert(i + 1, new Tuple<double,double>(_factortOfSegment.Item1,_factortOfSegment.Item2));
                        _isVertical.Insert(i + 1, false);
                        _isHorizontal.Insert(i + 1, false);
                        _hasAngle.Insert(i + 1, -1);
                        _isVertical[i] = _isHorizontal[i] = _isHorizontal[(i + 1) % _listOfSegments.Count] = _isVertical[(i + 1) % _listOfSegments.Count] = false;
                        _hasAngle[i] = _hasAngle[(i + 1) % _listOfSegments.Count] = -1;
                        _segment.Clear();
                        updateCenter();
                        pic_box.Invalidate();
                        return;
                    }
            }
        }
        private bool centerClick(Point p)
        {
            if (Math.Sqrt((p.X - centerPoint.X) * (p.X - centerPoint.X) + (p.Y - centerPoint.Y) * (p.Y - centerPoint.Y)) > 3)
                return false;
            _timeToSwapP = true;
            return true;
        }
        private void modifyPolygon(MouseEventArgs e)
        {
            Point verify = e.Location;
            if (centerClick(verify))
                return;
            for (int i = 0; i < _listOfSegments.Count; i++)
                //vertically.Text = $"{verify.X}, {verify.Y} - {_listOfSegments[i].First().X}.{_listOfSegments[i].First().Y}";
                if (Math.Abs(verify.X - _listOfSegments[i].First().X) <= 3 && Math.Abs(verify.Y - _listOfSegments[i].First().Y) <= 3)
                {
                    for (int j = 0; j<4; j++)
                    {
                        int k = (i + _listOfSegments.Count + j - 2) % _listOfSegments.Count;
                        for (int l = 0; l < _listOfSegments[k].Count; l += 4)
                            shadow.Add(_listOfSegments[k][l]);
                    }
                    _selectedE = -1;
                    _timeToSwapV = true;
                    drawVerticle(_listOfSegments[i].First().X, _listOfSegments[i].First().Y, Brushes.Yellow);
                    _selectedV = i;
                    return;
                }
            for (int i = 0; i < _listOfSegments.Count; i++)
                if (isVerticalOnThisEdge(verify, i))
                {
                    _timeToSwapE = true;
                    Pen p = new Pen(Brushes.Silver);
                    drawVerticle(_listOfSegments[i].First().X, _listOfSegments[i].First().Y, p.Brush);
                    drawVerticle(_listOfSegments[(i + 1) % _listOfSegments.Count].First().X, _listOfSegments[(i + 1) % _listOfSegments.Count].First().Y, p.Brush);
                    p.Dispose();
                    _selectedE = i;
                    return;
                }
            
            
        }

        private Point crossedPoint(double a1, double b1, double a2, double b2)
        {
            if (a1 == Double.MaxValue ^ a2 == Double.MaxValue)
            {
                if (a1 == Double.MaxValue)
                {
                    return new Point((int)b1, (int)(a2 * b1 + b2));
                }
                else
                {
                    return new Point((int)b2, (int)(a1 * b2 + b1));
                }
            }
            if (a1== a2)
            {
                a2 = _factors[(_selectedV + 2) % _listOfSegments.Count].Item1;
                b2 = _factors[(_selectedV + 2) % _listOfSegments.Count].Item2;
                if(a1==Double.MaxValue)
                {
                    return new Point((int)b1, (int)(a2 * b1 + b2));
                }
            }
            
            double x = (b1 - b2) / (a2 - a1);
            return new Point((int)x, (int)(a1 * x + b1));
        }
        private void setAngle(int val)
        {
            int n = _listOfSegments.Count;
            double alfa = val*Math.PI/180;
            double prevA = _factors[(_selectedV + n - 1) % n].Item1;
            if (prevA == Double.MaxValue)
                alfa += Math.PI/2;
            else
                alfa += Math.Atan(prevA);
            alfa %= Math.PI;
            double newA;
            double newB = Double.MaxValue;
            if (Math.Abs(alfa - Math.PI / 2) < Double.Epsilon)
            {
                if (prevA == 0)
                {
                    newA = Double.MaxValue;

                    newB = _listOfSegments[(_selectedV + _listOfSegments.Count - 1) % _listOfSegments.Count].Last().X;
                }
                else
                {
                    newA = -1 / prevA;
                }
            }
            else
            {
                newA = Math.Tan(alfa);
            }

            //mam juz nowy wspolczynnik kierunkowy
            if(newB==Double.MaxValue)
                newB = (double)_listOfSegments[(_selectedV + _listOfSegments.Count - 1) % _listOfSegments.Count].Last().Y - (double)_listOfSegments[(_selectedV + _listOfSegments.Count - 1) % _listOfSegments.Count].Last().X * newA;
            Point cross = crossedPoint(newA, newB, _factors[(_selectedV + 1) % n].Item1, _factors[(_selectedV + 1) % n].Item2);
            _segment.Clear();
            midPointLine(_listOfSegments[(_selectedV + _listOfSegments.Count - 1) % _listOfSegments.Count].Last().X, _listOfSegments[(_selectedV + _listOfSegments.Count - 1) % _listOfSegments.Count].Last().Y, cross.X, cross.Y);
            _listOfSegments[_selectedV] = new List<Point>(_segment);
            _factors[_selectedV] = new Tuple<double, double>(_factortOfSegment.Item1, _factortOfSegment.Item2);
            _segment.Clear();
            midPointLine(cross.X, cross.Y, _listOfSegments[(_selectedV + 1) % n].Last().X, _listOfSegments[(_selectedV + 1) % n].Last().Y);
            _listOfSegments[(_selectedV + 1) % n] = new List<Point>(_segment);
            _factors[(_selectedV + 1) % n] = new Tuple<double, double>(_factortOfSegment.Item1, _factortOfSegment.Item2);
            _segment.Clear();
            updateCenter();
            pic_box.Invalidate();
        }


        private void drawPolygon(MouseEventArgs e)
        {
            if (!_startPaint)
            {
                //rozpoczynamy rysowanie wielokąta
                _startPaint = true;
                _xstart = e.Location.X;
                _ystart = e.Location.Y;
            }
            else
            {
                Point end = new Point(e.Location.X, e.Location.Y);
                if (_listOfSegments.Count > 1 && Math.Abs(end.X - _listOfSegments.First().First().X) <= 3 && Math.Abs(end.Y - _listOfSegments.First().First().Y) <= 3)
                {
                    _segment.Clear();
                    midPointLine(_listOfSegments.Last().Last().X, _listOfSegments.Last().Last().Y, _listOfSegments.First().First().X, _listOfSegments.First().First().Y);

                    _startPaint = false;
                    _isPolygon = true;
                }
                _xstart = end.X;
                _ystart = end.Y;
                _listOfSegments.Add(new List<Point>(_segment));
                _factors.Add(new Tuple<double, double>(_factortOfSegment.Item1, _factortOfSegment.Item2));
                _isHorizontal.Add(false);
                _isVertical.Add(false);
                _hasAngle.Add(-1);
                updateCenter();
                pic_box.Invalidate();
            }
        }
        private void drawingOnClick(object sender, MouseEventArgs e)
        {
            if (_selectedV!=-1)
            {
                drawVerticle(_listOfSegments[_selectedV].First().X, _listOfSegments[_selectedV].First().Y, Brushes.Blue);
                _selectedV = -1;
            }
            if(_selectedE!=-1)
            {
                drawVerticle(_listOfSegments[_selectedE].First().X, _listOfSegments[_selectedE].First().Y, Brushes.Blue);
                drawVerticle(_listOfSegments[(_selectedE+1)%_listOfSegments.Count].First().X, _listOfSegments[(_selectedE + 1) % _listOfSegments.Count].First().Y, Brushes.Blue);
                _selectedE = -1;
            }
            if (!_isPolygon)
            {
                drawPolygon(e);
            }
            else
            {
                modifyPolygon(e);
            }
        }
        private void drawVerticle(int x, int y, Brush b)
        {
            pic_box.CreateGraphics().FillRectangle(b, new Rectangle(x - 3, y - 3, 7, 7));

        }

    }
}
