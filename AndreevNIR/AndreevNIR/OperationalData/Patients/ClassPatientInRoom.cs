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
        DBLogicConnection db = new DBLogicConnection();

        public void CreatePatietnInRoom(string omc, ComboBox cb1, ComboBox cb2, ComboBox cb3, ComboBox cb6) {
            CoreLogic cl = new CoreLogic();

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

        public void LoadPatietnInRoom(string id_patient, string omc, string hir_dep, string department, string room, DataGridView dgv, ComboBox cb1, ComboBox cb2, ComboBox cb3, ComboBox cb6) {
            DBLogicConnection db = new DBLogicConnection();
            
            cb1.SelectedItem = hir_dep;
            cb2.Text = department;
            cb3.Text = room;

            //получение therapist_id
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select t2.full_name from doc_patient t1 join staff t2 on t1.id_staff = t2.id_staff where id_patient = @find", connection))
                {
                    try
                    {
                        command.Parameters.Add("@find", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cb6.SelectedItem = reader["full_name"].ToString();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }
            }

        }

        public void LoadPatietnInRoomDGV(DataGridView dgv, string omc) { 

            //выделеине строки с пациентом
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Cells[0].Value.ToString() == omc)
                {
                    dgv.Rows[i].Selected = true;
                    break;
                }
            }
        }

        public void ChangePatientInRoom(string id_patient, string omc, ComboBox cb1, ComboBox cb2, ComboBox cb3, ComboBox cb6)
        {

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
                using (NpgsqlCommand command = new NpgsqlCommand($"update patient_in_room set omc = @omc, code_hir_department = @code_hir_department, " +
                    $"date_room = @date_room, number_room = @number_room, id_department = @id_department where id_patient = @id_patient;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc;
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = hir_id;
                        command.Parameters.Add("@date_room", NpgsqlTypes.NpgsqlDbType.Date).Value = date.Date;
                        command.Parameters.Add("@number_room", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cb3.SelectedItem.ToString();
                        command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = dep_id;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
                //изменение леч врача
                using (NpgsqlCommand command = new NpgsqlCommand($"update doc_patient set id_staff = @id_staff where id_patient = @id_patient;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = thera_id;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeletePatientInRoom(string id_patient) {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from doc_patient where id_patient = @id_patient;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
                
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from doc_patient where id_patient = @id_patient;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
