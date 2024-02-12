using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Documents.Initial
{
    class ClassInitial
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        public void GetInitial(string omc, TextBox tbName, TextBox tbDiagnosis, ComboBox cbStaff, DateTimePicker dtpDate, TextBox tbTime)
        {

            string id_patient = null;   //думаю не нужно менять пока что
            string id_staff = null;     //думаю не нужно менять пока что
                                        //потому что тогда проще удалить и создать по новому документ



            //получение полей по numb_extract
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select pa.omc, pa.full_name as \"full_patient\", i.date_initial as \"date\", i.time_initial as \"time\", s.full_name as \"full_staff\", s.id_staff, " +
                    $"i.diagnosis as \"diagnosis\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist where pa.omc = '{omc}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbName.Text = reader["full_patient"].ToString();
                            tbDiagnosis.Text = reader["diagnosis"].ToString();
                            cbStaff.SelectedItem = reader["full_staff"].ToString();
                            dtpDate.Value = Convert.ToDateTime(reader["date"].ToString());
                            tbTime.Text = reader["time"].ToString();
                            
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }

        public void ChangeInitial(string omc, TextBox tbDiagnosis, ComboBox cbStaff, DateTimePicker dtpDate, TextBox tbTime) {
            
            string id_patient = null;   
            string id_staff = null;     



            //получение staff_id по name
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_staff from staff where full_name = '{cbStaff.SelectedItem.ToString()}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_staff = reader["id_staff"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //изменение полей по numb_extract
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update initial_inspection set date_initial = @date_initial, time_initial = @time_initial, doc_receptinoist = @doc_receptinoist, " +
                    $"diagnosis = @diagnosis where omc = '{omc}';", connection))
                {
                    try
                    {
                        DateTime newDateTime = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day,
                        Convert.ToInt32(tbTime.Text.Split(':')[0]), Convert.ToInt32(tbTime.Text.Split(':')[1]), Convert.ToInt32(tbTime.Text.Split(':')[2]));

                        command.Parameters.Add("@date_initial", NpgsqlTypes.NpgsqlDbType.Date).Value = dtpDate.Value;
                        command.Parameters.Add("@time_initial", NpgsqlTypes.NpgsqlDbType.Time).Value = newDateTime.TimeOfDay;
                        command.Parameters.Add("@doc_receptinoist", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                        command.Parameters.Add("@diagnosis", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbDiagnosis.Text;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                    //command.ExecuteNonQuery();
                }
            }
        }

    }
}
