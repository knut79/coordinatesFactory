using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace WindowsFormsApplication1
{
    public class MapProvider
    {
        //public readonly static MapProvider Instance = new MapProvider();
        private List<Location> m_locationsList;
        private Map m_map;

        public Map Map
        {
            get { return m_map; }
        }


        delegate void InitLocations();
        public enum MapName
        {
            Norway = 1,
            USA = 2
        }

        public MapProvider(MapName p_mapName)
        {
            InitLocations d_InitMapLocations;
            switch(p_mapName)
            {
                case(MapName.Norway):
                    m_map = new Map("Norge", @"C:\Users\knut\Desktop\whatIsThis_TEMP\borders.png");
                    d_InitMapLocations = InitNorwegianLocations;
                    break;
                default:
                    m_map = new Map("Norge", @"C:\Users\knut\Desktop\whatIsThis_TEMP\norge2.png");
                    d_InitMapLocations = InitNorwegianLocations;
                    break;

            }

            d_InitMapLocations();
            
        }

        private void InitNorwegianLocations()
        {
            m_locationsList = new List<Location>();

            List<Point> region = new List<Point>();
            //Uri baseUri = new Uri(System.Reflection.Assembly.GetEntryAssembly().Location);
            var path = System.Reflection.Assembly.GetEntryAssembly().Location + @"\..\" + "Regions2.txt";
            using (StreamReader tr = new StreamReader(path))
            {
                string input = string.Empty;
                string[] data;
                string regionName, cityName; 
                int x, y;
                while ((input = tr.ReadLine()) != null)
                {
                    data = input.Split(',');
                    if (data[0].ToString() == "Region")
                    {
                        regionName = data[1].ToString();
                        m_locationsList.Add(new Region(regionName, string.Empty, region.ToArray()));
                        region.Clear();
                    }
                    else if (data[0].ToString() == "Point")
                    {
                        cityName = data[1].ToString();
                        m_locationsList.Add(new City(cityName, string.Empty, string.Empty, region.ToArray()[0]));
                        region.Clear();
                    }
                    else
                    {
                        int.TryParse(data[0],out x);
                        int.TryParse(data[1],out y);
                        region.Add(new Point(x, y));
                    }
                }
                tr.Close();
            }
            




            //using (StreamReader tr = new StreamReader(@"C:\Projects\iphoneApp\Points.txt"))
            //{
            //    string input = string.Empty;
            //    string[] data;
            //    string cityName;
            //    int x, y;
            //    while ((input = tr.ReadLine()) != null)
            //    {
            //        data = input.Split(',');
            //        if (data[0].ToString() == "Point")
            //        {
            //            cityName = data[1].ToString();
            //            m_locationsList.Add(new City(cityName, string.Empty, string.Empty, new Point(423, 1747)));
            //        }
            //        else
            //        {
            //            int.TryParse(data[0], out x);
            //            int.TryParse(data[1], out y);
            //            region.Add(new Point(x, y));
            //        }
            //    }
            //    tr.Close();
            //}



        }

        public List<Location> GetLocations()
        {
            return m_locationsList;
        }

    }
}
