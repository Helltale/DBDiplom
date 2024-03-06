using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.TypeHeal
{
    class ClassTypeHealConservative
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        public string ReturnProcedureID(string name_procedure) {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                string id_procedure = null;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_procedure from procedures_ where name_drocedure = '{name_procedure}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_procedure = reader["id_procedure"].ToString();
                        }
                    }
                }
                return id_procedure;
            }
        }

        public void CreateConservative(string id_staff, string id_patient, string id_procedure, string id_staff_nurce, DateTime date_procedure, string time_procedure) {
            
            //создание лечения
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into Сonservative (id_staff, id_patient, id_procedure, id_staff_nurce, date_procedure, time_procedure) VALUES " +
                    $"(@id_staff, @id_patient, @id_procedure, @id_staff_nurce, @date_procedure, @time_procedure);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        command.Parameters.Add("@id_procedure", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_procedure;
                        command.Parameters.Add("@id_staff_nurce", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff_nurce;
                        command.Parameters.Add("@date_procedure", NpgsqlTypes.NpgsqlDbType.Date).Value = date_procedure;
                        command.Parameters.AddWithValue("@time_procedure", Convert.ToDateTime(time_procedure));
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteConservative(string id_staff, string id_patient) {
            //удаление лечения
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from Сonservative where id_staff = @id_staff and id_patient = @id_patient;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void GetConservative(string id_staff, string id_patient, ComboBox cbProcedure, DataGridView dgvNurce, MonthCalendar mcDate, TextBox tbTime) {

            string name_nurce = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select date_procedure, time_procedure, t3.name_drocedure, t2.full_name from Сonservative t1 " +
                    $"join staff t2 on t1.id_staff_nurce = t2.id_staff join procedures_ t3 on t3.id_procedure = t1.id_procedure where t1.id_staff = '{id_staff}' and t1.id_patient = '{id_patient}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cbProcedure.SelectedItem = reader["name_drocedure"].ToString();
                            name_nurce = reader["full_name"].ToString();
                            mcDate.SelectionStart = Convert.ToDateTime(reader["date_procedure"].ToString());
                            mcDate.SelectionEnd = Convert.ToDateTime(reader["date_procedure"].ToString());
                            tbTime.Text = reader["time_procedure"].ToString();
                        }
                    }

                    foreach (DataGridViewRow row in dgvNurce.Rows) {
                        if (row.Cells[1].Value.ToString().Equals(name_nurce)) {
                            row.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        public void UpdateConservative(string id_staff, string id_patient, string id_procedure_old, string date_procedure_old, string time_procedure_old,
            string id_procedure, string id_staff_nurce, DateTime date_procedure, string time_procedure) {
            
            
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update Сonservative set id_procedure = @id_procedure, id_staff_nurce = @id_staff_nurce, date_procedure = @date_procedure, time_procedure = @time_procedure " +
                    $"where id_staff = @id_staff and id_patient = @id_patient and id_procedure = @id_procedure_old and date_procedure = @date_procedure_old and time_procedure = @time_procedure_old", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_procedure", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_procedure;
                        command.Parameters.Add("@id_staff_nurce", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff_nurce;
                        command.Parameters.Add("@date_procedure", NpgsqlTypes.NpgsqlDbType.Date).Value = date_procedure;
                        command.Parameters.AddWithValue("@time_procedure", Convert.ToDateTime(time_procedure));
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        command.Parameters.Add("@id_procedure_old", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_procedure_old;
                        command.Parameters.Add("@date_procedure_old", NpgsqlTypes.NpgsqlDbType.Date).Value = Convert.ToDateTime(date_procedure_old);
                        command.Parameters.AddWithValue("@time_procedure_old", NpgsqlTypes.NpgsqlDbType.Time, Convert.ToDateTime(time_procedure_old).TimeOfDay);
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }


            }
        }
    }
}
