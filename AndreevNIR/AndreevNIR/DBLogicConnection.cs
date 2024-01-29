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

        public string _connectionString = "Host=localhost;Username=postgres;Password=64210369;Database=andreev1";
        public string conHost = "localhost";
        public string conUsername = "postgres";
        public string conPassword = "64210369";
        public string conDB = "andreev1";

        public bool tmpFlag = false;

        NpgsqlConnection connection = new NpgsqlConnection();

        public void OpenSQLConnection(string connStr) {
            try
            {
                connection = new NpgsqlConnection(connStr);
                connection.Open();
                //MessageBox.Show("Подключение к PostgreSQL успешно установлено.");
            }
            catch(Exception ex) { MessageBox.Show($"Ошибка при подключении к PostgreSQL: \n{ex.Message}"); }
        }

        public void CreateQueryLogIn(string login_, string password_) {
            string queryLogIn = "select u.id_staff, login_user, password_user, role_user, full_name " +
                "from User_info u " +
                "join staff s on s.id_staff = u.id_staff " +
                "where login_user = @l and password_user = @p";
            NpgsqlCommand command = new NpgsqlCommand(queryLogIn, connection);

            command.Parameters.Add("@l", NpgsqlTypes.NpgsqlDbType.Varchar).Value = login_;
            command.Parameters.Add("@p", NpgsqlTypes.NpgsqlDbType.Varchar).Value = password_;

            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string idStaff = reader["id_staff"].ToString();
                string roleStaff = reader["role_user"].ToString();
                string nameStaff = reader["full_name"].ToString();
                MessageBox.Show("Добро пожаловать "+ nameStaff + "!\nID сотрудника: " + idStaff);
                FormAutorization form = new FormAutorization();
                tmpFlag = true;

                FormIndex2 f2 = new FormIndex2();
                SessionInformation si = new SessionInformation();
                f2.GetLabelData(nameStaff, idStaff);
                si.userID = idStaff;
                si.userRole = roleStaff;
                si.userName = nameStaff;
            }
            else {
                MessageBox.Show("Некорректные данные входа!");
            }

        }

        //возвращает последний id
        public string GetLastId(string connectionString, string tableName, string IDname)
        {
            string lastId = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT MAX({IDname}::varchar) AS last_id FROM {tableName}", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lastId = reader["last_id"].ToString();
                        }
                    }
                }
            }
            return lastId;
        }

        public string GetLastIdCast(string connectionString, string tableName, string IDname)
        {
            string lastId = null;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT MAX(CAST({IDname} as int)) AS last_id FROM {tableName}", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lastId = reader["last_id"].ToString();
                        }
                    }
                }
            }
            return lastId;
        }

        //connection str
        public NpgsqlConnection GetConnection() {
            return new NpgsqlConnection(_connectionString);
        }
        

        public string SetNewConnectionString(string host, string user, string password, string db) { //ручная настройка //всё ещё не работает
            string connectionString = "Host="+ host + ";Username="+ user + ";Password="+ password + ";Database="+ db;
            return connectionString;
        }
        public void SetNewConnectionStringDefault() //дефолт
        {
           
        }
    }
}
