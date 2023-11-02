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
    public partial class FormOperationalDataPatient : Form
    {
        public FormOperationalDataPatient()
        {
            InitializeComponent();
            OperationalDataPatient();
        }

        public void OperationalDataPatient() {
            DBLogicConnection dBLogicConnection = new DBLogicConnection();

            string str1 = " select pa.full_name as \"ФИО пациента\", pai.date_room as \"Дата попадания\", pai.number_room as \"Номер палаты\", td.name_department " +
                "as \"Отделение\", h.name_hir_department as \"Хирургический стационар\", e.date_extract as \"Дата выписки\" from patient_in_room pai " +
                "join patient pa on pa.omc = pai.omc join department d on d.id_department = pai.id_department join type_department td on td.id_department = " +
                "d.id_department join hir_hospital h on h.code_hir_department = pai.code_hir_department join extract_document e on e.id_patient = pai.id_patient;";
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormOperationalDataPatientAdd add = new FormOperationalDataPatientAdd();
            add.ShowDialog();
        }
    }
}
