using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR
{
    public partial class FormOperationalDataList : Form
    {
        public FormOperationalDataList()
        {
            InitializeComponent();
            OperationalDataExtract();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormOperationalDataListAdd add = new FormOperationalDataListAdd();
            add.ShowDialog();
        }

        public void OperationalDataExtract()
        {
            DBLogicConnection dBLogicConnection = new DBLogicConnection();
            string str1 = "select numb_extract as \"Номер эпикриза\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО сотрудника\", e.date_extract as " +
                "\"Дата создания\", e.time_extract as \"Время создания\", e.diagnosis_extract as \"Диагноз при выписке\" from extract_document e join staff s on s.id_staff " +
                "= e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc";
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }
    }
}
