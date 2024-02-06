using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace AndreevNIR.OperationalData.Patients
{
    class ClassPatientInRoom
    {
        public void CreatePatietnInRoom(string omc, ComboBox cb1, ComboBox cb2, ComboBox cb3, ComboBox cb6) {
            CoreLogic cl = new CoreLogic();
            DBLogicConnection db = new DBLogicConnection();

            string last_id = cl.GetLastIdFromQuery("patient_in_room", "id_patient");
            string hir_id = null;
            string dep_id = null;
            string thera_id = null;
            DateTime date = DateTime.MinValue;
            
            

            //получение code_hir_dep
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = @name_hir_department", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cb1.SelectedItem.ToString();
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hir_id = reader["code_hir_department"].ToString();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }
            }
            //получение dep_id
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from type_department where name_department = @name_department", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cb2.SelectedItem.ToString();
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dep_id = reader["id_department"].ToString();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }
            }
            //получение date_init
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from initial_inspection where omc = @omc", connection))
                {
                    try
                    {
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc;
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                date = Convert.ToDateTime(reader["date_initial"].ToString());
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }
            }
            //получение therapist_id
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_staff from staff where full_name = @find", connection))
                {
                    try
                    {
                        command.Parameters.Add("@find", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cb6.SelectedItem.ToString();
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                thera_id = reader["id_staff"].ToString();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }
            }



            //кладем в палату
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into patient_in_room (id_patient, omc, code_hir_department, date_room, number_room, id_department) " +
                    $"values (@id_patient, @omc, @code_hir_department, @date_room, @number_room, @id_department);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = last_id;
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc;
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = hir_id;
                        command.Parameters.Add("@date_room", NpgsqlTypes.NpgsqlDbType.Date).Value = date.Date;
                        command.Parameters.Add("@number_room", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cb3.SelectedItem.ToString();
                        command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = dep_id;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into Doc_Patient (id_patient, id_staff) values " +
                    $"(@id_patient, @id_staff);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = last_id;
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = thera_id;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
