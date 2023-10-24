using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

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
         */

        public string _connectionString = "Host=localhost;Username=postgres;Password=;Database=postgres";
        public string conHost = "localhost";
        public string conUsername = "postgres";
        public string conPassword = "";
        public string conDB = "postgres";

        public NpgsqlConnection ConnectToPostgres()
        {
            _connectionString = "Host="+ conHost + ";Username="+ conUsername + ";Password="+ conPassword + ";Database=" + conDB;
            try
            {
                var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                MessageBox.Show("Подключение к PostgreSQL успешно установлено.");
                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении к PostgreSQL: \n{ex.Message}");
                return null;
            }
        }

        public void SetNewConnectionString(string conHost_, string conUsername_, string conPassword_, string conDB_) {
            _connectionString = "Host=" + conHost_ + ";Username=" + conUsername_ + ";Password=" + conPassword_ + ";Database=" + conDB_;
            conHost = conHost_;
            conUsername = conUsername_;
            conPassword = conPassword_;
            conDB = conDB_;
        }
        public void SetNewConnectionStringDefault()
        {
            string defHost = "localhost";        conHost = defHost;
            string defUserName = "postgres";     conUsername = defUserName;
            string defPassword = "";             conPassword = defPassword;
            string defDB = "postgres";           conDB = defDB;

            _connectionString = "Host=" + conHost + ";Username=" + conUsername + ";Password=" + conPassword + ";Database=" + conDB;

        }
    }
}
