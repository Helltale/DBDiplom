using Npgsql;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreevNIR.ReferenceData.Struct
{
    class ClassHospital
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection dB = new DBLogicConnection();


        public string GetHospital(string nameHirDepartment, TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, ComboBox comboBox1)
        {
            string idHirDepartment_ = null;
            string idBossHirDepartment = null;

            //получение id стационара по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{nameHirDepartment}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idHirDepartment_ = reader["code_hir_department"].ToString();
                        }
                    }
                }
            }

            //вывод всех значений в существующие поля
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from hir_hospital where code_hir_department = '{idHirDepartment_}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            textBox4.Text = reader["name_hir_department"].ToString();
                            textBox3.Text = reader["adress_hir_department"].ToString();
                            textBox1.Text = reader["phone_hir_department"].ToString();
                            idBossHirDepartment = reader["boss_hir_department"].ToString();
                            textBox2.Text = reader["ogrm_hir_department"].ToString();
                        }
                    }
                }

            }

            //вывод вывод босса в комбобокс по его id
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select full_name from staff where id_staff = '{idBossHirDepartment}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            comboBox1.Text = reader["full_name"].ToString();
                        }
                    }
                }

            }

            return idHirDepartment_;
        }

        public static string FindIDByName(List<string> records, char delimeter, string name)
        {
            foreach (string record in records)
            {
                string[] parts = record.Split(delimeter);
                if (parts[1].Trim() == name)
                {
                    return parts[0].Trim();
                }
            }
            return null;
        }

        public void CreateHospital(TextBox textBox4, TextBox textBox3, TextBox textBox1, TextBox textBox2, ComboBox boss_hir_department_id_cb) {
            DBLogicConnection dB = new DBLogicConnection();
            string stringLastID = dB.GetLastId(dB._connectionString, "Hir_hospital", "code_hir_department");
            int intLastID = int.Parse(stringLastID);
            intLastID++;
            stringLastID = intLastID.ToString();

            string boss_hir_department_id = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                //find boss_hir_department_id
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from staff where full_name = @full_name)", connection))
                {
                    try
                    {
                        command.Parameters.Add("@full_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = boss_hir_department_id_cb.SelectedItem.ToString();
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                boss_hir_department_id = reader["id_staff"].ToString();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }

                using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO Hir_hospital (code_hir_department, name_hir_department, adress_hir_department, boss_hir_department, phone_hir_department, ogrm_hir_department) VALUES (@code_hir_department, @name_hir_department, @adress_hir_department, @boss_hir_department, @phone_hir_department, @ogrm_hir_department)", connection))
                {
                    try
                    {
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = stringLastID;
                        command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox4.Text;
                        command.Parameters.Add("@adress_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox3.Text;

                        
                        command.Parameters.Add("@boss_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = boss_hir_department_id;

                        command.Parameters.Add("@phone_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox1.Text;
                        command.Parameters.Add("@ogrm_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox2.Text;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Запись добавлена");
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }
            }
        }

        public void ChangeHospital(ComboBox comboBox1, TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, string idHirDepartment)
        {
            FormAddStruct1 fas1 = new FormAddStruct1();
            string idBossHirDepartment = null;

            //получим id управленца по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_Staff from staff where full_name = '{comboBox1.Text}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idBossHirDepartment = reader["id_Staff"].ToString();
                        }
                    }
                }
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update hir_hospital set name_hir_department = @name_hir_department, adress_hir_department = @adress_hir_department, boss_hir_department = @boss_hir_department, phone_hir_department = @phone_hir_department, ogrm_hir_department = @ogrm_hir_department where code_hir_department = @id", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox4.Text;
                        command.Parameters.Add("@adress_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox3.Text;
                        command.Parameters.Add("@boss_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idBossHirDepartment;
                        command.Parameters.Add("@phone_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox1.Text;
                        command.Parameters.Add("@ogrm_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox2.Text;

                        command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idHirDepartment;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Запись обновлена");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
    }
}
