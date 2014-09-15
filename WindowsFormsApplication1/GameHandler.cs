using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class GameHandler
    {
        private Game m_game;
        private CoordinateHandler m_ch;
        private Form1 m_form;

        public Game Game
        {
            get { return m_game; }
        }
        public CoordinateHandler CH
        {
            get { return m_ch; }
        }

        
        public GameHandler()
        {
            InitGame(); 
        }

        public void InitGame()
        {
            MapProvider mapProvider = new MapProvider(MapProvider.MapName.Norway);

            List<Player> players = new List<Player>();
            Player playerOne = new Player("knut");
            Player playerTwo = new Player("kari");
            players.Add(playerOne);
            players.Add(playerTwo);

            m_game = new Game(players, Game.Difficulty.easy, Game.Type.none, mapProvider);
            m_game.LoadGame();

            m_ch = new CoordinateHandler(m_game);
        }
    }
}
