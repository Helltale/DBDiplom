using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR
{
    public partial class FormReferenceDataAdd : Form
    {
        public FormReferenceDataAdd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelFormAdd.Controls.Clear();

            FormAddStaff addStaff = new FormAddStaff()
            { TopLevel = false, TopMost = true };
            addStaff.FormBorderStyle = FormBorderStyle.None;
            panelFormAdd.Controls.Add(addStaff);
            addStaff.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelFormAdd.Controls.Clear();

            FormAddStruct addStruct = new FormAddStruct()
            { TopLevel = false, TopMost = true };
            addStruct.FormBorderStyle = FormBorderStyle.None;
            panelFormAdd.Controls.Add(addStruct);
            addStruct.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelFormAdd.Controls.Clear();

            FormAddTypeHeal formAddTypeHeal = new FormAddTypeHeal()
            { TopLevel = false, TopMost = true };
            formAddTypeHeal.FormBorderStyle = FormBorderStyle.None;
            panelFormAdd.Controls.Add(formAddTypeHeal);
            formAddTypeHeal.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panelFormAdd.Controls.Clear();

            FormAddTypeDock formAddTypeDock = new FormAddTypeDock() 
            { TopLevel = false, TopMost = true };
            formAddTypeDock.FormBorderStyle = FormBorderStyle.None;
            panelFormAdd.Controls.Add(formAddTypeDock);
            formAddTypeDock.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelFormAdd.Controls.Clear();

            FormAddTypeProc formAddTypeProc = new FormAddTypeProc()
            { TopLevel = false, TopMost = true };
            formAddTypeProc.FormBorderStyle = FormBorderStyle.None;
            panelFormAdd.Controls.Add(formAddTypeProc);
            formAddTypeProc.Show();
        }
    }
}
