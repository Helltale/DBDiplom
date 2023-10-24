using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Data;

namespace AndreevNIR
{
    class DBLogicConnection
    {
        /*
         Распределение на роли:
        "1" - руководитель отделения
        "2" - постовая мед сестра
        "3" - мед сестра приёмного покоя
        "4" - врач
        "5" - оператор
        "6" - админ?????
         */

        public string _connectionString = "Host=localhost;Username=postgres;Password=64210369;Database=postgres";
        public string conHost = "localhost";
        public string conUsername = "postgres";
        public string conPassword = "64210369";
        public string conDB = "postgres";

        public bool tmpFlag = false;

        NpgsqlConnection connection = new NpgsqlConnection();

        public void OpenSQLConnection(string connStr) {
            try
            {
                connection = new NpgsqlConnection(connStr);
                connection.Open();
                MessageBox.Show("Подключение к PostgreSQL успешно установлено.");
            }
            catch(Exception ex) { MessageBox.Show($"Ошибка при подключении к PostgreSQL: \n{ex.Message}"); }
        }

        public void CreateQueryLogIn(string login_, string password_) {
            string queryLogIn = "select id_staff, login_user, password_user, role_user " +
                "from User_info where login_user = @l and password_user = @p";
            NpgsqlCommand command = new NpgsqlCommand(queryLogIn, connection);

            command.Parameters.Add("@l", NpgsqlTypes.NpgsqlDbType.Varchar).Value = login_;
            command.Parameters.Add("@p", NpgsqlTypes.NpgsqlDbType.Varchar).Value = password_;

            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string idStaff = reader["id_staff"].ToString();
                string roleStaff = reader["role_user"].ToString();
                MessageBox.Show("Добро пожаловать!\nID сотрудника: " + idStaff);
                FormAutorization form = new FormAutorization();
                tmpFlag = true;

                SessionInformation si = new SessionInformation();
                si.userID = idStaff;
                si.userRole = roleStaff;
            }
            else {
                MessageBox.Show("Некорректные данные входа!");
            }


            /*adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0) { MessageBox.Show("Авторизация прошла успешно"); return true; }
            else { MessageBox.Show("Ошибка авторизации"); return false; }*/


        }

        public void SetNewConnectionString(string conHost_, string conUsername_, string conPassword_, string conDB_) { //переписать, не работают
            _connectionString = "Host=" + conHost_ + ";Username=" + conUsername_ + ";Password=" + conPassword_ + ";Database=" + conDB_;
            conHost = conHost_;
            conUsername = conUsername_;
            conPassword = conPassword_;
            conDB = conDB_;
        }
        public void SetNewConnectionStringDefault() //переписать, не работают
        {
            string defHost = "localhost";        conHost = defHost;
            string defUserName = "postgres";     conUsername = defUserName;
            string defPassword = "";             conPassword = defPassword;
            string defDB = "postgres";           conDB = defDB;

            _connectionString = "Host=" + conHost + ";Username=" + conUsername + ";Password=" + conPassword + ";Database=" + conDB;

        }
    }
}
