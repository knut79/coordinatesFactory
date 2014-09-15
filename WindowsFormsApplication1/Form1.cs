using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        private bool m_startWriteCoordinates;
        private StreamWriter m_coordinatesFile;

        private bool mouseDownLeft = false;
        private bool mouseDownRight = false;

        private int drawPosX = 0;
        private int drawPosY = 0;
        private int m_lastMouseX;
        private int m_lastMouseY;

        private bool m_drawDirectionToCity = false;
        private bool m_drawDirectionToRegion = false;
        public bool DrawDirectionToCity
        {
            set { m_drawDirectionToCity = value; }
        }
        public bool DrawDirectionToRegion
        {
            set { m_drawDirectionToRegion = value; }
        }

        private bool m_paintMode = false;
        private List<Point> m_paintRegion;


        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth,
           int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern bool DeleteObject(IntPtr hObject);

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062
        }

        private GameHandler m_gh;
        public Form1(GameHandler gh)
        {
            

            InitializeComponent();
            m_gh = gh;
            m_gh.CH.Form = this;
            m_gh.CH.ViewPort.Rect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            m_paintRegion = new List<Point>();
        }

     
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Invalidate();
            //panel1.Refresh();
            //Graphics g = e.Graphics;

            //g.DrawImage(m_img, drawPosX, drawPosY );

            //IntPtr pTarget = e.Graphics.GetHdc();
            //IntPtr pSource = CreateCompatibleDC(pTarget);
            //IntPtr pOrig = SelectObject(pSource, m_bmp.GetHbitmap());
            //BitBlt(pTarget, drawPosX, drawPosY, m_bmp.Width, m_bmp.Height, pSource, 0, 0, TernaryRasterOperations.SRCCOPY);
            //IntPtr pNew = SelectObject(pSource, pOrig);
            //DeleteObject(pNew);
            //DeleteDC(pSource);
            //e.Graphics.ReleaseHdc(pTarget);
        }

        static SolidBrush B2 = new SolidBrush(Color.Blue);
        static SolidBrush B1 = new SolidBrush(Color.Red);
        static SolidBrush B3 = new SolidBrush(Color.Green);
        Pen P1 = new Pen(B1, 5);
        Pen P2 = new Pen(B2, 5);
        Pen P3 = new Pen(B3, 5);

        private void Panel1_paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

            //g.DrawImage(m_img, new RectangleF(drawPosX, drawPosY, panel1.Width, panel1.Height), (RectangleF)panel1.ClientRectangle, GraphicsUnit.Pixel);

            textBoxToFile.AppendText(string.Format("DEBUG: {0},{1},{2}", m_gh.Game.MapProvider.Map.Rectangle.X,m_gh.Game.MapProvider.Map.Rectangle.Y, Environment.NewLine));

            //Rectangle rect = new Rectangle(m_gh.Game.MapProvider.Map.Rectangle.X > 0 ? 0 : m_gh.Game.MapProvider.Map.Rectangle.X,
            //    m_gh.Game.MapProvider.Map.Rectangle.Y > 0 ? 0 : m_gh.Game.MapProvider.Map.Rectangle.Y,
            //    m_gh.Game.MapProvider.Map.Rectangle.Width, m_gh.Game.MapProvider.Map.Rectangle.Height);
            e.Graphics.DrawImage(m_gh.Game.MapProvider.Map.Image, m_gh.Game.MapProvider.Map.Rectangle);

            Location regionOrCity = m_gh.Game.GetQuestion().Answer.Location;

            //_?
            if (m_paintMode)
            {
                if (m_paintRegion.Count > 1)
                {

                    Point[] tempPolygon = m_gh.CH.RecalibratePolygon(m_paintRegion.ToArray());

                    e.Graphics.DrawLines(P2, tempPolygon);

                }
            }

            if (regionOrCity.GetType().Name == "City")
            {
                //draw against city
                if (m_drawDirectionToCity)
                {
                    m_drawDirectionToCity = false;
                    e.Graphics.DrawLine(P1, m_gh.CH.MouseSource, m_gh.CH.RecalibratePoint(regionOrCity.GetBaseType()[0]));
                }
            }
            if(regionOrCity.GetType().Name == "Region")
            {
                
                Point[] tempPolygon = m_gh.CH.RecalibratePolygon(regionOrCity.GetBaseType());
                e.Graphics.DrawPolygon(P1, tempPolygon);

                if (m_drawDirectionToRegion)
                {
                    e.Graphics.DrawLine(P2, m_gh.CH.MouseSource, m_gh.CH.NearestRegionPoint);
                    m_drawDirectionToRegion = false;
                }
            }
        }


        private void Panel1_mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLeft = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouseDownRight = true;
                 if (m_startWriteCoordinates)
                {

                    int tempX = e.X;
                    int tempY = e.Y;

                    if (m_gh.Game.MapProvider.Map.Rectangle.X < 0)
                        tempX = tempX + (-1 * m_gh.Game.MapProvider.Map.Rectangle.X);

                    if (m_gh.Game.MapProvider.Map.Rectangle.Y < 0)
                        tempY = tempY + (-1 * m_gh.Game.MapProvider.Map.Rectangle.Y);

                    textBoxToFile.AppendText(string.Format("{0},{1}{2}",tempX,tempY,Environment.NewLine));
                    m_coordinatesFile.WriteLine(tempX + "," + tempY);

                    if (m_paintMode)
                    {
                        //m_gh.CH.RecalibratePoint(
                        m_paintRegion.Add(new Point(tempX, tempY));
                        //m_paintRegion.Add(new Point(e.X, e.Y));

                        panel1.Refresh();
                    }

                }
                else
                {
                    m_gh.CH.SetCoordinate(e.X, e.Y);
                }

                 
            }

            
        }

        private void Panel1_mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLeft = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                mouseDownRight = false;
            }
            
        }

        private void Panel1_mouseMove(object sender, MouseEventArgs e)
        {
            //int drawFactor = 20;;
            int drawFactor = 10 * m_gh.Game.MapProvider.Map.ZoomFactor.GetZoomFactor() / 100;

            if (mouseDownLeft)
            {
                int vXFactor = 0;
                int vYFactor = 0;
                bool up = false;
                bool down = false;
                bool right = false;
                bool left = false;

                if (e.X > m_lastMouseX)
                {
                    vXFactor = 1;
                    right = true;
                }
                if (e.X < m_lastMouseX)
                {
                    vXFactor = -1;
                    left = true;
                }
                if (e.Y > m_lastMouseY)
                {
                    vYFactor = 1;
                    up = true;
                }

                if (e.Y < m_lastMouseY)
                {
                    vYFactor = -1;
                    down = true;
                }

                //update x and y
                m_lastMouseX = e.X;
                m_lastMouseY = e.Y;

                //find out what ways to draw picture up,down,left ,right or combinations down/left , down/right , up/left, up/right
                if (right)
                {
                    if (m_gh.CH.FarRight())
                    {
                        //restore mainRect X
                        m_gh.Game.MapProvider.Map.RectX = m_gh.Game.MapProvider.Map.Rectangle.X;
                        right = false;
                    }
                    else
                    {
                        textBoxToFile.AppendText(string.Format("offset right {0},{1}{2}", vXFactor * drawFactor, 0, Environment.NewLine));
                        m_gh.Game.MapProvider.Map.Offset(vXFactor * drawFactor, 0);
                    }
                }

                if (left)
                {
                    if (m_gh.CH.FarLeft())
                    {
                        //m_gh.Game.MapProvider.Map.RectX = m_gh.Game.MapProvider.Map.Rectangle.X;
                        m_gh.Game.MapProvider.Map.RectX = 0;
                        right = false;
                    }
                    else
                    {

                        m_gh.Game.MapProvider.Map.Offset(vXFactor * drawFactor, 0);
                        textBoxToFile.AppendText(string.Format("offset left {0},{1}{2}", vXFactor * drawFactor, 0, Environment.NewLine));
                    }
                }

                if (up)
                {
                    if (m_gh.CH.FarUp())
                    {
                        //m_gh.Game.MapProvider.Map.RectY = m_gh.Game.MapProvider.Map.Rectangle.Y;
                        m_gh.Game.MapProvider.Map.RectY = 0;
                        up = false;
                    }
                    else
                    {
                        textBoxToFile.AppendText(string.Format("offset up {0},{1} vYFactor: {2} drawFactor {3} {4}", 0, vYFactor * drawFactor, vYFactor, drawFactor, Environment.NewLine));
                        m_gh.Game.MapProvider.Map.Offset(0, vYFactor * drawFactor);
                    }
                }

                if (down)
                {
                    if (m_gh.CH.FarDown())
                    {
                        m_gh.Game.MapProvider.Map.RectY = m_gh.Game.MapProvider.Map.Rectangle.Y;
                        up = false;
                    }
                    else
                    {
                        textBoxToFile.AppendText(string.Format("offset down {0},{1} vYFactor: {2} drawFactor {3} {4}", 0, vYFactor * drawFactor,vYFactor,drawFactor, Environment.NewLine));
                        m_gh.Game.MapProvider.Map.Offset(0, vYFactor * drawFactor);
                    }

                }
                Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_paintMode)
            {
                this.button1.Text = "Paint mode on";
                m_paintMode = true;
            }
            else
            {
                this.button1.Text = "Paint mode off";
                m_paintRegion.Clear();
                m_paintMode = false;
            }
        }

        public void RefreshPanel()
        {
            panel1.Refresh();
        }

        public int GetPanelWidth()
        {
            return panel1.Width;
        }

        public int GetPanelHeight()
        {
            return panel1.Height;
        }

        private void btn_nextQuestion_Click(object sender, EventArgs e)
        {
            m_gh.Game.SetNextQuestion();

            textBox_question.Text = m_gh.Game.GetQuestion().QuestionString;
        }

        private void btn_zoomPlus_Click(object sender, EventArgs e)
        {
            m_gh.Game.MapProvider.Map.ZoomIn();

        }

        private void btn_zoomMinus_Click(object sender, EventArgs e)
        {
            m_gh.Game.MapProvider.Map.ZoomOut();
        }

        private void btn_startWrite_Click(object sender, EventArgs e)
        {
            if (m_startWriteCoordinates)
            {
                m_startWriteCoordinates = false;
                btn_startWrite.Text = "start writing";
                m_coordinatesFile.Close();
                

            }
            else
            {
                m_startWriteCoordinates = true;
                btn_startWrite.Text = "stop writing";
                var path = System.Reflection.Assembly.GetEntryAssembly().Location + @"\..\" + "test.txt";
                textBoxToFile.AppendText(string.Format("Start writing to {0}{1}",path,Environment.NewLine));
                m_coordinatesFile = new StreamWriter(path, true);

            }
        }

        private void btn_setLbl_Click(object sender, EventArgs e)
        {
            if (m_startWriteCoordinates)
            {
                textBoxToFile.AppendText(string.Format(textBox_lbl.Text));
                m_coordinatesFile.WriteLine(string.Format("{0}{1}",textBox_lbl.Text,Environment.NewLine));
                textBox_lbl.Text = string.Empty;
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (m_paintRegion.Count > 0)
            {
                var indexedValue = m_paintRegion[m_paintRegion.Count - 1];
                if (m_coordinatesFile != null)
                {
                    textBoxToFile.AppendText(string.Format("!!!{0},{1} --should be removed{2}", indexedValue.X, indexedValue.Y, Environment.NewLine));
                    m_coordinatesFile.WriteLine(string.Format("!!!{0},{1} --should be removed{2}", indexedValue.X, indexedValue.Y, Environment.NewLine));
                    m_paintRegion.RemoveAt(m_paintRegion.Count - 1);
                }
            }
        }

    }
}
