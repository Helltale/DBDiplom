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
    public partial class FormAddTypeHealOperations : Form
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        private string id_therapist = null;
        private string id_patient = null;
        private string id_operation = null;


        public FormAddTypeHealOperations()
        {
            InitializeComponent();
            LoadDGV("select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView1); //therapist
        }

        public FormAddTypeHealOperations(string id_operation_)
        {
            ClassTypeHealOperation ct = new ClassTypeHealOperation();
            InitializeComponent();
            LoadDGV("select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView1); //therapist
            id_operation = id_operation_;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            ct.GetOperation(id_operation_, monthCalendar1, textBox1, textBox4, richTextBox1, richTextBox2);
        }

        private void LoadDGV(string str, DataGridView dgv)
        {
            cl.ShowDGV(str, dgv, db._connectionString);
            dgv.Columns[0].Visible = false;
        }



        private void button8_Click(object sender, EventArgs e)
        {
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox1, textBox4);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Время проведения", "Название операции");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.CheckRichTextBox(richTextBox1);
                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1 });
                var errorMessage2 = cf.GenerateErrorMessageEmptyRichTextBox(listFill2, "Описание операции");
                if (errorMessage2 == "Не были заполнены: ")
                {

                    var flag2 = cf.DigitAndColon(textBox1); //время
                    var flag3 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox4); //название операции

                    var listFill4 = new List<bool>();
                    listFill4.AddRange(new bool[] { flag2, flag3 });
                    var errorMessage4 = cf.GenerateErrorMessageErrors(listFill4, "Время проведения", "Название операции");
                    if (errorMessage4 == "Следующие поля были заполнены с ошибками: ")
                    {
                        ClassTypeHealOperation ct = new ClassTypeHealOperation();
                        if (id_operation == null)
                        {
                            ct.CreateOperation(monthCalendar1, textBox1.Text, id_therapist, id_patient, textBox4, richTextBox1, richTextBox2);
                        }
                        else
                        {
                            ct.ChangeOperation(id_operation, monthCalendar1, textBox1, textBox4, richTextBox1, richTextBox2);
                        }
                        this.Close();
                    }
                    else { MessageBox.Show(errorMessage4); }
                }
                else { MessageBox.Show(errorMessage2); }
            }
            else { MessageBox.Show(errorMessage1); }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_therapist = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить id_therapist
            LoadDGV($"select t1.id_patient, t3.full_name from doc_patient t1 join patient_in_room t2 on t1.id_patient = t2.id_patient join patient t3 on t3.omc = t2.omc where t1.id_staff = '{id_therapist}'", dataGridView2);
            MessageBox.Show(id_therapist);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_patient = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить id_patient
            MessageBox.Show(id_patient);
        }
    }
}
