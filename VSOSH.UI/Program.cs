using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace VSOSH.UI;

internal static class Program
{
    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        // Создаем провайдер сервисов
        var serviceProvider = ServiceProviderFactory.CreateServiceProvider();
        
        // Получаем главную форму через DI
        var mainForm = serviceProvider.GetRequiredService<MainWindow>();
        Application.Run(mainForm);
    }
}