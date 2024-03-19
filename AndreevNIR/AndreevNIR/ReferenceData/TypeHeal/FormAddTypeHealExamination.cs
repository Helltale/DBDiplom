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
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox1);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Время осмотра");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.SelectedDGV(dataGridView1); //врач
                var flag2 = cf.SelectedDGV(dataGridView2); //пациент

                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1, flag2 });
                var errorMessage2 = cf.GenerateErrorMessageEmptyDGV(listFill2, "Лечащий врач", "Пациент");
                if (errorMessage2 == "Не были выбраны поля: ")
                {

                    var flag3 = cf.CheckRichTextBox(richTextBox1);
                    var listFill3 = new List<bool>();
                    listFill3.AddRange(new bool[] { flag3 });
                    var errorMessage3 = cf.GenerateErrorMessageEmptyRichTextBox(listFill3, "Комментарий врача");
                    if (errorMessage3 == "Не были заполнены: ")
                    {

                        var flag4 = cf.DigitAndColon(textBox1); //время осмотра
                        var listFill4 = new List<bool>();
                        listFill4.AddRange(new bool[] { flag4 });
                        var errorMessage4 = cf.GenerateErrorMessageErrors(listFill4, "Время осмотра");
                        if (errorMessage4 == "Следующие поля были заполнены с ошибками: ")
                        {
                            ClassTypeHealExamination ct = new ClassTypeHealExamination();
                            if (id_meeting == null)
                            {
                                ct.CreateExamination(id_therapist, id_patient, monthCalendar1.SelectionStart, richTextBox1, richTextBox2, textBox1);
                            }
                            else { ct.UpdateExamination(monthCalendar1, richTextBox1, richTextBox2, textBox1); }
                            this.Close();
                        }
                        else { MessageBox.Show(errorMessage4); }
                    }
                    else { MessageBox.Show(errorMessage3); }
                }
                else { MessageBox.Show(errorMessage2); }
            }
            else { MessageBox.Show(errorMessage1); }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_patient = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить patient_id
        }
    }
}
