using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //init game
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameHandler gh = new GameHandler();

            
            Form1 v_form = new Form1(gh);
            Application.Run(v_form);
        }
    }
}
