using System;
using System.IO;
using System.Windows.Forms;

namespace CSFormat
{
    public partial class CSFormat : Form
    {
        public CSFormat()
        {
            InitializeComponent();
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

        private void BtnProcesar_Click(object? sender, EventArgs e)
        {
            try
            {
                var (formato, entrada, separador) = (textBoxFormato.Text, textBoxEntrada.Text, textBoxSeparador.Text);
                
                var (isValid, errorMessage) = (formato, entrada, separador) switch
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

                if (!isValid)
                {
                    MessageBox.Show(errorMessage, "Error de validación", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                progressBarProceso.Visible = true;
                progressBarProceso.Style = ProgressBarStyle.Marquee;

                try
                {
                    var progress = new Progress<int>(percent =>
                    {
                        if (progressBarProceso.InvokeRequired)
                        {
                            progressBarProceso.Invoke(new Action(() => 
                                progressBarProceso.Value = Math.Min(percent, progressBarProceso.Maximum)));
                        }
                        else
                        {
                            progressBarProceso.Value = Math.Min(percent, progressBarProceso.Maximum);
                        }
                    });

                    var fileProcessor = new FileProcessor(progress);
                    
                    // Asegurarse de que los valores no sean nulos
                    string nonNullFormato = formato ?? throw new ArgumentNullException(nameof(formato));
                    string nonNullEntrada = entrada ?? throw new ArgumentNullException(nameof(entrada));
                    string nonNullSeparador = separador ?? throw new ArgumentNullException(nameof(separador));
                    
                    string outputFile = fileProcessor.ProcessFile(
                        formatFilePath: nonNullFormato,
                        inputFilePath: nonNullEntrada,
                        separator: nonNullSeparador,
                        hasTitles: chkTitulos.Checked);

                    MessageBox.Show($"Archivo procesado correctamente.\nGuardado en: {outputFile}", 
                        "Proceso completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (progressBarProceso.InvokeRequired)
                    {
                        progressBarProceso.Invoke(new Action(() => progressBarProceso.Visible = false));
                    }
                    else
                    {
                        progressBarProceso.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los archivos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
