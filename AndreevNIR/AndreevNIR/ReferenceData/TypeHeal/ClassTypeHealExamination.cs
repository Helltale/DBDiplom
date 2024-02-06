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

        public void CreateExamination(string id_staff_, string id_patient_, DateTime dateTime, RichTextBox richTextBox1, RichTextBox richTextBox2, TextBox tbTime)
        {
            CoreLogic cl = new CoreLogic();
            string id_meeting = cl.GetLastIdFromQuery("meetings", "id_meeting");
            string id_staff = id_staff_;
            string id_patient = id_patient_;
            DateTime date_meeting = dateTime;
            string discription_meeting = richTextBox1.Text;
            string operation_control = richTextBox2.Text;
            DateTime time_meeting = Convert.ToDateTime(tbTime.Text);

            DBLogicConnection db = new DBLogicConnection();

            //создание палаты
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into meetings (id_meeting, id_staff, id_patient, discription_meeting, date_meeting, time_meeting, operation_control) values (@id_meeting, @id_staff, @id_patient, @discription_meeting, @date_meeting, @time_meeting, @operation_control);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_meeting", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_meeting;
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        command.Parameters.Add("@discription_meeting", NpgsqlTypes.NpgsqlDbType.Varchar).Value = discription_meeting;
                        command.Parameters.Add("@date_meeting", NpgsqlTypes.NpgsqlDbType.Date).Value = date_meeting;
                        //command.Parameters.Add("@time_meeting", NpgsqlTypes.NpgsqlDbType.Time).Value = time_meeting;//проблемный?
                        command.Parameters.AddWithValue("@time_meeting", time_meeting);
                        command.Parameters.Add("@operation_control", NpgsqlTypes.NpgsqlDbType.Varchar).Value = operation_control;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }

            

            //using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            //{
            //    connection.Open();
            //    using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{cbNameHirDepartment.SelectedItem.ToString()}'", connection))
            //    {
            //        using (NpgsqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.Read())
            //            {
            //                code_hir_department = reader["code_hir_department"].ToString();
            //            }
            //        }
            //    }
            //}
        }
    }
}
