using System;
using System.Reflection;
using System.Windows.Forms;

namespace CSFormat
{
    partial class CSAbout : Form
    {
        public CSAbout()
        {
            InitializeComponent();
            this.Text = $"Acerca de {AssemblyTitle}";
            this.labelProductName.Text = AssemblyTitle;
            this.labelVersion.Text = "Versión 1.0.0";
            this.labelAuthor.Text = $"© {DateTime.Now.Year} Luis Oyanader C.";
        }

        #region Descriptores de acceso de atributos de ensamblado

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0 && attributes[0] is AssemblyTitleAttribute titleAttribute && !string.IsNullOrEmpty(titleAttribute.Title))
                {
                    return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return version?.ToString() ?? "1.0.0.0";
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0 || attributes[0] is not System.Reflection.AssemblyCompanyAttribute companyAttribute)
                {
                    return "Autor desconocido";
                }
                return companyAttribute.Company ?? "Autor desconocido";
            }
        }
        #endregion

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
