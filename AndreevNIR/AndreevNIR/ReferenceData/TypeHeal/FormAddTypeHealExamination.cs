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
        ClassTypeHealExamination ct = new ClassTypeHealExamination();
        private int id_staff_selected_d;
        private int id_patient_selected_d;
        private string id_staff_selected_s = null;
        private string id_patient_selected_s = null;
        private DateTime date_meeteng;


        public FormAddTypeHealExamination()
        {
            InitializeComponent();
            FillDataGrids(); //id хранятся в [1]
            richTextBox2.Enabled = false;
        }

        private void FillDataGrids() {
            //врач
            string str1 = "select t2.full_name, t1.id_staff from therapist t1 join staff t2 on t1.id_staff = t2.id_staff";
            ct.FindAllPeople(dataGridView1, str1);

            //пациент
            string str2 = "select full_name, id_staff from staff";
            ct.FindAllPeople(dataGridView2, str2);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_staff_selected_d = dataGridView1.SelectedRows[0].Index; } catch { }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_patient_selected_d = dataGridView2.SelectedRows[0].Index; } catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1 = $"select t2.full_name, t1.id_staff from therapist t1 join staff t2 on t1.id_staff = t2.id_staff where full_name like '%{textBox2.Text}%'";
            ct.FindAllPeople(dataGridView1, str1);
        }

        private void FormAddTypeHealExamination_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str1 = "select t2.full_name, t1.id_staff from therapist t1 join staff t2 on t1.id_staff = t2.id_staff";
            ct.FindAllPeople(dataGridView1, str1);
            textBox2.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str2 = $"select full_name, id_staff from staff where full_name like '%{textBox3.Text}%'";
            ct.FindAllPeople(dataGridView2, str2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str2 = "select full_name, id_staff from staff";
            ct.FindAllPeople(dataGridView2, str2);
            textBox3.Text = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            date_meeteng = monthCalendar1.SelectionStart;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                richTextBox2.Enabled = true;
            }
            else {
                richTextBox2.Enabled = false;
                richTextBox2.Text = null;
            }
        }
    }
}
