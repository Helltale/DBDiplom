using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace AndreevNIR.ReferenceData.TypeHeal
{
    class ClassTypeHealExamination
    {
        public void FindAllPeople(DataGridView dgv, string strQuery) {
            ShowDGVSelected(strQuery, dgv);
        }

        private void ShowDGVSelected(string query, DataGridView dgv)
        {
            DBLogicConnection db = new DBLogicConnection();

            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgv.DataSource = dt;
                        dgv.Columns[1].Visible = false; // показывать только колонку "full_name"
                    }
                }

                connection.Close();
            }
        }

        //public void CreateExamination() {
        //    DBLogicConnection db = new DBLogicConnection();

        //    using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
        //    {
        //        connection.Open();
        //        using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{cbNameHirDepartment.SelectedItem.ToString()}'", connection))
        //        {
        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    code_hir_department = reader["code_hir_department"].ToString();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
