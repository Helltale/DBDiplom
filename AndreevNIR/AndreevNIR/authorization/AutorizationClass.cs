using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;

namespace AndreevNIR.authorization
{
    class AutorizationClass
    {
        private bool CheckPassword(TextBox tb1, TextBox tb2) {
            bool res = false;
            if (tb1.Text != tb2.Text) {
                res = true;
            }
            return res;
        }

        public bool CheckPasswordGenerateMessage(TextBox tb1, TextBox tb2) {
            bool error = CheckPassword(tb1, tb2);
            if (error == true) {
                MessageBox.Show("Пароли не совпадают!");
            }
            return error;
        }

        public void CreateNewUser(TextBox tbLogin, TextBox tbPass, ComboBox cbJob)
        {
            DBLogicConnection db = new DBLogicConnection();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO user_info (id_staff, login_user, password_user, role_user, trust_user) VALUES " +
                        $"(@id_staff, @login_user, @password_user, @role_user, @trust_user)", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = DBNull.Value;
                            command.Parameters.Add("@login_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbLogin.Text;
                            command.Parameters.Add("@password_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbPass.Text;
                            command.Parameters.Add("@role_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Convert.ToString(cbJob.SelectedIndex+1);
                            command.Parameters.Add("@trust_user", NpgsqlTypes.NpgsqlDbType.Varchar).Value = 'n';
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //добавление сотрудника
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось зарегистрировать сотрудника:\n" + ex.Message); }
            
        }
    }
}
