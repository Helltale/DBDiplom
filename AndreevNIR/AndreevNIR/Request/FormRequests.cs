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
    public partial class FormRequests : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();

        private string statisticDiagnosis1 = "SELECT diagnosis AS \"Название болезни\", COUNT(diagnosis) AS \"Количество диагнозов\", ROUND((COUNT(diagnosis) / 100.0 * (SELECT COUNT(*) FROM Initial_inspection WHERE date_initial >= '2024-01-01'))*1000, 2) AS \"Процентное соотношение\" FROM Initial_inspection";
        private string statisticDiagnosis2 = " WHERE date_initial >= ";
        private string statisticDiagnosis3 = " AND date_initial <= ";
        private string statisticDiagnosis4 = " GROUP BY diagnosis;";

        private string buzy = "SELECT  h.name_hir_department AS \"Название стационара\", td.name_department AS \"Название отделения\", r.number_room AS \"Палата\", (CAST(r.sit_empt AS INT) - COALESCE(p.count_patients, 0)) AS \"Количество свободных мест\" FROM Room r JOIN Department d ON r.id_department = d.id_department AND r.code_hir_department = d.code_hir_department JOIN  Hir_hospital h ON d.code_hir_department = h.code_hir_department JOIN Type_department td ON d.id_department = td.id_department LEFT JOIN  (SELECT code_hir_department, id_department,  number_room,  COUNT(*) AS count_patients FROM Patient_in_room  GROUP BY code_hir_department, id_department, number_room) p ON r.code_hir_department = p.code_hir_department AND r.id_department = p.id_department AND r.number_room = p.number_room ORDER BY h.name_hir_department, td.name_department, r.number_room;";
        DateTime currentDay = DateTime.Now;
        public FormRequests()
        {
            InitializeComponent();
            checkBox1.Checked = true;
            dateTimePicker1.Value = new DateTime(currentDay.Year, 1, 1);
            dateTimePicker2.Enabled = false;
            label1.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormIndex2 formIndex2 = new FormIndex2();
            this.Hide();
        }

        private void FormRequests_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "Запрос";
            dateTimePicker1.Value = new DateTime(currentDay.Year, 1, 1);
            dateTimePicker2.Value = currentDay;
            label1.Text = null;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else {
                dateTimePicker1.Enabled = false;
                dateTimePicker1.Value = new DateTime(currentDay.Year, 1, 1);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Value = currentDay;
            }
        }

        private string GenerateQuery(string baseQuery, string whereClause1, string whereClause2, string groupByClause, DateTime? startDate = null, DateTime? endDate = null)
        {
            string query = baseQuery;

            if (startDate.HasValue)
            {
                query += whereClause1 + "'" + startDate.Value.ToString("yyyy-MM-dd") + "'";
            }

            if (endDate.HasValue)
            {
                query += (startDate.HasValue ? whereClause2 : whereClause1) + "'" + endDate.Value.ToString("yyyy-MM-dd") + "'";
            }

            query += groupByClause;

            return query;
        }

        private void BuzyLabelFill() {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT  SUM(CAST(r.sit_empt AS INT)) AS \"Общее количество свободных мест\",  COUNT(p.number_room) AS \"Общее количество занятых мест\" FROM  Room r LEFT JOIN Patient_in_room p ON r.number_room = p.number_room AND r.code_hir_department = p.code_hir_department AND r.id_department = p.id_department; ", connection))
                {
                    command.ExecuteNonQuery();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) {
                            string empt = reader["Общее количество свободных мест"].ToString();
                            string bizy = reader["Общее количество занятых мест"].ToString();
                            label1.Text = $"Общее количество свободных мест: {empt}, Общее количество занятых мест: {bizy}";
                        }
                        
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex) {
                case 0: //statisticDiagnosis
                    {
                        string query =  GenerateQuery(statisticDiagnosis1, statisticDiagnosis2, statisticDiagnosis3, statisticDiagnosis4,
                                        checkBox1.Checked ? (DateTime?)dateTimePicker1.Value : null,
                                        checkBox2.Checked ? (DateTime?)dateTimePicker2.Value : null);

                        cl.ShowDGV(query, dataGridView1, db._connectionString);
                    }
                    break;
                case 1:
                    {
                        string query = buzy;
                        cl.ShowDGV(query, dataGridView1, db._connectionString);
                        BuzyLabelFill();
                    }
                    break;
                default:
                    MessageBox.Show("Выберите пункт \"Запрос\"");
                    break;
            }
        }
    }
}
