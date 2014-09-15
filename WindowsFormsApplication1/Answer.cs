using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Answer
    {
        private string m_name;
        private string m_answerString;
        private Location m_location;

        public Location Location
        {
            get { return m_location; }
        }

        public Answer(string name, Location location)
        {
            m_name = name;
            m_location = location;
       }
    }
}
