using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class CoordinateHandler
    {
        private Point s_mouseSource;
        private Game m_game;
        private ViewPort m_viewPort;


        private Point m_nearestRegionPoint;

        private Form1 m_form;

        public Point MouseSource
        {
            get { return s_mouseSource; }
        }

        public Form1 Form
        {
            set { m_form = value; }
        }

        public ViewPort ViewPort
        {
            get { return m_viewPort; }
        }

        public CoordinateHandler(Game game)
        {
            m_game = game;
            m_viewPort = new ViewPort(0, 0, 10, 10);
        }

        public Point NearestRegionPoint
        {
            get { return m_nearestRegionPoint; }
        }
        
        public void SetCoordinate(int x, int y)
        {
            s_mouseSource = new Point(x, y);

            //find real source coordinates
            if (m_game.MapProvider.Map.Rectangle.X < 0)
                x = x + (-1 * m_game.MapProvider.Map.Rectangle.X);

            if (m_game.MapProvider.Map.Rectangle.Y < 0)
                y = y + (-1 * m_game.MapProvider.Map.Rectangle.Y);

            m_form.SetText("Pixel Coordinates x : " + x + " y : " + y);

            //bool checkCity = true;
            //if (checkCity)
            //{
            //    //resetCityPoint
            //    m_cityPoint = m_oslo;
            //    ////recalibrate Point relative to image 
            //    m_cityPoint = RecalibratePoint(m_cityPoint);
            //}

            Point[] twoNearestPoints = new Point[2];
            bool drawToRegion = true;
            if (drawToRegion)
            {
                if (m_game.GetQuestion().Answer.Location.GetType().Name == "Region")
                {

                //if not within region  ....  
                //if (!PointInPolygon(s_mouseSource,  m_region))
                if (!PointInPolygon(s_mouseSource, m_game.GetQuestion().Answer.Location.GetBaseType()))
                {
                    //draw line to nearest point from s_mouseSource
                    double distance;
                    double shortestDistance_first = GetDistance(s_mouseSource, RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[0]));
                    int indexOfShortestPoint_first = 0;
                    Point shortestPoint_first = RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[0]);
                    double shortestDistance_second = GetDistance(s_mouseSource, RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[1]));
                    int indexOfShortestPoint_second = 1;
                    Point shortestPoint_second = RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[1]);

                    for (int i = 1; i < m_game.GetQuestion().Answer.Location.GetBaseType().Length; i++)
                    {

                        distance = GetDistance(s_mouseSource, RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[i]));
                        if (distance < shortestDistance_first)
                        {
                            shortestDistance_first = distance;
                            indexOfShortestPoint_first = i;
                            shortestPoint_first = RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[i]);
                        }
                    }
                    //second nearest , must be either to the left or right of the shortest point

                    int newIndexDeclined = (indexOfShortestPoint_first - 1) % m_game.GetQuestion().Answer.Location.GetBaseType().Length;
                    if (newIndexDeclined < 0)
                        newIndexDeclined = (newIndexDeclined + m_game.GetQuestion().Answer.Location.GetBaseType().Length) % m_game.GetQuestion().Answer.Location.GetBaseType().Length;
                    if (GetDistance(s_mouseSource, RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[(indexOfShortestPoint_first + 1) % m_game.GetQuestion().Answer.Location.GetBaseType().Length])) <
                        GetDistance(s_mouseSource, RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[newIndexDeclined])))
                    {
                        shortestPoint_second = RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[(indexOfShortestPoint_first + 1) % m_game.GetQuestion().Answer.Location.GetBaseType().Length]);
                        indexOfShortestPoint_second = (indexOfShortestPoint_first + 1) % m_game.GetQuestion().Answer.Location.GetBaseType().Length;
                    }
                    else
                    {
                        shortestPoint_second = RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[newIndexDeclined]);
                        indexOfShortestPoint_second = newIndexDeclined;
                    }

                    Point originalShortestPoint = shortestPoint_first;

                    //find middle point
                    Point middlePoint = new Point();

                    for (int i = 0; i < 5; i++)
                        DevideAndFindNearerPoint(ref middlePoint, ref shortestPoint_first, ref shortestPoint_second);

                    if (originalShortestPoint == shortestPoint_first)
                    {
                        //test if second shortest index is higher
                        if ((indexOfShortestPoint_first - indexOfShortestPoint_second) < 0)
                        {
                            indexOfShortestPoint_second = indexOfShortestPoint_first - 1;
                            if (indexOfShortestPoint_second < 0)
                                indexOfShortestPoint_second = m_game.GetQuestion().Answer.Location.GetBaseType().Length - 1;
                        }
                        else
                        {
                            indexOfShortestPoint_second = indexOfShortestPoint_first + 1;
                            if (indexOfShortestPoint_second >= m_game.GetQuestion().Answer.Location.GetBaseType().Length)
                                indexOfShortestPoint_second = 0;
                        }

                        shortestPoint_second = RecalibratePoint(m_game.GetQuestion().Answer.Location.GetBaseType()[indexOfShortestPoint_second]);

                        for (int i = 0; i < 5; i++)
                            DevideAndFindNearerPoint(ref middlePoint, ref shortestPoint_first, ref shortestPoint_second);
                    }


                    if (GetDistance(s_mouseSource, shortestPoint_first) < GetDistance(s_mouseSource, shortestPoint_second))
                        m_nearestRegionPoint = shortestPoint_first;
                    else
                        m_nearestRegionPoint = shortestPoint_second;

                }
            }
            }

            m_form.DrawDirectionToRegion = true;
            m_form.DrawDirectionToCity = true;
            m_form.RefreshPanel();
        }


        public void DevideAndFindNearerPoint(ref Point middlePoint, ref Point shortestPoint_first, ref Point shortestPoint_second)
        {
            middlePoint.X = (shortestPoint_first.X + shortestPoint_second.X) / 2;
            middlePoint.Y = (shortestPoint_first.Y + shortestPoint_second.Y) / 2;

            if (GetDistance(s_mouseSource, shortestPoint_first) < GetDistance(s_mouseSource, middlePoint))
            {
                shortestPoint_second = middlePoint;
            }
            else
            {
                shortestPoint_second = shortestPoint_first;
                shortestPoint_first = middlePoint;
            }
        }

        public double GetDistance(PointF point1, PointF point2)
        {
            //pythagoras theorem c^2 = a^2 + b^2
            //thus c = square root(a^2 + b^2)
            double a = (double)(point2.X - point1.X);
            double b = (double)(point2.Y - point1.Y);

            return Math.Sqrt(a * a + b * b);
        }




        private bool IsWithinBounds()
        {
            bool withinbounds = false;

            if (m_viewPort.Rect.X >= m_game.MapProvider.Map.Rectangle.X && m_viewPort.Rect.Right <= m_game.MapProvider.Map.Rectangle.Right)
            {
                if (m_viewPort.Rect.Y >= m_game.MapProvider.Map.Rectangle.Y && m_viewPort.Rect.Bottom <= m_game.MapProvider.Map.Rectangle.Bottom)
                {
                    withinbounds = true;
                }
            }
            return withinbounds;
        }

        private bool PointInPolygon(Point p, Point[] poly)
        {
            poly = RecalibratePolygon(poly);

            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }
            Point oldPoint = new Point(
            poly[poly.Length - 1].X, poly[poly.Length - 1].Y);


            for (int i = 0; i < poly.Length; i++)
            {
                Point newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X) && ((long)p.Y - (long)p1.Y) * (long)(p2.X - p1.X) < ((long)p2.Y - (long)p1.Y) * (long)(p.X - p1.X))
                {
                    inside = !inside;
                }
                oldPoint = newPoint;
            }
            return inside;
        }


        public Point RecalibratePoint(Point p)
        {
            //p.X += m_game.MapProvider.Map.Rectangle.X;
            //p.Y += m_game.MapProvider.Map.Rectangle.Y;
            int mapRectx = m_game.MapProvider.Map.Rectangle.X;
            int mapRexty = m_game.MapProvider.Map.Rectangle.Y;
            int realPX = p.X  * m_game.MapProvider.Map.ZoomFactor.GetZoomFactor() / 100;
            int realPY = p.Y  * m_game.MapProvider.Map.ZoomFactor.GetZoomFactor() / 100;

            //p.X = p.X + mapRectx;
            //p.Y = p.Y + mapRexty;
            p.X = realPX + mapRectx;
            p.Y = realPY + mapRexty;
            return p;
        }

        public Point[] RecalibratePolygon(Point[] akershusPoly)
        {
            Point[] tempList = (Point[])akershusPoly.Clone();
            //for (int i = 0; i < tempList.Length; i++)
            //{
            //    tempList[i].X = tempList[i].X + m_game.MapProvider.Map.Rectangle.X;
            //    tempList[i].Y = tempList[i].Y + m_game.MapProvider.Map.Rectangle.Y;
            //}
            for (int i = 0; i < tempList.Length; i++)
            {
                tempList[i] = RecalibratePoint( tempList[i] );
            }
            return tempList;
        }

        public bool FarUp()
        {
            bool farUp = false;
            if (m_game.MapProvider.Map.Rectangle.Y >= m_viewPort.Rect.Y)
                farUp = true;
            return farUp;
        }

        public bool FarDown()
        {
            bool farDown = false;
            if (m_game.MapProvider.Map.Rectangle.Bottom <= m_viewPort.Rect.Bottom)
                farDown = true;
            return farDown;
        }

        public bool FarLeft()
        {
            bool farLeft = false;
            if (m_game.MapProvider.Map.Rectangle.Right <= m_viewPort.Rect.Right)
                farLeft = true;
            return farLeft;
        }

        public bool FarRight()
        {
            bool farRight = false;
            if (m_game.MapProvider.Map.Rectangle.Left >= m_viewPort.Rect.Left)
                farRight = true;
            return farRight;
        }
    }
}
