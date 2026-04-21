using System;
using System.Windows.Forms;
using pac_man;

static class Program {
    [STAThread]
    static void Main() {
        Database.Initialize(); 
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}