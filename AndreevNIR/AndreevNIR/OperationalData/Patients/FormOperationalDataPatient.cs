using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using AndreevNIR.OperationalData.Patients;

namespace AndreevNIR
{
    public partial class FormOperationalDataPatient : Form
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();
        private string str1 = "select t1.id_patient, t1.omc, t2.full_name \"ФИО пацента\", t6.full_name \"ФИО врача\", t1.date_room \"Дата попадания\", t7.date_extract \"Дата выписки\", t3.name_hir_department \"Стационар\", t4.name_department \"Отделение\", t1.number_room \"Палата\" from patient_in_room t1 join patient t2 on t1.omc = t2.omc join hir_hospital t3 on t3.code_hir_department = t1.code_hir_department join type_department t4 on t4.id_department = t1.id_department join doc_patient t5 on t5.id_patient = t1.id_patient join staff t6 on t5.id_staff = t6.id_staff full outer join extract_document t7 on t7.id_patient = t5.id_patient and t7.id_staff = t5.id_staff";
        
        private string id_patient = null;
        private string omc = null;
        private string hir_hosp = null;
        private string department = null;
        private string room = null;


        public FormOperationalDataPatient()
        {
            InitializeComponent();
            OperationalDataPatient();
        }

        public void OperationalDataPatient() {
            DBLogicConnection dBLogicConnection = new DBLogicConnection();

            
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);
                dataGridView1.DataSource = table;

                List<string> list = new List<string>();
                list.Add("ФИО пациента"); list.Add("Номер палаты"); list.Add("Отделение"); list.Add("Хирургический стационар");
                FillComboBox(comboBox2, list);
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
            dataGridView1.Columns[0].Visible = false; //id_patient
            dataGridView1.Columns[1].Visible = false; //omc
        }

        private void FillComboBox(ComboBox cb, List<string> li) {
            cb.DataSource = li;
            cb.Text = "Параметр";
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormOperationalDataPatientAdd add = new FormOperationalDataPatientAdd();
            add.ShowDialog();
            cl.ShowDGV(str1, dataGridView1, db._connectionString);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool flag = true;
            FormOperationalDataPatientAdd add = new FormOperationalDataPatientAdd(flag, id_patient, omc, hir_hosp, department, room);
            add.ShowDialog();
            cl.ShowDGV(str1, dataGridView1, db._connectionString);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            DataGridViewCell cell1 = row.Cells[0];
            DataGridViewCell cell2 = row.Cells[1];
            DataGridViewCell cell3 = row.Cells[6];
            DataGridViewCell cell4 = row.Cells[7];
            DataGridViewCell cell5 = row.Cells[8];
            id_patient = cell1.Value.ToString();
            omc = cell2.Value.ToString();
            hir_hosp = cell3.Value.ToString();
            department = cell4.Value.ToString();
            room = cell5.Value.ToString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ClassPatientInRoom cp = new ClassPatientInRoom();
            cp.DeletePatientInRoom(id_patient);
            cl.ShowDGV(str1, dataGridView1, db._connectionString);
        }
    }
}
