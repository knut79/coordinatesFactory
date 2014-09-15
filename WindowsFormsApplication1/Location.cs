using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public abstract class Location
    {
        private string m_name;
        private string m_fylke;
        private LocationType m_type;

        public string Name
        {
            get { return m_name; }
        }

        public string Fylke
        {
            get { return m_fylke; }
        }

        public LocationType LocationTypeValue
        {
            get { return m_type; }
        }

        public enum LocationType
        {
            City = 1,
            Region = 2
        }


        public Location(string name, string fylke)
        {
            switch (GetType().Name)
            {
                case ("City"):
                    m_type = LocationType.City;
                    break;
                case ("Region"):
                    m_type = LocationType.Region;
                    break;
                default:
                    m_type = LocationType.City;
                    break;
            }

            m_name = name;
            m_fylke = fylke;
        }

        public abstract Point[] GetBaseType();

    }
}
