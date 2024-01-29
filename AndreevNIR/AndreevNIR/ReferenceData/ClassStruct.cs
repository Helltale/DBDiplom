using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace AndreevNIR.ReferenceData
{
    class ClassStruct
    {
        public void CreateRoom(ComboBox cbNameDepartment, ComboBox cbNameHirDepartment ,NumericUpDown nudSits, TextBox tbNumber) {
            string id_department = null;
            string code_hir_department = null;
            string number_room = tbNumber.Text; 
            string sits = nudSits.Value.ToString(); 

            DBLogicConnection dB = new DBLogicConnection();
            
            //получение code_hir_department по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{cbNameHirDepartment.SelectedItem.ToString()}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            code_hir_department = reader["code_hir_department"].ToString();
                        }
                    }
                }
            }

            //получение id_department по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_department from type_department where name_department = '{cbNameDepartment.SelectedItem.ToString()}'", connection))
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

            //создание палаты
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO Room (number_room, code_hir_department, id_department, sit_empt, sit_bisy) VALUES (@number_room, @code_hir_department, @id_department, @sit_empt, @sit_bisy); ", connection))
                {
                    try
                    {
                        command.Parameters.Add("@number_room", NpgsqlTypes.NpgsqlDbType.Varchar).Value = number_room;
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = code_hir_department;
                        command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_department;
                        command.Parameters.Add("@sit_empt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = sits;
                        command.Parameters.Add("@sit_bisy", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "";
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public string[] GetStruct(ComboBox cbNameDepartment, ComboBox cbNameHirDepartment, NumericUpDown nudSits, TextBox tbNumber, string id_department, string code_hir_department, string number_room)
        {
            DBLogicConnection dB = new DBLogicConnection();
            string[] result = new string[4];

            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT name_hir_department, hir_hospital.code_hir_department, adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\", name_department ,department.id_department, (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room, room.sit_empt FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff where number_room = '{number_room}' and hir_hospital.code_hir_department = '{code_hir_department}' and department.id_department = '{id_department}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            try
                            {
                                cbNameDepartment.Text = reader["name_department"].ToString();
                                cbNameHirDepartment.Text = reader["name_hir_department"].ToString();
                                string tmp = reader["sit_empt"].ToString();
                                nudSits.Value = int.Parse(tmp);
                                tbNumber.Text = reader["number_room"].ToString();

                                
                                result[0] = tbNumber.Text;
                                result[1] = cbNameDepartment.Text;
                                result[2] = cbNameHirDepartment.Text;
                                result[3] = tmp;
                            }
                            catch (NpgsqlException ex){ ex.ToString(); }
                            
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
            return result;
        }

        public void UpdateStruct(string[] data, string cbNameDepartment, string cbNameHirDepartment, NumericUpDown nudSits, TextBox tbNumber) {
            string new_code_hir_department = null;
            string new_id_department = null;
            //string new_number_room = null;
            //string new_sit_empt = null;

            DBLogicConnection dB = new DBLogicConnection();

            //получение id_стационара по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{cbNameHirDepartment}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            new_code_hir_department = reader["code_hir_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //получение id_отделения по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_department from type_department where name_department = '{cbNameDepartment}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            new_id_department = reader["id_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //запрос обновления имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update room set number_room = @number_room, code_hir_department = @code_hir_department, id_department = @id_department, sit_empt = @sit_empt where number_room = '{data[0]}' and id_department = '{data[1]}' and code_hir_department = '{data[2]}' and sit_empt = '{data[3]}';", connection))
                {
                    try
                    {
                        command.Parameters.Add("@number_room", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbNumber.Text;
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = new_code_hir_department;
                        command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = new_id_department;
                        command.Parameters.Add("@sit_empt", NpgsqlTypes.NpgsqlDbType.Varchar).Value = nudSits.Value.ToString();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
