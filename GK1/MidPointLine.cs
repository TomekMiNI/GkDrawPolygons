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

        private void midPointLine(int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            //oblicz wspolczynniki a i b
            double a = 0, b = 0;
            if((x2-x1)==0)
            {
                //nie jest funkcja = pionowa
                a = Double.MaxValue;
                b = x1;
            }
            else
            {
                a = (double)(y2 - y1) / (double)(x2 - x1);
                b = y1 - a * x1;
            }
            _factortOfSegment = new Tuple<double, double>(a, b);
            //I cwiartka y2-y1>0 x2-x2>0
            if (dy > 0 && dx >= 0)
            {
                if (dx != 0 && Math.Abs(dy / dx) < 1)
                //cw 1.1
                {
                    int d = 2 * dy - dx; //initial value of d
                    int incrE = 2 * dy; //increment used for move to E
                    int incrNE = 2 * (dy - dx); //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (x < x2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            x++;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            x++;
                            y++;
                        }
                        mySetPixel(x, y);

                    }
                }
                else
                /* cw 1.2*/
                {
                    int d = dy - 2 * dx; //initial value of d  | *2
                    int incrE = 2 * (dy - dx); //increment used for move to E
                    int incrNE = -2 * dx; //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (y < y2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            x++;
                            y++;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            y++;
                        }
                        mySetPixel(x, y);
                    }
                }
            }
            //II cwiartka y2-y1>0 x2-x1<0
            //cw 2.1
            else if (dy >= 0 && dx < 0)
            {
                if (Math.Abs(dy / dx) >= 1)
                {
                    int d = -dy - 2 * dx; //initial value of d  | *2
                    int incrE = -2 * dx; //increment used for move to E
                    int incrNE = -2 * dx - 2 * dy; //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (y < y2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            y++;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            y++;
                            x--;
                        }
                        mySetPixel(x, y);
                    }
                }
                //cw 2.2
                else
                {
                    int d = -2 * dy - dx; //initial value of d  | *2
                    int incrE = -2 * dy - 2 * dx; //increment used for move to E
                    int incrNE = -2 * dy; //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (x > x2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            y++;
                            x--;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            x--;
                        }
                        mySetPixel(x, y);
                    }
                }
            }
            //III cwiartka y2-y1<0 x2-x1<0
            else if (dy < 0 && dx < 0)
            {
                // cw 3.1
                if (Math.Abs(dy / dx) < 1)
                {
                    int d = -2 * dy + dx; //initial value of d  | *2
                    int incrE = -2 * dy; //increment used for move to E
                    int incrNE = -2 * dy + 2 * dx; //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (x > x2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            x--;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            y--;
                            x--;
                        }
                        mySetPixel(x, y);
                    }
                }
                else
                {
                    // cw 3.2
                    int d = -dy + 2 * dx; //initial value of d  | *2
                    int incrE = -2 * dy + 2 * dx; //increment used for move to E
                    int incrNE = 2 * dx; //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (y > y2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            y--;
                            x--;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            y--;
                        }
                        mySetPixel(x, y);
                    }
                }
            }

            //IV cwiartka y2-y1<0 x2-x1>0
            else
            {
                // cw 4.1
                if (Math.Abs(dy) > Math.Abs(dx))
                {
                    int d = dy + 2 * dx; //initial value of d  | *2
                    int incrE = 2 * dx; //increment used for move to E
                    int incrNE = 2 * dx + 2 * dy; //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (y > y2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            y--;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            x++;
                            y--;
                        }
                        mySetPixel(x, y);
                    }
                }
                else
                {
                    // cw 4.2
                    int d = 2 * dy + dx; //initial value of d  | *2
                    int incrE = 2 * dx + 2 * dy; //increment used for move to E
                    int incrNE = 2 * dy; //increment used for move to NE
                    int x = x1;
                    int y = y1;
                    mySetPixel(x, y);
                    while (x < x2)
                    {
                        if (d < 0) //choose E
                        {
                            d += incrE;
                            x++;
                            y--;
                        }
                        else //choose NE
                        {
                            d += incrNE;
                            x++;
                        }
                        mySetPixel(x, y);
                    }
                }
            }
        }
        private void mySetPixel(int x, int y)
        {
            _segment.Add(new Point(x, y));
        }
    }
}

