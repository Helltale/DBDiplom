using System;
using System.Collections.Generic;
using Npgsql;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

namespace AndreevNIR
{
    class CoreLogic
    {
        private List<string> GetDataForComboBoxDB(string connectionString, string query)
        {
            List<string> data = new List<string>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> row = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row.Add(reader[i].ToString());
                            }
                            data.Add(string.Join(",", row));
                        }
                    }
                }
            }
            return data;
        } //возвразает list<string> для комбобокса

        public void LoadComboboxByQuery(ComboBox cb, string sqlQuery, string strText)
        {
            List<string> listForComboBox = new List<string>();
            DBLogicConnection db = new DBLogicConnection();

            listForComboBox = GetDataForComboBoxDB(db._connectionString, sqlQuery);
            cb.DataSource = listForComboBox;
            cb.SelectedIndex = -1;
            cb.Text = strText;
        } //заполняет comboBox

        public void ShowDGV(string strQuery, DataGridView dgv, string connStr)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(strQuery, connStr);
            try
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgv.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        } //заполнение DGV

        public string GetLastIdFromQuery(string tableName, string IDname) {
            DBLogicConnection dB = new DBLogicConnection();
            string stringLastID = dB.GetLastId(dB._connectionString, tableName, IDname);
            Int64 intLastID = Int64.Parse(stringLastID);
            intLastID++;
            return intLastID.ToString();
        } //отдаёт последний id + 1

        public DateTime GetDatetimeFromString(string date) {
            string formattedDate = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-dd-MM");
            DateTime completeDate = Convert.ToDateTime(formattedDate);
            return completeDate;

        } //возвращает DateTime по стринге с датой

        public DateTime SetDatetimeOnlyDateForDB(DateTime date)
        {
            return DateTime.ParseExact(date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
        }

        public List<string> CreateFullListOfShowDGV(string query, List<string> data, char delimiter)
        {
            DBLogicConnection db = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string rowData = "";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowData += reader[i].ToString() + delimiter;
                            }

                        }
                    }
                }
            }
            return data;
        } //возвращает полные данные (не урезанные), для дальнейшей точной идентификации записи на dgv

        public List<string> CreateFullListOfShowDGV(string query, char delimiter)
        {
            List<string> data = new List<string>();
            DBLogicConnection db = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string rowData = "";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowData += reader[i].ToString() + delimiter;
                            }
                            data.Add(rowData);
                        }
                    }
                }
            }
            return data;
        } //перегрузка при проблемном верхнем методе... переписал, без входного листа со стрингами

        //не совсем понимаю как работали предыдущие методы, ведь он был написан неправильно, сейчас перепиши, потом если будет желание повозиться, то
        //перепишу код для тех фрагментов.
        public List<string> CreateFullListOfShowDGV1(string query, List<string> data, char delimiter)
        {
            DBLogicConnection db = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string rowData = "";
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowData += reader[i].ToString() + delimiter;
                            }
                            data.Add(rowData);
                        }
                    }
                }
            }
            return data;
        }

        public string GetElementFromListOfShowDGV(List<string> data, char delimiter, int indexRow, int indexColoumn)
        {
            string selectedRow = data[indexRow];
            string[] words = selectedRow.Split(delimiter);
            string substring = words[indexColoumn];
            return substring;
        } //возвращает sting для поиска

        
    }
}
