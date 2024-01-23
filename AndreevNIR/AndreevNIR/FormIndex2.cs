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

namespace AndreevNIR
{
    public partial class FormIndex2 : Form
    {

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
            richTextBoxPrimeTime.Hide();
            panelForms.Controls.Clear();

            FormReferenceData formReferenceData = new FormReferenceData() 
            { TopLevel = false, TopMost = true};
            formReferenceData.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(formReferenceData);
            formReferenceData.Show();
        }

        private void buttonInformation_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Информация");
        }

        private void buttonRequests_Click(object sender, EventArgs e)
        {
            richTextBoxPrimeTime.Hide();
            panelForms.Controls.Clear();

            FormRequests formRequests = new FormRequests()
            { TopLevel = false, TopMost = true };
            formRequests.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(formRequests);
            formRequests.Show();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            richTextBoxPrimeTime.Hide();
            panelForms.Controls.Clear();

            FormReport formReport = new FormReport()
            { TopLevel = false, TopMost = true };
            formReport.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(formReport);
            formReport.Show();
        }

        private void comboBoxOperationalData_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBoxOperationalData.SelectedItem.ToString();

            switch (selected) {

                case "Учёт пациентов":
                    richTextBoxPrimeTime.Hide();
                    panelForms.Controls.Clear();

                    FormOperationalDataPatient formChild1 = new FormOperationalDataPatient()
                    { TopLevel = false, TopMost = true };
                    formChild1.FormBorderStyle = FormBorderStyle.None;
                    panelForms.Controls.Add(formChild1);
                    formChild1.Show();

                    break;

                case "Выписные листы":
                    richTextBoxPrimeTime.Hide();
                    panelForms.Controls.Clear();

                    FormOperationalDataList formChild2 = new FormOperationalDataList()
                    { TopLevel = false, TopMost = true };
                    formChild2.FormBorderStyle = FormBorderStyle.None;
                    panelForms.Controls.Add(formChild2);
                    formChild2.Show();

                    break;

                case "История болезни":
                    richTextBoxPrimeTime.Hide();
                    panelForms.Controls.Clear();

                    FormOperationalDataHistory formChild3 = new FormOperationalDataHistory()
                    { TopLevel = false, TopMost = true };
                    formChild3.FormBorderStyle = FormBorderStyle.None;
                    panelForms.Controls.Add(formChild3);
                    formChild3.Show();

                    break;

                default:
                    MessageBox.Show("Ошбика выбора оперативных данных");
                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBoxPrimeTime.Hide();
            panelForms.Controls.Clear();

            ChipiChipi chipi = new ChipiChipi()
            { TopLevel = false, TopMost = true };
            chipi.FormBorderStyle = FormBorderStyle.None;
            panelForms.Controls.Add(chipi);
            chipi.Show();

        }
    }
}
