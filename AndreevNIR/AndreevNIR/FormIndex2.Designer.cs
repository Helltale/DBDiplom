namespace AndreevNIR
{
    partial class FormIndex2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelName = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelInstruments = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxOperationalData = new System.Windows.Forms.ComboBox();
            this.buttonRequests = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonInformation = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonReferenceData = new System.Windows.Forms.Button();
            this.panelForms = new System.Windows.Forms.Panel();
            this.richTextBoxPrimeTime = new System.Windows.Forms.RichTextBox();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panelInstruments.SuspendLayout();
            this.panelForms.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panelHeader.Controls.Add(this.labelName);
            this.panelHeader.Controls.Add(this.pictureBoxLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1839, 100);
            this.panelHeader.TabIndex = 0;
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(224, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(1615, 100);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "Роль - должность";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.Gray;
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxLogo.Image = global::AndreevNIR.Properties.Resources.database;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(224, 100);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // panelInstruments
            // 
            this.panelInstruments.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelInstruments.Controls.Add(this.button1);
            this.panelInstruments.Controls.Add(this.comboBoxOperationalData);
            this.panelInstruments.Controls.Add(this.buttonRequests);
            this.panelInstruments.Controls.Add(this.buttonReport);
            this.panelInstruments.Controls.Add(this.buttonInformation);
            this.panelInstruments.Controls.Add(this.buttonExit);
            this.panelInstruments.Controls.Add(this.buttonReferenceData);
            this.panelInstruments.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelInstruments.Location = new System.Drawing.Point(0, 100);
            this.panelInstruments.Name = "panelInstruments";
            this.panelInstruments.Size = new System.Drawing.Size(224, 1011);
            this.panelInstruments.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 722);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 52);
            this.button1.TabIndex = 7;
            this.button1.Text = "???";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxOperationalData
            // 
            this.comboBoxOperationalData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxOperationalData.FormattingEnabled = true;
            this.comboBoxOperationalData.ItemHeight = 20;
            this.comboBoxOperationalData.Items.AddRange(new object[] {
            "Учёт пациентов",
            "Выписные листы",
            "История болезни"});
            this.comboBoxOperationalData.Location = new System.Drawing.Point(12, 115);
            this.comboBoxOperationalData.Name = "comboBoxOperationalData";
            this.comboBoxOperationalData.Size = new System.Drawing.Size(200, 28);
            this.comboBoxOperationalData.TabIndex = 6;
            this.comboBoxOperationalData.Text = "Оперативные данные";
            this.comboBoxOperationalData.SelectedIndexChanged += new System.EventHandler(this.comboBoxOperationalData_SelectedIndexChanged);
            // 
            // buttonRequests
            // 
            this.buttonRequests.Location = new System.Drawing.Point(12, 444);
            this.buttonRequests.Name = "buttonRequests";
            this.buttonRequests.Size = new System.Drawing.Size(200, 52);
            this.buttonRequests.TabIndex = 5;
            this.buttonRequests.Text = "Запросы";
            this.buttonRequests.UseVisualStyleBackColor = true;
            this.buttonRequests.Click += new System.EventHandler(this.buttonRequests_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.Location = new System.Drawing.Point(12, 502);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(200, 52);
            this.buttonReport.TabIndex = 4;
            this.buttonReport.Text = "Отчёты";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonInformation
            // 
            this.buttonInformation.Location = new System.Drawing.Point(12, 780);
            this.buttonInformation.Name = "buttonInformation";
            this.buttonInformation.Size = new System.Drawing.Size(200, 52);
            this.buttonInformation.TabIndex = 3;
            this.buttonInformation.Text = "Информация";
            this.buttonInformation.UseVisualStyleBackColor = true;
            this.buttonInformation.Click += new System.EventHandler(this.buttonInformation_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(12, 838);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(200, 52);
            this.buttonExit.TabIndex = 2;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonReferenceData
            // 
            this.buttonReferenceData.Location = new System.Drawing.Point(12, 57);
            this.buttonReferenceData.Name = "buttonReferenceData";
            this.buttonReferenceData.Size = new System.Drawing.Size(200, 52);
            this.buttonReferenceData.TabIndex = 0;
            this.buttonReferenceData.Text = "Справоные данные";
            this.buttonReferenceData.UseVisualStyleBackColor = true;
            this.buttonReferenceData.Click += new System.EventHandler(this.buttonReferenceData_Click);
            // 
            // panelForms
            // 
            this.panelForms.Controls.Add(this.richTextBoxPrimeTime);
            this.panelForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForms.Location = new System.Drawing.Point(224, 100);
            this.panelForms.Name = "panelForms";
            this.panelForms.Size = new System.Drawing.Size(1615, 1011);
            this.panelForms.TabIndex = 2;
            // 
            // richTextBoxPrimeTime
            // 
            this.richTextBoxPrimeTime.Location = new System.Drawing.Point(32, 35);
            this.richTextBoxPrimeTime.Name = "richTextBoxPrimeTime";
            this.richTextBoxPrimeTime.Size = new System.Drawing.Size(1544, 854);
            this.richTextBoxPrimeTime.TabIndex = 0;
            this.richTextBoxPrimeTime.Text = "Здесь будет\nЗагруженность отделения\n\n\nА могла бы быть ваша реклама\n";
            // 
            // FormIndex2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1839, 1111);
            this.Controls.Add(this.panelForms);
            this.Controls.Add(this.panelInstruments);
            this.Controls.Add(this.panelHeader);
            this.Name = "FormIndex2";
            this.Text = "FormIndex2";
            this.Load += new System.EventHandler(this.FormIndex2_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panelInstruments.ResumeLayout(false);
            this.panelForms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelInstruments;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonInformation;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonReferenceData;
        private System.Windows.Forms.Button buttonRequests;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.ComboBox comboBoxOperationalData;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Panel panelForms;
        public System.Windows.Forms.RichTextBox richTextBoxPrimeTime;
        private System.Windows.Forms.Button button1;
    }
}