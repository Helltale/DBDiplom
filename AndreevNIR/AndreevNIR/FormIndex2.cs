using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.additionalForms;
using AndreevNIR.OperationalData;

namespace AndreevNIR
{
    public partial class FormIndex2 : Form
    {

        private string CoreLable = "Домашняя страница";

        public FormIndex2()
        {
            InitializeComponent();
            Size = new Size(1250, 700);
            panelInstruments.Size = new Size(150,800);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        public void GetLabelData(string newName, string newID) {
            tmpName = newName;
            tmpJobID = newID;
        }

        //СОЗДАЛ ПЕРЕМЕННЫЕ ДЛЯ ВЫВОДА НА ГЛАВНЫЙ ЭКРАН!!!!! НЕ ЗАБЫТЬ!!!
        public string tmpName;
        public string tmpJobID;

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonReferenceData_Click(object sender, EventArgs e)
        {
            panelForms.Controls.Clear();

            FormReferenceData formReferenceData = new FormReferenceData() 
            { TopLevel = false, TopMost = true};
            formReferenceData.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(formReferenceData);
            try { formReferenceData.Show(); } catch { }

            ShowPrimeTime();
        }

        private void buttonInformation_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Информация");
        }

        private void buttonRequests_Click(object sender, EventArgs e)
        {
            //panelForms.Controls.Clear();

            //FormRequests formRequests = new FormRequests()
            //{ TopLevel = false, TopMost = true };
            //formRequests.FormBorderStyle = FormBorderStyle.None;
            //panelForms.Controls.Add(formRequests);
            //formRequests.Show();

            //ShowPrimeTime();

            panelForms.Controls.Clear();

            FormRequests formChild = new FormRequests()
            { TopLevel = false, TopMost = true };
            formChild.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(formChild);
            try { formChild.Show(); } catch { }

            ShowPrimeTime();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            panelForms.Controls.Clear();

            FormReport formReport = new FormReport()
            { TopLevel = false, TopMost = true };
            formReport.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(formReport);
            formReport.Show();

            ShowPrimeTime();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelForms.Controls.Clear();

            ChipiChipi chipi = new ChipiChipi()
            { TopLevel = false, TopMost = true };
            chipi.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(chipi);
            chipi.Show();

            ShowPrimeTime();
        }

        private void ShowPrimeTime() {
            PrimeTime form = new PrimeTime()
            { TopLevel = false, TopMost = true };
            form.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(form);
            form.Show();
        }

        private void FormIndex2_Load(object sender, EventArgs e)
        {
            ShowPrimeTime();
            labelName.Text = CoreLable;
        }

        private void buttonOperativeData_Click(object sender, EventArgs e)
        {
            panelForms.Controls.Clear();
            labelName.Text = "Оперативные данные";
            FormOperativeData formChild = new FormOperativeData()
            { TopLevel = false, TopMost = true };
            formChild.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(formChild);
            
            try { formChild.Show(); } catch { }
            
            ShowPrimeTime();
        }
    }
}
