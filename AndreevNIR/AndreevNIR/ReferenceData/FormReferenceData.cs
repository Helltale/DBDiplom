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
                    NpgsqlDataAdapter adapter0 = new NpgsqlDataAdapter("", dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter0.Fill(table);

                        dataGridView3.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;

                case 1: //структура больницы, id поменять на фио докторов, через подзапрос
                    NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter("SELECT name_department, boss_department, name_hir_department, adress_hir_department, boss_hir_department, phone_hir_department, " +
                        "ogrm_hir_department, number_room FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = " +
                        "hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department", dBLogicConnection._connectionString);
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
