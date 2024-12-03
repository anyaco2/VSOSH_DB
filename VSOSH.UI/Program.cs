using System;
using System.Windows.Forms;

namespace VSOSH.UI;

internal static class Program
{
    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainWindow());
    }
}