namespace CSFormat
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
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