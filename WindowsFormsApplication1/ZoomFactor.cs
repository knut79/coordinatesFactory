using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class ZoomFactor
    {
        private int m_zoom;
        private const int m_maxZoom = 4;

        public ZoomFactor()
        {
            m_zoom = 0;
        }

        public void IncreaseZoom()
        {
            if(m_zoom + 1 <= m_maxZoom)
                m_zoom++;
        }

        public void DecreaseZoom()
        {
            if (m_zoom - 1 >= 0)
                m_zoom--;
        }

        public int GetZoomFactor()
        {
            int zoomFactor;
            switch (m_zoom)
            {
                case(0):
                    zoomFactor = 100;
                    break;
                case(1):
                    zoomFactor = 110;
                    break;
                case(2):
                    zoomFactor = 150;
                    break;
                case(3):
                    zoomFactor = 170;
                    break;
                case(4):
                    zoomFactor = 200;
                    break;
                default:
                    zoomFactor = 100;
                    break;
            }

            return zoomFactor;
        }


    }
}
