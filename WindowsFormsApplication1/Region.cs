using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    //representerer fylke eller kommune
    class Region: Location 
    {
        private Point[] m_polygon;

        public Region(string name, string fylke, Point[] polygon)
            :base(name,fylke)
        {
            m_polygon = polygon;
        }

        public override Point[] GetBaseType()
        {
            return m_polygon;
        }


    }
}
