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
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        public void FindAllPeople(DataGridView dgv, string strQuery)
        {
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
            string id_meeting = cl.GetLastIdFromQueryCast("meetings", "id_meeting");
            string id_staff = id_staff_;
            string id_patient = id_patient_;
            DateTime date_meeting = dateTime;
            string discription_meeting = richTextBox1.Text;
            string operation_control = richTextBox2.Text;
            DateTime time_meeting = Convert.ToDateTime(tbTime.Text);



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
        }

        public void DeleteExamination(string id_meeting)
        {
            //удаление палаты
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from meetings where id_meeting = @find;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@find", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_meeting;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LoadExamination(string id_meeting, MonthCalendar monthCalendar, RichTextBox richTextBox1, RichTextBox richTextBox2, TextBox tbTime) {
            
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from meetings where id_meeting = '{id_meeting}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            richTextBox1.Text = reader["discription_meeting"].ToString();
                            monthCalendar.SelectionStart = Convert.ToDateTime(reader["date_meeting"].ToString());
                            monthCalendar.SelectionEnd = Convert.ToDateTime(reader["date_meeting"].ToString());
                            tbTime.Text = reader["time_meeting"].ToString();
                            richTextBox2.Text = reader["operation_control"].ToString();
                        }
                    }
                }
            }
        }

        public void UpdateExamination(MonthCalendar dateTime, RichTextBox richTextBox1, RichTextBox richTextBox2, TextBox tbTime)
        {
            //обновление палаты
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update meetings set discription_meeting = @discription_meeting, date_meeting = @date_meeting, " +
                    $"time_meeting = @time_meeting, operation_control = @operation_control;", connection))
                {
                    try
                    {
                        var newTime = new DateTime();
                        newTime = Convert.ToDateTime(tbTime.Text);

                        command.Parameters.Add("@discription_meeting", NpgsqlTypes.NpgsqlDbType.Varchar).Value = richTextBox1.Text;
                        command.Parameters.Add("@date_meeting", NpgsqlTypes.NpgsqlDbType.Date).Value = dateTime.SelectionStart;
                        command.Parameters.AddWithValue("@time_meeting", newTime);
                        command.Parameters.Add("@operation_control", NpgsqlTypes.NpgsqlDbType.Varchar).Value = richTextBox2.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
