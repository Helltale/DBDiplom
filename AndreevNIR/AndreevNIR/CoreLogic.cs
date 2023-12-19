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
    }
}
