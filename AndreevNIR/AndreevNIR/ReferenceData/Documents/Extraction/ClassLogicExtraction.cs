using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Documents.Extraction
{
    class ClassLogicExtraction
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        public void CreateExtraction(string number, string id_staff, string id_patient, DateTime date, string time, string diagnosis, string recomendations, bool death) {
            //string lastID = cl.GetLastIdFromQueryCast("extract_document", "numb_extract");


            //создание extract_document
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into Extract_document (numb_extract, id_patient, id_staff, date_extract, time_extract, diagnosis_extract, recomendations, death_mark) values " +
                    $"(@numb_extract, @id_patient, @id_staff, @date_extract, @time_extract, @diagnosis_extract, @recomendations, @death_mark);", connection))
                {
                    try
                    {
                        DateTime newDateTime = new DateTime(date.Year, date.Month, date.Day,
                        Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1]), Convert.ToInt32(time.Split(':')[2]));

                        command.Parameters.Add("@numb_extract", NpgsqlTypes.NpgsqlDbType.Varchar).Value = number;
                        command.Parameters.Add("@id_patient", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_patient;
                        command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                        command.Parameters.Add("@date_extract", NpgsqlTypes.NpgsqlDbType.Date).Value = date;
                        command.Parameters.Add("@time_extract", NpgsqlTypes.NpgsqlDbType.Time).Value = newDateTime.TimeOfDay;
                        command.Parameters.Add("@diagnosis_extract", NpgsqlTypes.NpgsqlDbType.Varchar).Value = diagnosis;
                        command.Parameters.Add("@recomendations", NpgsqlTypes.NpgsqlDbType.Varchar).Value = recomendations;
                        if (death) {
                            command.Parameters.Add("@death_mark", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "+";
                        } else {
                            command.Parameters.AddWithValue("@death_mark", DBNull.Value);
                        }
                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }

        }

        public void DeleteExtraction(string numb_extract) {
            //удаление extract_document
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from extract_document where numb_extract = '{numb_extract}';", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void GetExtraction(string numb_extract_old, TextBox tbNumb_extract, TextBox tbDiagnosis, RichTextBox rRecomendations, MonthCalendar mDate, TextBox tbTime, CheckBox cbDeath) {
            
            string id_patient = null;   //думаю не нужно менять пока что
            string id_staff = null;     //думаю не нужно менять пока что
                                        //потому что тогда проще удалить и создать по новому документ



            //получение полей по numb_extract
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from extract_document where numb_extract = '{numb_extract_old}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbNumb_extract.Text = reader["numb_extract"].ToString();
                            tbDiagnosis.Text = reader["diagnosis_extract"].ToString();
                            tbTime.Text = reader["time_extract"].ToString();
                            rRecomendations.Text = reader["recomendations"].ToString();
                            if(reader["death_mark"].ToString() == "+")
                            {
                                cbDeath.Checked = true;
                            }
                            mDate.SelectionStart = Convert.ToDateTime(reader["date_extract"]);
                            mDate.SelectionEnd = Convert.ToDateTime(reader["date_extract"]);
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }

        public void ChangeExtraction(string numb_extract_old, string number, DateTime date, string time, string diagnosis, string recomendations, bool death) {
            
            
            //изменение extract_document
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update extract_document set date_extract = @date_extract, time_extract = @time_extract, " +
                    $"diagnosis_extract = @diagnosis_extract, recomendations = @recomendations, death_mark = @death_mark where numb_extract = @numb_extract_old", connection))
                {
                    try
                    {
                        DateTime newDateTime = new DateTime(date.Year, date.Month, date.Day,
                        Convert.ToInt32(time.Split(':')[0]), Convert.ToInt32(time.Split(':')[1]), Convert.ToInt32(time.Split(':')[2]));

                        command.Parameters.Add("@numb_extract_old", NpgsqlTypes.NpgsqlDbType.Varchar).Value = numb_extract_old;
                        command.Parameters.Add("@date_extract", NpgsqlTypes.NpgsqlDbType.Date).Value = date;
                        command.Parameters.Add("@time_extract", NpgsqlTypes.NpgsqlDbType.Time).Value = newDateTime.TimeOfDay;
                        command.Parameters.Add("@diagnosis_extract", NpgsqlTypes.NpgsqlDbType.Varchar).Value = diagnosis;
                        command.Parameters.Add("@recomendations", NpgsqlTypes.NpgsqlDbType.Varchar).Value = recomendations;
                        if (death)
                        {
                            command.Parameters.Add("@death_mark", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "+";
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@death_mark", DBNull.Value);
                        }
                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
