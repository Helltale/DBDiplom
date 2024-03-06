using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData.Documents.Extraction;

namespace AndreevNIR.ReferenceData
{
    public partial class FormAddTypeDockExtract : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();

        private string id_staff = null;
        private string id_patient = null;
        private readonly string number_extract = null;


        public FormAddTypeDockExtract()
        {
            InitializeComponent();
            LoadDGVs(dataGridView1, dataGridView2);
        }

        public FormAddTypeDockExtract(string _number_extract)
        {
            InitializeComponent();
            LoadDGVs(dataGridView1, dataGridView2);
            number_extract = _number_extract;
            groupBox2.Enabled = false;
            groupBox1.Enabled = false;
            ClassLogicExtraction cle = new ClassLogicExtraction();
            cle.GetExtraction(_number_extract, textBox5, textBox4, richTextBox1, monthCalendar1, textBox1, checkBox1);
            textBox5.Enabled = false;
        }

        private void LoadDGVs(DataGridView dgv1, DataGridView dgv2) {
            cl.ShowDGV("select id_staff, full_name from staff", dgv1, db._connectionString); //врач
            dgv1.Columns[0].Visible = false; //id_staff
            cl.ShowDGV("select t1.id_patient, t2.full_name from patient_in_room t1 join patient t2 on t1.omc = t2.omc", dgv2, db._connectionString); //пац
            dgv2.Columns[0].Visible = false; //id_patient
        }

        private void button1_Click_Find(object sender, EventArgs e)
        {
            cl.ShowDGV($"select id_staff, full_name from staff where full_name like '%{textBox2.Text}%'", dataGridView1, db._connectionString); //врач
            dataGridView1.Columns[0].Visible = false; //id_staff
        }

        private void button2_Click_Clear(object sender, EventArgs e)
        {
            cl.ShowDGV("select id_staff, full_name from staff", dataGridView1, db._connectionString); //врач
            dataGridView1.Columns[0].Visible = false; //id_staff
            textBox2.Text = null;
        }

        private void button4_Click_Find(object sender, EventArgs e)
        {
            cl.ShowDGV($"select t1.id_patient, t2.full_name from patient_in_room t1 join patient t2 on t1.omc = t2.omc where t2.full_name like '%{textBox3.Text}%'", dataGridView2, db._connectionString); //пац
            dataGridView2.Columns[0].Visible = false; //id_patient
        }

        private void button3_Click_Clear(object sender, EventArgs e)
        {
            cl.ShowDGV("select t1.id_patient, t2.full_name from patient_in_room t1 join patient t2 on t1.omc = t2.omc", dataGridView2, db._connectionString); //пац
            dataGridView2.Columns[0].Visible = false; //id_patient
            textBox3.Text = null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_staff = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); } catch { }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_patient = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); } catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClassLogicExtraction cle = new ClassLogicExtraction();
            if (number_extract != null) //изменяем ли мы запись?
            {
                cle.ChangeExtraction(number_extract, textBox5.Text, monthCalendar1.SelectionStart, textBox1.Text, textBox4.Text, richTextBox1.Text, checkBox1.Checked);
            }
            else {
                
                cle.CreateExtraction(textBox5.Text, id_staff, id_patient, monthCalendar1.SelectionStart, textBox1.Text, textBox4.Text, richTextBox1.Text, checkBox1.Checked);
            }
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
