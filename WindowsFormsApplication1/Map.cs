using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class Map
    {
        private string m_name;
        private Image m_image;
        private Rectangle m_rectangle;
        private ZoomFactor m_zoomFactor;
        private int m_orgHeight;
        private int m_orgWidth;

        public Rectangle Rectangle
        {
            get { return m_rectangle; }
            set { m_rectangle = value; }
        }
        public ZoomFactor ZoomFactor
        {
            get { return m_zoomFactor; }
        }
        public int RectX
        {
            set { m_rectangle.X = value; }
        }
        public int RectY
        {
            set { m_rectangle.Y = value; }
        }
        public int RectHeight
        {
            set { m_rectangle.Height = value; }
            get { return m_rectangle.Height; }
        }
        public int RectWidth
        {
            set { m_rectangle.Width = value; }
            get { return m_rectangle.Width; }
        }

        public Image Image
        {
            get { return m_image; }
        }

        public Map(string name, string imagePath)
        {
            m_name = name;
            m_image = Image.FromFile(imagePath);
            m_zoomFactor = new ZoomFactor();
            m_orgHeight = m_image.Height;
            m_orgWidth = m_image.Width;
            m_rectangle = new Rectangle(0, 0, m_image.Width, m_image.Height);
        }

        public void Offset(int x, int y)
        {
            m_rectangle.Offset(x, y);
        }

        public void ZoomIn()
        {
            m_zoomFactor.IncreaseZoom();
            m_rectangle.Height = m_orgHeight * m_zoomFactor.GetZoomFactor() / 100;
            m_rectangle.Width = m_orgWidth * m_zoomFactor.GetZoomFactor() / 100;
        }

        public void ZoomOut()
        {
            m_zoomFactor.DecreaseZoom();
            m_rectangle.Height = m_orgHeight * m_zoomFactor.GetZoomFactor() / 100;
            m_rectangle.Width = m_orgWidth * m_zoomFactor.GetZoomFactor() / 100;
        }
    }
}
