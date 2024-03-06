using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData.TypeHeal;

namespace AndreevNIR.ReferenceData
{
    public partial class FormAddTypeHealExamination : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        private string id_therapist;
        private string id_patient;
        private string id_meeting;

        public FormAddTypeHealExamination()
        {
            InitializeComponent();
            LoadDGV("select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView1); //врач
            richTextBox2.Enabled = false;
        }

        public FormAddTypeHealExamination(string id_meeting_) {
            ClassTypeHealExamination ct = new ClassTypeHealExamination();

            InitializeComponent();
            LoadDGV("select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView1); //врач
            richTextBox2.Enabled = false;
            id_meeting = id_meeting_;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            ct.LoadExamination(id_meeting_, monthCalendar1, richTextBox1, richTextBox2, textBox1);
            if (richTextBox2.Text != null) { checkBox1.Checked = true; richTextBox2.Enabled = true; }
        }


        private void LoadDGV(string str, DataGridView dgv) {
            cl.ShowDGV(str, dgv, db._connectionString);
            dgv.Columns[0].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_therapist = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить therapist id
            LoadDGV($"select t1.id_patient, t3.full_name from doc_patient t1 join patient_in_room t2 on t1.id_patient = t2.id_patient join patient t3 on t3.omc = t2.omc where t1.id_staff = '{id_therapist}'", dataGridView2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDGV($"select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff where t2.full_name like '%{textBox2.Text}%'", dataGridView1); //врач
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadDGV("select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView1); //врач
            textBox2.Text = null;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                richTextBox2.Enabled = true;
            }
            else {
                richTextBox2.Enabled = false;
                richTextBox2.Text = null;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClassTypeHealExamination ct = new ClassTypeHealExamination();
            if (id_meeting == null)
            {
                ct.CreateExamination(id_therapist, id_patient, monthCalendar1.SelectionStart, richTextBox1, richTextBox2, textBox1);
            }
            else { ct.UpdateExamination(monthCalendar1, richTextBox1, richTextBox2, textBox1); }
            this.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_patient = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить patient_id
        }
    }
}
