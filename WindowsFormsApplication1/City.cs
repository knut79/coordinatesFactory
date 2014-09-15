using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class City: Location
    {
        private string m_kommune;
        private Point m_point;


        public City(string name, string kommune, string fylke, Point point)
            :base(name, fylke)
        {
            m_kommune = kommune;
            m_point = point;
        }

        public override Point[] GetBaseType()
        {
            return new Point[]{ m_point };
            //throw new NotImplementedException();
        }
    }
}
