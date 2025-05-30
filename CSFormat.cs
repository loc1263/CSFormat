using System;
using System.IO;
using System.Windows.Forms;
using CSFormat.Helpers;
using System.Globalization;

namespace CSFormat
{
    public partial class CSFormat : Form
    {
        public CSFormat()
        {
            InitializeComponent();
            InitializeLocalization();
            WireUpEvents();
        }

        private void InitializeLocalization()
        {
            // Suscribirse al evento de cambio de idioma
            LocalizationHelper.LanguageChanged += (s, e) => UpdateUI();
            
            // Cargar la interfaz de usuario con el idioma actual
            UpdateUI();
        }

        private void WireUpEvents()
        {
            btnAbrirFormato.Click += BtnAbrirFormato_Click;
            btnAbrirInput.Click += BtnAbrirInput_Click;
            btnCerrar.Click += BtnCerrar_Click;
            btnProcesar.Click += BtnProcesar_Click;
        }

        private void BtnAbrirFormato_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxFormato.Text = openFileDialog.FileName;
            }
        }

        private void BtnAbrirInput_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxEntrada.Text = openFileDialog.FileName;
            }
        }

        private void BtnCerrar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private (bool IsValid, string? ErrorMessage) ValidateInputs(string? formato, string? entrada, string? separador)
        {
            return (formato, entrada, separador) switch
            {
                (null or "" or { Length: 0 }, _, _) =>
                    (false, "Por favor seleccione un archivo de formato."),
                (var f, _, _) when !File.Exists(f) =>
                    (false, "El archivo de formato no existe."),
                (_, null or "" or { Length: 0 }, _) =>
                    (false, "Por favor seleccione un archivo de entrada."),
                (_, var i, _) when !File.Exists(i) =>
                    (false, "El archivo de entrada no existe."),
                (_, _, null or "" or { Length: 0 }) =>
                    (false, "Por favor ingrese un carácter separador."),
                _ => (true, string.Empty)
            };
        }

        private void UpdateProgressBar(int percent, ProgressBar progressBar)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() =>
                    progressBar.Value = Math.Min(percent, progressBar.Maximum)));
            }
            else
            {
                progressBar.Value = Math.Min(percent, progressBar.Maximum);
            }
        }

        private void SetProgressBarVisibility(ProgressBar progressBar, bool isVisible)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() => progressBar.Visible = isVisible));
            }
            else
            {
                progressBar.Visible = isVisible;
            }
        }

        private void BtnProcesar_Click(object? sender, EventArgs e)
        {
            try
            {
                var (formato, entrada, separador) = (textBoxFormato.Text, textBoxEntrada.Text, textBoxSeparador.Text);

                var validation = ValidateInputs(formato, entrada, separador);
                if (!validation.IsValid)
                {
                    MessageBox.Show(validation.ErrorMessage, "Error de validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                progressBarProceso.Visible = true;
                progressBarProceso.Style = ProgressBarStyle.Marquee;

                try
                {
                    var progress = new Progress<int>(percent =>
                        UpdateProgressBar(percent, progressBarProceso));

                    ProcessFileWithProgress(progress, formato!, entrada!, separador!);
                }
                finally
                {
                    SetProgressBarVisibility(progressBarProceso, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los archivos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessFileWithProgress(IProgress<int> progress, string formato, string entrada, string separador)
        {
            var fileProcessor = new FileProcessor(progress);

            string outputFile = fileProcessor.ProcessFile(
                formatFilePath: formato,
                inputFilePath: entrada,
                separator: separador,
                hasTitles: chkTitulos.Checked);

            MessageBox.Show($"Archivo procesado correctamente.\nGuardado en: {outputFile}",
                "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CSAbout aboutBox = new CSAbout();
            aboutBox.ShowDialog();
        }

        private void UpdateUI()
        {
            // Actualizar textos del formulario
            this.Text = LocalizationHelper.GetString("AppTitle");
            lblFormato.Text = LocalizationHelper.GetString("SelectFormatFile");
            lbEntrada.Text = LocalizationHelper.GetString("SelectInputFile");
            lblSeparador.Text = LocalizationHelper.GetString("SeparatorLabel");
            chkTitulos.Text = LocalizationHelper.GetString("HasHeaders");
            btnProcesar.Text = LocalizationHelper.GetString("ProcessButton");
            btnCerrar.Text = LocalizationHelper.GetString("CloseButton");
            btnAbrirFormato.Text = LocalizationHelper.GetString("OpenButton") + "..";
            btnAbrirInput.Text = LocalizationHelper.GetString("OpenButton") + "..";
            
            // Actualizar menús
            configuracionToolStripMenuItem.Text = LocalizationHelper.GetString("MenuSettings");
            idiomaToolStripMenuItem1.Text = LocalizationHelper.GetString("MenuLanguage");
            espanolToolStripMenuItem1.Text = LocalizationHelper.GetString("LanguageSpanish");
            inglesToolStripMenuItem1.Text = LocalizationHelper.GetString("LanguageEnglish");
            acercaToolStripMenuItem.Text = LocalizationHelper.GetString("MenuAbout");
            
            // Actualizar estado de los checkboxes de idioma
            espanolToolStripMenuItem1.Checked = Thread.CurrentThread.CurrentUICulture.Name == "es-ES";
            inglesToolStripMenuItem1.Checked = Thread.CurrentThread.CurrentUICulture.Name == "en-US";
        }

        private void espanolToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LocalizationHelper.SetLanguage("es-ES");
        }

        private void inglesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LocalizationHelper.SetLanguage("en-US");
        }
    }
}
