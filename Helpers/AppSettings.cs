using System;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace CSFormat.Helpers
{
    public static class AppSettings
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CSFormat",
            "settings.json");

        public static string Language { get; private set; } = "es-ES"; // Valor por defecto

        public static void Load()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    var json = File.ReadAllText(SettingsPath);
                    var settings = JsonSerializer.Deserialize<SettingsModel>(json);
                    if (settings != null && !string.IsNullOrEmpty(settings.Language))
                    {
                        Language = settings.Language;
                        // Aplicar el idioma cargado
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Language);
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Language);
                    }
                }
            }
            catch (Exception)
            {
                // Si hay algún error, se usa el idioma por defecto
            }
        }

        public static void SaveLanguage(string languageCode)
        {
            try
            {
                Language = languageCode;
                var settings = new SettingsModel { Language = languageCode };
                var json = JsonSerializer.Serialize(settings);
                
                // Asegurarse de que el directorio existe
                var directory = Path.GetDirectoryName(SettingsPath);
                if (directory == null)
                {
                    throw new InvalidOperationException("No se pudo determinar el directorio de configuración.");
                }
                
                if (!Directory.Exists(directory))
                {
                    _ = Directory.CreateDirectory(directory);
                }
                
                File.WriteAllText(SettingsPath, json);
            }
            catch (Exception)
            {
                // Manejar el error según sea necesario
            }
        }

        private class SettingsModel
        {
            public string Language { get; set; } = string.Empty;
        }
    }
}
