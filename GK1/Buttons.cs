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

        private void deleteVerticle(object sender, EventArgs e)
        {
            int n = _listOfSegments.Count;
            if (n > 3)
                if (_selectedV != -1)
                {
                    _segment.Clear();

                    //construct new edge
                    midPointLine(_listOfSegments[(_selectedV + n - 1) % n].First().X, _listOfSegments[(_selectedV - 1 + n) % n].First().Y, _listOfSegments[(_selectedV + 1) % n].First().X, _listOfSegments[(_selectedV + 1) % n].First().Y);

                    //"merge" two 
                    _listOfSegments[(_selectedV + n - 1) % n] = new List<Point>(_segment);
                    _listOfSegments.RemoveAt(_selectedV % n);
                    _isVertical.RemoveAt(_selectedV % n);
                    _isHorizontal.RemoveAt(_selectedV % n);
                    _factors.RemoveAt(_selectedV % n);
                    _hasAngle.RemoveAt(_selectedV % n);
                    n = _listOfSegments.Count;
                    _isHorizontal[_selectedV % n] = _isVertical[_selectedV % n] = _isVertical[(_selectedV + n - 1) % n] = _isHorizontal[(_selectedV + n - 1) % n] = false;
                    _hasAngle[_selectedV % n] = _hasAngle[(_selectedV + n - 1) % n] = -1;
                    _selectedV = -1;
                    _segment.Clear();
                    updateCenter();
                    pic_box.Invalidate();
                }
            _selectedV = -1;
        }
        private void setVertical(object sender, EventArgs e)
        {
            if (_selectedE == -1) return;
            if (_isVertical[_selectedE])
            {
                _isVertical[_selectedE] = false;
            }
            else if (_factors[(_selectedE + 1) % _listOfSegments.Count].Item1 != Double.MaxValue && _factors[(_selectedE + _listOfSegments.Count - 1) % _listOfSegments.Count].Item1 != Double.MaxValue && !_isHorizontal[_selectedE] && _hasAngle[_selectedE] == -1 && _hasAngle[(_selectedE + 1) % _listOfSegments.Count] == -1)
            {
                _isHorizontal[_selectedE] = false;
                _segment.Clear();
                int newY = getY((_selectedE + 1) % _listOfSegments.Count, _listOfSegments[_selectedE].First().X);
                int newX = _listOfSegments[_selectedE].First().X;
                midPointLine(newX, _listOfSegments[_selectedE].First().Y, newX, newY);
                _listOfSegments[_selectedE] = new List<Point>(_segment);
                _factors[_selectedE] = new Tuple<double, double>(_factortOfSegment.Item1, _factortOfSegment.Item2);
                _segment.Clear();
                midPointLine(newX, newY, _listOfSegments[(_selectedE + 1) % _listOfSegments.Count].Last().X, _listOfSegments[(_selectedE + 1) % _listOfSegments.Count].Last().Y);
                _listOfSegments[(_selectedE + 1) % _listOfSegments.Count] = new List<Point>(_segment);
                _isVertical[_selectedE] = true;
                _segment.Clear();

                updateCenter();

                pic_box.Invalidate();
            }
            _selectedE = -1;
        }
        private void setHorizontal(object sender, EventArgs e)
        {
            if (_selectedE == -1) return;
            if (_isHorizontal[_selectedE])
            {
                _isHorizontal[_selectedE] = false;
            }
            else if (!_isHorizontal[(_selectedE + 1) % _listOfSegments.Count] && !_isHorizontal[(_selectedE + _listOfSegments.Count - 1) % _listOfSegments.Count] && !_isVertical[_selectedE] && _hasAngle[_selectedE] == -1 && _hasAngle[(_selectedE + 1) % _listOfSegments.Count] == -1)
            {
                _segment.Clear();
                int newX = getX((_selectedE + 1) % _listOfSegments.Count, _listOfSegments[_selectedE].First().Y);
                int newY = _listOfSegments[_selectedE].First().Y;
                midPointLine(_listOfSegments[_selectedE].First().X, newY, newX, newY);
                _listOfSegments[_selectedE] = new List<Point>(_segment);
                _factors[_selectedE] = new Tuple<double, double>(_factortOfSegment.Item1, _factortOfSegment.Item2);

                _segment.Clear();
                midPointLine(newX, newY, _listOfSegments[(_selectedE + 1) % _listOfSegments.Count].Last().X, _listOfSegments[(_selectedE + 1) % _listOfSegments.Count].Last().Y);
                _listOfSegments[(_selectedE + 1) % _listOfSegments.Count] = new List<Point>(_segment);
                _isHorizontal[_selectedE] = true;
                _segment.Clear();
                updateCenter();
                pic_box.Invalidate();
            }
            _selectedE = -1;
        }
        private void introduceAngle(object sender, EventArgs e)
        {
            int value = 0;
            if (Int32.TryParse(_angleText.Text, out value))
            {
                if (_selectedV != -1)
                {
                    int n = _listOfSegments.Count;
                    if (!_isVertical[_selectedV] && !_isHorizontal[_selectedV] && !_isVertical[(_selectedV + n - 1) % n] && !_isHorizontal[(_selectedV + n - 1) % n])
                    {
                        setAngle(-value);
                        _hasAngle[_selectedV] = -(value % 180);
                    }
                }
            }
            else
                MessageBox.Show("Wrong value!");
        }


        private void resetClick(object sender, EventArgs e)
        {
            if(_selectedV!=-1)
            {
                _hasAngle[_selectedV] = -1;
            }
        }
        private void newDrawing(object sender, EventArgs e)
        {
            _startPaint = false;
            _selectedE = _selectedV = -1;
            _listOfSegments.Clear();
            _segment.Clear();
            _isVertical.Clear();
            _isHorizontal.Clear();
            _hasAngle.Clear();
            _isPolygon = false;
            pic_box.Invalidate();
        }
    }
}
