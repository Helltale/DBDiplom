using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Globalization;

namespace AndreevNIR.ReferenceData.TypeHeal
{
    class ClassTypeHealOperation
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        public void CreateOperation(MonthCalendar mcDate, string time, string id_staff, string id_patient, TextBox tbNameOperation, RichTextBox rtDiscrip, RichTextBox rtDicripBad)
        {
            //создание операции
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into operation (id_operation, date_operation, time_operation, id_staff, id_patient, name_operation, discriptionary_operation, discriptionary_bad) VALUES " +
                    $"(@id_operation, @date_operation, @time_operation, @id_staff, @id_patient, @name_operation, @discriptionary_operation, @discriptionary_bad);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cl.GetLastIdFromQueryCast("operation", "id_operation");
                        command.Parameters.Add("@date_operation", NpgsqlTypes.NpgsqlDbType.Date).Value = mcDate.SelectionStart;
                        command.Parameters.Add("@time_operation", NpgsqlTypes.NpgsqlDbType.Time).Value = DateTime.Parse(time).TimeOfDay;
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        command.Parameters.Add("@name_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbNameOperation.Text;
                        command.Parameters.Add("@discriptionary_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rtDiscrip.Text;
                        command.Parameters.Add("@discriptionary_bad", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rtDicripBad.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleleOperation(string id_operation) {
            //удаление операции
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from operation where id_operation = @id_operation", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_operation;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void GetOperation(string id_operation, MonthCalendar mcDate, TextBox tbTime, TextBox tbNameOperation, RichTextBox tbDiscription, RichTextBox tbDiscriptionBad) {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from operation where id_operation = @id_operation;", connection))
                {
                    command.Parameters.Add("@id_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_operation;

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mcDate.SelectionStart = Convert.ToDateTime(reader["date_operation"].ToString());
                            mcDate.SelectionEnd = Convert.ToDateTime(reader["date_operation"].ToString());
                            tbTime.Text = reader["time_operation"].ToString();
                            tbNameOperation.Text = reader["name_operation"].ToString();
                            tbDiscription.Text = reader["discriptionary_operation"].ToString();
                            tbDiscriptionBad.Text = reader["discriptionary_bad"].ToString();
                        }
                    }
                }
            }
        }

        public void ChangeOperation(string id_operation, MonthCalendar mcDate, TextBox tbTime, TextBox tbNameOperation, RichTextBox tbDiscription, RichTextBox tbDiscriptionBad) {
            //создание операции
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update operation set date_operation = @date_operation, time_operation = @time_operation, " +
                    $"name_operation = @name_operation, discriptionary_operation = @discriptionary_operation, discriptionary_bad = @discriptionary_bad where id_operation = @id_operation;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_operation;
                        command.Parameters.Add("@date_operation", NpgsqlTypes.NpgsqlDbType.Date).Value = mcDate.SelectionStart;
                        command.Parameters.Add("@time_operation", NpgsqlTypes.NpgsqlDbType.Time).Value = DateTime.Parse(tbTime.Text).TimeOfDay;
                        command.Parameters.Add("@name_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbNameOperation.Text;
                        command.Parameters.Add("@discriptionary_operation", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbDiscription.Text;
                        command.Parameters.Add("@discriptionary_bad", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbDiscriptionBad.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
