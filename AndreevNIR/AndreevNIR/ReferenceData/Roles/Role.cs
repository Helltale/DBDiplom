using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Roles
{
    class Role
    {
        DBLogicConnection db = new DBLogicConnection();
        public void DeleteUser(string login)
        {
            DBLogicConnection dB = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM user_info WHERE login_user = @login_user;", connection))
                {
                    command.Parameters.Add("@login_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = login;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    MessageBox.Show("Запись удалёна");
                }
            }
        }

        public void GetUser(string login, ComboBox cb1, ComboBox cb2)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from user_info where login_user = @login", connection))
                {
                    command.Parameters.Add("@login", NpgsqlTypes.NpgsqlDbType.Varchar).Value = login;
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cb1.SelectedItem = reader["role_user"];
                            cb1.Text = reader["role_user"].ToString();
                            cb2.SelectedItem = reader["trust_user"];
                            cb2.Text = reader["trust_user"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUser(string login, string id_staff_therapist, ComboBox cbRole, ComboBox cbTrust)
        {
            DBLogicConnection dB = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update user_info set id_staff = @id_staff, login_user = @login_user," +
                    $"role_user = @role_user, trust_user = @trust_user where login_user = @login_user", connection))
                {
                    //command.Parameters.Add("@id_staff", Np);
                    command.Parameters.Add("@login_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = login;
                    command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff_therapist;
                    command.Parameters.Add("@role_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cbRole.SelectedItem.ToString();
                    command.Parameters.Add("@trust_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cbTrust.SelectedItem.ToString();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    MessageBox.Show("Запись обновлена");
                }
            }
        }
    }
}
