using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Documents.ListNotWorking
{
    class ClassListNotWorking
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        public void CreateListNotWorking(string omc, string number_extract, MonthCalendar mDateOut, TextBox tbTime) {
            //string lastID = cl.GetLastIdFromQueryCast("List_not_working", "id_not_working_initial");
            DateTime dateIn = DateTime.Now;


            //получение dateIn
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select date_initial from initial_inspection where omc = '{omc}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dateIn = Convert.ToDateTime(reader["date_initial"].ToString());
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }


            //создание extract_document
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into List_not_working (numb_extract, date_in, date_extract_to, omc) values " +
                    $"(@numb_extract, @date_in, @date_extract_to, @omc)", connection))
                {
                    try
                    {
                        //DateTime newDateTime = new DateTime(date.Year, date.Month, date.Day,
                        //Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1]), Convert.ToInt32(time.Split(':')[2]));

                        command.Parameters.Add("@numb_extract", NpgsqlTypes.NpgsqlDbType.Varchar).Value = number_extract;
                        command.Parameters.Add("@date_in", NpgsqlTypes.NpgsqlDbType.Date).Value = dateIn;
                        command.Parameters.Add("@date_extract_to", NpgsqlTypes.NpgsqlDbType.Date).Value = mDateOut.SelectionStart;
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc;
                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public string GetNumberExtract(string omc) {
            string number_extract = null;

            //получение NUMBER_EXTRACT;
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select numb_extract from extract_document t1 join patient_in_room t2 on t1.id_patient = t2.id_patient where omc = '{omc}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            number_extract = reader["numb_extract"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
            return number_extract;
        }

    }
}
