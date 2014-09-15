using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class ViewPort
    {
        private Rectangle m_rectangle;

        public Rectangle Rect
        {
            get { return m_rectangle; }
            set { m_rectangle = value; }
        }

        public ViewPort(int x, int y, int width, int height)
        {
            m_rectangle = new Rectangle(x, y, width, height);
        }
    }
}
