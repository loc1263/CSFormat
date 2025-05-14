namespace CSFormat
{
    public partial class CSFormat : Form
    {
        public CSFormat()
        {
            InitializeComponent();
            // Assign the click event handlers
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
            Application.Exit();
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
                    (_, var inputFile, _) when !File.Exists(inputFile) => 
                        (false, "El archivo de entrada no existe."),
                    (_, _, null or "" or { Length: 0 }) => 
                        (false, "El separador no puede estar vacío."),
                    _ => (true, string.Empty)
                };

                if (!isValid)
                {
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool includeHeaders = chkTitulos.Checked;
                Console.WriteLine($"Incluir encabezados: {includeHeaders}");
                
                // Configurar la barra de progreso
                progressBarProceso.Value = 0;
                progressBarProceso.Visible = true;
                
                try
                {
                    // Crear un Progress para reportar el progreso
                    var progress = new Progress<int>(percent =>
                    {
                        // Asegurarse de que la actualización de la UI se haje en el hilo correcto
                        if (progressBarProceso.InvokeRequired)
                        {
                            progressBarProceso.Invoke(new Action(() => progressBarProceso.Value = percent));
                        }
                        else
                        {
                            progressBarProceso.Value = percent;
                        }
                    });
                    
                    // Crear el procesador de archivos con el reporte de progreso
                    var fileProcessor = new FileProcessor(progress);
                    
                    // Procesar el archivo (esto puede tardar)
                    string outputFile = fileProcessor.ProcessFile(
                        textBoxFormato.Text, 
                        textBoxEntrada.Text, 
                        textBoxSeparador.Text, 
                        includeHeaders);
                    
                    MessageBox.Show($"Archivo procesado exitosamente.\nGuardado como: {outputFile}", 
                        "Proceso Completado", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                }
                finally
                {
                    // Asegurarse de que la barra de progreso se oculte al finalizar
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

        private string ProcessFile(string formatFilePath, string inputFilePath, string separator, bool hasTitles)
        {
            // Este método ya no se usa directamente, pero lo mantenemos por compatibilidad
            // El procesamiento ahora se maneja desde BtnProcesar_Click
            var processor = new FileProcessor(null);
            return processor.ProcessFile(formatFilePath, inputFilePath, separator, hasTitles);
        }


    }
}
