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

namespace AndreevNIR
{
    public partial class FormReferenceData : Form
    {
        DBLogicConnection dBLogicConnection = new DBLogicConnection();


        public FormReferenceData()
        {
            InitializeComponent();
            LoadGridPersonal();
        }

        public void LoadGridPersonal() {
            
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM test1", dBLogicConnection._connectionString);

            try
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView2.DataSource = table;
            }
            catch { 
            }

            
        }


        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormIndex2 formIndex2 = new FormIndex2();
            formIndex2.richTextBoxPrimeTime.Show();
            this.Hide();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void FormReferenceData_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReferenceDataAdd add = new FormReferenceDataAdd();
            add.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (tabControl1.SelectedIndex) {
                case 0:
                    string str0 = "SELECT staff.full_name as ФИО, type_department.name_department Название_отделения, hir_hospital.name_hir_department Название_стационара, staff.phone Телефон, staff.mail Почта, user_info.role_user Уровень_доступа, " +
                        "department.boss_department Начальник_отделения, hir_hospital.boss_hir_department Начальник_стационара, case when guard_nurse.id_staff is not null then 'Постовая мед сестра' when therapist.id_staff is not null then 'Врач' " +
                        "when receptionist.id_staff is not null then 'Врач приёмного покоя' end as \"Должность\" FROM staff FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = " +
                        "staff.id_staff FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff FULL OUTER JOIN department ON department.id_department = staff.id_department " +
                        "FULL OUTER JOIN type_department ON department.id_department = type_department.id_department FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department;";
                    NpgsqlDataAdapter adapter0 = new NpgsqlDataAdapter(str0, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter0.Fill(table);

                        dataGridView2.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;

                case 1: //структура больницы, id поменять на фио докторов, через подзапрос
                    string str1 = "SELECT name_department, boss_department, name_hir_department, adress_hir_department, boss_hir_department, phone_hir_department, " +
                        "ogrm_hir_department, number_room FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = " +
                        "hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department";
                    NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
                    try{
                        DataTable table = new DataTable();
                        adapter1.Fill(table);

                        dataGridView3.DataSource = table;
                    }
                    catch(Exception ex) { MessageBox.Show("Ошибка: "+ ex); }
                    break;

                case 2:
                    MessageBox.Show("Окно1");
                    break;

                case 3:
                    MessageBox.Show("Окно1");
                    break;

                case 4:
                    MessageBox.Show("Окно1");
                    break;

                case 5:
                    MessageBox.Show("Окно1");
                    break;

            }
        }

        private void LoadGridRooms() {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM test1", dBLogicConnection._connectionString);

            try
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;
            }
            catch
            {
            }
        }
    }
}
