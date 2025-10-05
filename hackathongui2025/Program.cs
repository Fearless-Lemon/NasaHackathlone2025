using System;
using System.Windows.Forms;

namespace Hackathlone2025
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); // start the GUI
        }
    }
}
