using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace AndreevNIR.ReferenceData.FormAddStructF
{
    class ClassDepartment
    {
        private string nameDepartment;

        public ClassDepartment() { }

        public ClassDepartment(string nameDepartment_) {
            nameDepartment = nameDepartment_;
        }

        public void CreateDepartment(TextBox tbName, ComboBox cbBoss, ComboBox cbHirHosp) {
            string id_staff = null;
            string code_hir_department = null;


            DBLogicConnection dB = new DBLogicConnection();
            string stringLastID = dB.GetLastIdCast(dB._connectionString, "type_department", "id_department");
            // tmp = dB.GetLastIdCast(dB._connectionString, "type_department", "id_department");

            int intLastID = int.Parse(stringLastID);
            intLastID++;
            stringLastID = intLastID.ToString(); //id_department

            //создание type_department
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into type_department (id_department, name_department) values (@id, @name);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = stringLastID;
                        command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbName.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }

            //получение id_заведущего по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_staff from staff where full_name = '{cbBoss.SelectedItem.ToString()}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_staff = reader["id_staff"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //получение id_стационара по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{cbHirHosp.SelectedItem.ToString()}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            code_hir_department = reader["code_hir_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //создание department
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into department (code_hir_department, id_department, boss_department) values (@code_hir_department, @id_department, @boss_department);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = code_hir_department;
                        command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = stringLastID;
                        command.Parameters.Add("@boss_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDepartment(TextBox tbName, ComboBox cbBoss, ComboBox cbHirHosp, string idDepartment)
        {
            string id_staff = null;
            string code_hir_department = null;
            string id_department = idDepartment;

            DBLogicConnection dB = new DBLogicConnection();
            
            //получение id_заведущего по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_staff from staff where full_name = '{cbBoss.SelectedItem.ToString()}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_staff = reader["id_staff"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //получение id_стационара по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{cbHirHosp.SelectedItem.ToString()}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            code_hir_department = reader["code_hir_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //запрос обновления имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update type_department set name_department = @name_department where id_department = '{id_department}';", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbName.Text;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }

            //запрос обновления отделения
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update department set code_hir_department = @code_hir_department, boss_department = @boss_department where id_department = '{id_department}';", connection))
                {
                    try
                    {
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = code_hir_department;
                        command.Parameters.Add("@boss_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetDepartment(TextBox tbName, ComboBox cbBoss, ComboBox cbHirHosp) {
            string id_department = null;


            DBLogicConnection dB = new DBLogicConnection();

            //получение id_department по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_department from type_department where name_department = '{nameDepartment}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_department = reader["id_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }


            //получение всего остального по id_department
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select t1.name_department, t3.name_hir_department, t4.full_name from type_department t1 join department t2 on t1.id_department = t2.id_department join hir_hospital t3 on t2.code_hir_department = t3.code_hir_department join staff t4 on t2.boss_department = t4.id_staff where t1.id_department = '{id_department}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbName.Text = reader["name_department"].ToString();
                            cbBoss.Text = reader["full_name"].ToString();
                            cbHirHosp.Text = reader["name_hir_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
            return id_department;
        }

        public void DeleteDepartment(){
            string id_department = null;

            DBLogicConnection dB = new DBLogicConnection();

            //получение id_department по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_department from type_department where name_department = '{nameDepartment}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_department = reader["id_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //запрос на удаление
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from type_department where id_department = @id_department;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_department;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
