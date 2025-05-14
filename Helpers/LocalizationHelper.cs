using System.Globalization;
using System.Resources;
using System.Threading;
using System.Reflection;
using System;

namespace CSFormat.Helpers
{
    public static class LocalizationHelper
    {
        private static ResourceManager? _resourceManager;
        
        public static event EventHandler? LanguageChanged;

        public static void SetLanguage(string languageCode)
        {
            try
            {
                var culture = new CultureInfo(languageCode);
                Thread.CurrentThread.CurrentUICulture = culture;
                Thread.CurrentThread.CurrentCulture = culture;
                
                // Forzar la recarga del ResourceManager
                _resourceManager = null;
                
                // Guardar la preferencia de idioma
                AppSettings.SaveLanguage(languageCode);
                
                // Disparar el evento de cambio de idioma
                LanguageChanged?.Invoke(null, EventArgs.Empty);
            }
            catch (CultureNotFoundException)
            {
                // Si el idioma no es válido, usar el idioma por defecto
                var defaultCulture = new CultureInfo("es-ES");
                Thread.CurrentThread.CurrentUICulture = defaultCulture;
                Thread.CurrentThread.CurrentCulture = defaultCulture;
                _resourceManager = null;
                AppSettings.SaveLanguage("es-ES");
                LanguageChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static string GetString(string key)
        {
            _resourceManager ??= new ResourceManager("CSFormat.Resources.Resources", 
                Assembly.GetExecutingAssembly());
                
            try
            {
                return _resourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture) ?? key;
            }
            catch
            {
                return key;
            }
        }
    }
}
