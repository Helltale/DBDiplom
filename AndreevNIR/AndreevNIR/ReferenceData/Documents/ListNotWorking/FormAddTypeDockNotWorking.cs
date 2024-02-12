using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData.Documents.ListNotWorking;

namespace AndreevNIR.ReferenceData
{
    public partial class FormAddTypeDockNotWorking : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        string omc = null;

        public FormAddTypeDockNotWorking()
        {
            InitializeComponent();
            LoadDGV(dataGridView2);
            textBox2.Enabled = false;
        }

        private void LoadDGV(DataGridView dgv) {
            string str = "select omc, full_name \"ФИО пациента\" from patient";
            cl.ShowDGV(str, dgv, db._connectionString);
            dgv.Columns[0].Visible = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClassListNotWorking cln = new ClassListNotWorking();
            try { omc = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить omc
            textBox2.Text = cln.GetNumberExtract(omc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDGV(dataGridView2);
            textBox3.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cl.ShowDGV($"select omc, full_name \"ФИО пациента\" from patient where full_name like '%{textBox3.Text}%'", dataGridView2, db._connectionString);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClassListNotWorking cln = new ClassListNotWorking();
            cln.CreateListNotWorking(omc, textBox2.Text, monthCalendar1, textBox1);
            this.Close();
        }
    }
}
