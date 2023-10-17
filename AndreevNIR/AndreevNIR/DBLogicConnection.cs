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
        public string _connectionString = "Host=localhost;Username=postgres;Password=;Database=postgres";

        public NpgsqlConnection ConnectToPostgres()
        {
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
    }
}
