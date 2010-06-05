/****************************************************/
// Filename: Program.cs
// Created: Chris Fay
// Change history:
// 5.1.2009 / Chris Fay
// 5.5.2010 / Chris Fay (cleanup)
/****************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EnvatoTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EnvatoTrackerFrame());
        }
    }
}