using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Game
    {
        private List<Player> m_players;
        private Player m_nextTurn;
        private List<Question> m_questions;
        private Difficulty m_difficulty;
        private Type m_type;
        private MapProvider m_mapProvider;
        private int m_currentQuestionIndex = 0;
        

        public MapProvider MapProvider
        {
            get { return m_mapProvider; }
        }

        public enum Difficulty
        {
            easy = 1,
            medium = 2,
            hard = 3
        }

        public enum Type
        {
            none =1 
        }

        public enum Language
        {
            English = 1,
            Norwegian = 2
        }

        public Game(List<Player> players, Difficulty difficulty, Type type, MapProvider mapProvider)
        {
            m_players = players;
            m_difficulty = difficulty;
            m_type = type;
            m_mapProvider = mapProvider;
        }

        public void InitQuestions()
        {
            m_questions = new List<Question>();
            Question quest;
            foreach (Location loc in m_mapProvider.GetLocations())
            {
                quest = new Question(loc.Name,Language.Norwegian,loc);
                m_questions.Add(quest);
            }
        }

        public Question GetQuestion()
        {
            return (Question)m_questions[m_currentQuestionIndex];
        }

        public void SetNextQuestion()
        {
            m_currentQuestionIndex = (m_currentQuestionIndex + 1) % m_questions.Count;
        }

        public void LoadGame()
        {
            InitQuestions();   
        }
    }
}
