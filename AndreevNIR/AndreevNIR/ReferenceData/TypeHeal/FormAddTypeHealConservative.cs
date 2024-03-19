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
    public partial class FormAddTypeHealConservative : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        ClassTypeHealConservative ct = new ClassTypeHealConservative();
        private bool isUpdate = false;
        string id_therapist = null;
        string id_patient = null;
        string id_nurce = null;
        string id_procedure = null;
        string date_procedure = null;
        string time_procedure = null;

        public FormAddTypeHealConservative()
        {
            InitializeComponent();
            LoadDGV("select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView1); //therapist
            LoadDGV("select t1.id_staff, t2.full_name from nurce t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView3); //nurce
            cl.LoadComboboxByQuery(comboBox2, "select name_drocedure from procedures_;", "Выберите процедуру");
        }

        public FormAddTypeHealConservative(string id_staff_, string id_patient_, string id_procedure_, string date_, string time_, bool tmp)
        {
            InitializeComponent();
            isUpdate = tmp;
            LoadDGV("select t1.id_staff, t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView1); //therapist
            LoadDGV("select t1.id_staff, t2.full_name from nurce t1 join staff t2 on t1.id_staff = t2.id_staff", dataGridView3); //nurce
            cl.LoadComboboxByQuery(comboBox2, "select name_drocedure from procedures_;", "Выберите процедуру");
            ct.GetConservative(id_staff_, id_patient_, comboBox2, dataGridView3, monthCalendar1, textBox1);

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            id_therapist = id_staff_;
            id_patient = id_patient_;
            id_procedure = id_procedure_;
            date_procedure = date_;
            time_procedure = time_;
        }

        private void LoadDGV(string str, DataGridView dgv)
        {
            cl.ShowDGV(str, dgv, db._connectionString);
            dgv.Columns[0].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_therapist = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить id_therapist
            LoadDGV($"select t1.id_patient, t3.full_name from doc_patient t1 join patient_in_room t2 on t1.id_patient = t2.id_patient join patient t3 on t3.omc = t2.omc where t1.id_staff = '{id_therapist}'", dataGridView2);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_patient = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить id_patient
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { id_nurce = dataGridView3.SelectedRows[0].Cells[0].Value.ToString(); } catch { } //получить id_nurce
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox1);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Время проведения");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.CheckedCombobox(comboBox2);
                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1 });
                var errorMessage2 = cf.GenerateErrorMessageEmptyComboBox(listFill2, "Процедура");
                if (errorMessage2 == "Значения в следующих выпадающих меню не были выбраны: ")
                {

                    var flag2 = cf.SelectedDGV(dataGridView1);
                    var flag3 = cf.SelectedDGV(dataGridView2);
                    var flag4 = cf.SelectedDGV(dataGridView3);

                    var listFill3 = new List<bool>();
                    listFill3.AddRange(new bool[] { flag2, flag3, flag4 });
                    var errorMessage3 = cf.GenerateErrorMessageEmptyDGV(listFill3, "Лечащий врач", "Пациент", "Медицинская сестра");
                    if (errorMessage3 == "Не были выбраны поля: ")
                    {

                        var flag5 = cf.DigitAndColon(textBox1);

                        var listFill4 = new List<bool>();
                        listFill4.AddRange(new bool[] { flag5 });
                        var errorMessage4 = cf.GenerateErrorMessageErrors(listFill4, "Время проведения");
                        if (errorMessage4 == "Следующие поля были заполнены с ошибками: ")
                        {
                            if (isUpdate)
                            {
                                string id_procedure_new = ct.ReturnProcedureID(comboBox2.Text);
                                ct.UpdateConservative(id_therapist, id_patient, id_procedure, date_procedure, time_procedure, id_procedure_new, id_nurce, monthCalendar1.SelectionStart, textBox1.Text);
                            }
                            else
                            {
                                string id_procedure = ct.ReturnProcedureID(comboBox2.SelectedItem.ToString());
                                ct.CreateConservative(id_therapist, id_patient, id_procedure, id_nurce, monthCalendar1.SelectionStart, textBox1.Text);
                            }
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
    }
}
