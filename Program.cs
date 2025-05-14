using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CSFormat.Helpers;

namespace CSFormat
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Cargar la configuración guardada (incluyendo el idioma)
            AppSettings.Load();
            
            // Si no hay configuración guardada, usar español por defecto
            if (string.IsNullOrEmpty(AppSettings.Language))
            {
                var culture = new CultureInfo("es-ES");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            
            InitializeApplication();
            RunApplication();
        }

        private static void InitializeApplication()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
        }

        private static void RunApplication()
        {
            using var mainForm = new CSFormat();
            Application.Run(mainForm);
        }
    }
}