
namespace AndreevNIR.ReferenceData.Procedures
{
    partial class FormAddDrug
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button7);
            this.groupBox6.Controls.Add(this.button8);
            this.groupBox6.Location = new System.Drawing.Point(12, 99);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(447, 103);
            this.groupBox6.TabIndex = 32;
            this.groupBox6.TabStop = false;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(225, 25);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(207, 59);
            this.button7.TabIndex = 1;
            this.button7.Text = "Закрыть";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 25);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(207, 59);
            this.button8.TabIndex = 0;
            this.button8.Text = "Сохранить";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 81);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Название препарата";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(420, 26);
            this.textBox1.TabIndex = 0;
            // 
            // FormAddDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 220);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Name = "FormAddDrug";
            this.Text = "FormAddGrud";
            this.groupBox6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
    }
}