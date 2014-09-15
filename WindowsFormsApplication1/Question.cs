using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace WindowsFormsApplication1
{
    //can be 
    public class Question
    {
        private string m_question;
        private Answer m_answer;
        private Location.LocationType m_type; //point ; region
        private string m_name;

        public Answer Answer
        {
            get { return m_answer; }
        }

        public string QuestionString
        {
            get
            {
                return m_question;
            }
        }

        public Question(string name, Game.Language language, Location loc)
        {
            m_name = name;
            m_type = loc.LocationTypeValue;



            m_answer = new Answer(name, loc);
            

            string firstPart, secondPart, thirdPart;
            switch (m_type)
            {
                case(Location.LocationType.City):
                    switch (language)
                    {
                        case(Game.Language.Norwegian):
                            secondPart = "byen";
                            break;
                        case(Game.Language.English):
                            secondPart = "the city";
                            break;
                        default:
                            secondPart = "the city";
                            break;
                    }
                    break;
                case(Location.LocationType.Region):
                    switch (language)
                    {
                        case(Game.Language.Norwegian):
                            secondPart = "området";
                            break;
                        case(Game.Language.English):
                            secondPart = "the region";
                            break;
                        default:
                            secondPart = "the region";
                            break;
                    }
                    break;
                default :
                    secondPart = "the city";
                    break;
            }

            switch (language)
            {
                case(Game.Language.Norwegian):
                    thirdPart = "";
                    break;
                case(Game.Language.English):
                    thirdPart = "located";
                    break;
                default:
                    thirdPart = "located";
                    break;
            }

            switch (language)
            {
                case (Game.Language.Norwegian):
                    firstPart = "Hvor er";
                    break;
                case (Game.Language.English):
                    firstPart = "Where is";
                    break;
                default:
                    firstPart = "Where is";
                    break;
            }

            m_question = string.Format("{0} {1} {2} {3}", firstPart, secondPart, m_name, thirdPart);
        }
    }
}
