namespace CSFormat
{
    partial class CSAbout
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            labelProductName = new Label();
            labelVersion = new Label();
            labelAuthor = new Label();
            okButton = new Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(labelProductName, 0, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 0, 1);
            tableLayoutPanel.Controls.Add(labelAuthor, 0, 2);
            tableLayoutPanel.Controls.Add(okButton, 0, 3);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(15, 15);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 23F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 17F));
            tableLayoutPanel.Size = new Size(320, 160);
            tableLayoutPanel.TabIndex = 0;
            // 
            // labelProductName
            // 
            labelProductName.Dock = DockStyle.Fill;
            labelProductName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            labelProductName.Location = new Point(3, 0);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new Size(294, 50);
            labelProductName.TabIndex = 0;
            labelProductName.Text = "Nombre de la aplicación";
            labelProductName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            labelVersion.Dock = DockStyle.Fill;
            labelVersion.Location = new Point(3, 50);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(294, 50);
            labelVersion.TabIndex = 1;
            labelVersion.Text = "Versión";
            labelVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelAuthor
            // 
            labelAuthor.Dock = DockStyle.Fill;
            labelAuthor.Location = new Point(3, 100);
            labelAuthor.Name = "labelAuthor";
            labelAuthor.Size = new Size(294, 50);
            labelAuthor.TabIndex = 2;
            labelAuthor.Text = "Autor";
            labelAuthor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.None;
            okButton.DialogResult = DialogResult.Cancel;
            okButton.Location = new Point(110, 165);
            okButton.Name = "okButton";
            okButton.Size = new Size(80, 25);
            okButton.TabIndex = 3;
            okButton.Text = "&Aceptar";
            okButton.Click += OkButton_Click;
            // 
            // CSAbout
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 220);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CSAbout";
            Padding = new Padding(15);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Acerca de";
            tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Button okButton;
    }
}
