namespace CSFormat
{
    partial class CSFormat
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnAbrirFormato = new Button();
            lblFormato = new Label();
            textBoxFormato = new TextBox();
            textBoxEntrada = new TextBox();
            lbEntrada = new Label();
            btnAbrirInput = new Button();
            textBoxSeparador = new TextBox();
            lblSeparador = new Label();
            panelDatosEntrada = new Panel();
            btnCerrar = new Button();
            btnProcesar = new Button();
            chkTitulos = new CheckBox();
            progressBarProceso = new ProgressBar();
            menuStrip1 = new MenuStrip();
            configuracionToolStripMenuItem = new ToolStripMenuItem();
            idiomaToolStripMenuItem1 = new ToolStripMenuItem();
            espanolToolStripMenuItem1 = new ToolStripMenuItem();
            inglesToolStripMenuItem1 = new ToolStripMenuItem();
            acercaToolStripMenuItem = new ToolStripMenuItem();
            panelDatosEntrada.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnAbrirFormato
            // 
            btnAbrirFormato.Location = new Point(326, 34);
            btnAbrirFormato.Name = "btnAbrirFormato";
            btnAbrirFormato.Size = new Size(75, 23);
            btnAbrirFormato.TabIndex = 0;
            btnAbrirFormato.Text = "Abrir..";
            btnAbrirFormato.UseVisualStyleBackColor = true;
            // 
            // lblFormato
            // 
            lblFormato.AutoSize = true;
            lblFormato.Location = new Point(74, 60);
            lblFormato.Name = "lblFormato";
            lblFormato.Size = new Size(52, 15);
            lblFormato.TabIndex = 1;
            lblFormato.Text = "Formato";
            // 
            // textBoxFormato
            // 
            textBoxFormato.Location = new Point(74, 78);
            textBoxFormato.Name = "textBoxFormato";
            textBoxFormato.Size = new Size(280, 23);
            textBoxFormato.TabIndex = 2;
            // 
            // textBoxEntrada
            // 
            textBoxEntrada.Location = new Point(74, 139);
            textBoxEntrada.Name = "textBoxEntrada";
            textBoxEntrada.Size = new Size(280, 23);
            textBoxEntrada.TabIndex = 5;
            // 
            // lbEntrada
            // 
            lbEntrada.AutoSize = true;
            lbEntrada.Location = new Point(74, 121);
            lbEntrada.Name = "lbEntrada";
            lbEntrada.Size = new Size(107, 15);
            lbEntrada.TabIndex = 4;
            lbEntrada.Text = "Archivo de entrada";
            // 
            // btnAbrirInput
            // 
            btnAbrirInput.Location = new Point(326, 94);
            btnAbrirInput.Name = "btnAbrirInput";
            btnAbrirInput.Size = new Size(75, 23);
            btnAbrirInput.TabIndex = 3;
            btnAbrirInput.Text = "Abrir ..";
            btnAbrirInput.UseVisualStyleBackColor = true;
            // 
            // textBoxSeparador
            // 
            textBoxSeparador.Location = new Point(74, 211);
            textBoxSeparador.Name = "textBoxSeparador";
            textBoxSeparador.Size = new Size(60, 23);
            textBoxSeparador.TabIndex = 6;
            textBoxSeparador.Text = ",";
            // 
            // lblSeparador
            // 
            lblSeparador.AutoSize = true;
            lblSeparador.Location = new Point(74, 193);
            lblSeparador.Name = "lblSeparador";
            lblSeparador.Size = new Size(60, 15);
            lblSeparador.TabIndex = 7;
            lblSeparador.Text = "Separador";
            // 
            // panelDatosEntrada
            // 
            panelDatosEntrada.BorderStyle = BorderStyle.FixedSingle;
            panelDatosEntrada.Controls.Add(btnCerrar);
            panelDatosEntrada.Controls.Add(btnProcesar);
            panelDatosEntrada.Controls.Add(chkTitulos);
            panelDatosEntrada.Controls.Add(btnAbrirFormato);
            panelDatosEntrada.Controls.Add(btnAbrirInput);
            panelDatosEntrada.Location = new Point(53, 43);
            panelDatosEntrada.Name = "panelDatosEntrada";
            panelDatosEntrada.Size = new Size(425, 234);
            panelDatosEntrada.TabIndex = 8;
            // 
            // btnCerrar
            // 
            btnCerrar.Location = new Point(326, 165);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(75, 23);
            btnCerrar.TabIndex = 10;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnProcesar
            // 
            btnProcesar.Location = new Point(245, 165);
            btnProcesar.Name = "btnProcesar";
            btnProcesar.Size = new Size(75, 23);
            btnProcesar.TabIndex = 9;
            btnProcesar.Text = "Procesar";
            btnProcesar.UseVisualStyleBackColor = true;
            // 
            // chkTitulos
            // 
            chkTitulos.AutoSize = true;
            chkTitulos.Location = new Point(96, 169);
            chkTitulos.Name = "chkTitulos";
            chkTitulos.Size = new Size(61, 19);
            chkTitulos.TabIndex = 0;
            chkTitulos.Text = "Titulos";
            chkTitulos.UseVisualStyleBackColor = true;
            // 
            // progressBarProceso
            // 
            progressBarProceso.Location = new Point(53, 283);
            progressBarProceso.Name = "progressBarProceso";
            progressBarProceso.Size = new Size(425, 10);
            progressBarProceso.TabIndex = 9;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { configuracionToolStripMenuItem, acercaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(535, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStripFormat";
            // 
            // configuracionToolStripMenuItem
            // 
            configuracionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { idiomaToolStripMenuItem1 });
            configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            configuracionToolStripMenuItem.Size = new Size(95, 20);
            configuracionToolStripMenuItem.Text = "Configuracion";
            // 
            // idiomaToolStripMenuItem1
            // 
            idiomaToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { espanolToolStripMenuItem1, inglesToolStripMenuItem1 });
            idiomaToolStripMenuItem1.Name = "idiomaToolStripMenuItem1";
            idiomaToolStripMenuItem1.Size = new Size(111, 22);
            idiomaToolStripMenuItem1.Text = "Idioma";
            // 
            // espanolToolStripMenuItem1
            // 
            espanolToolStripMenuItem1.Checked = true;
            espanolToolStripMenuItem1.CheckState = CheckState.Checked;
            espanolToolStripMenuItem1.Name = "espanolToolStripMenuItem1";
            espanolToolStripMenuItem1.Size = new Size(115, 22);
            espanolToolStripMenuItem1.Text = "Espanol";
            espanolToolStripMenuItem1.Click += espanolToolStripMenuItem1_Click;
            // 
            // inglesToolStripMenuItem1
            // 
            inglesToolStripMenuItem1.Name = "inglesToolStripMenuItem1";
            inglesToolStripMenuItem1.Size = new Size(115, 22);
            inglesToolStripMenuItem1.Text = "Ingles";
            inglesToolStripMenuItem1.Click += inglesToolStripMenuItem1_Click;
            // 
            // acercaToolStripMenuItem
            // 
            acercaToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            acercaToolStripMenuItem.Name = "acercaToolStripMenuItem";
            acercaToolStripMenuItem.Size = new Size(24, 20);
            acercaToolStripMenuItem.Text = "?";
            acercaToolStripMenuItem.Click += acercaToolStripMenuItem_Click;
            // 
            // CSFormat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(535, 342);
            Controls.Add(progressBarProceso);
            Controls.Add(lblSeparador);
            Controls.Add(textBoxSeparador);
            Controls.Add(textBoxEntrada);
            Controls.Add(lbEntrada);
            Controls.Add(textBoxFormato);
            Controls.Add(lblFormato);
            Controls.Add(panelDatosEntrada);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "CSFormat";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CSFormat";
            panelDatosEntrada.ResumeLayout(false);
            panelDatosEntrada.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelDatosEntrada; 
        
        private Label lblFormato;
        private Label lbEntrada;
        private Label lblSeparador;

        private TextBox textBoxFormato;
        private TextBox textBoxEntrada;
        private TextBox textBoxSeparador;

        private Button btnAbrirFormato;
        private Button btnAbrirInput;
        private Button btnProcesar;
        private Button btnCerrar;

        private CheckBox chkTitulos;
        private ProgressBar progressBarProceso;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem configuracionToolStripMenuItem;
        private ToolStripMenuItem idiomaToolStripMenuItem1;
        private ToolStripMenuItem espanolToolStripMenuItem1;
        private ToolStripMenuItem inglesToolStripMenuItem1;
        private ToolStripMenuItem acercaToolStripMenuItem;
    }
}
