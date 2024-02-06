using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace AndreevNIR.OperationalData.Patients
{
    class ClassPatientAddLogic
    {
        public void CreatePatient(TextBox omc, TextBox diagnosis_enter, TextBox snils, TextBox full_name, TextBox allergy, ComboBox rh, ComboBox blood, RichTextBox additional_info, TextBox adress_real, 
            TextBox code_pass, DateTimePicker data_get, TextBox number_pass, TextBox who_give, TextBox tally_pass, TextBox adress_pass, DateTimePicker date_initial,
            TextBox time_initial, ComboBox doc_receptionist, TextBox diagnosis) {

            DBLogicConnection db = new DBLogicConnection();

            //создание пациента
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into patient (omc, diagnosis_enter, snils, full_name) values (@omc, @diagnosis_enter, @snils, @full_name);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc.Text;
                        command.Parameters.Add("@diagnosis_enter", NpgsqlTypes.NpgsqlDbType.Varchar).Value = diagnosis_enter.Text;
                        command.Parameters.Add("@snils", NpgsqlTypes.NpgsqlDbType.Varchar).Value = snils.Text;
                        command.Parameters.Add("@full_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = full_name.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }

            //создание доп инфы пациента
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into additional_information (omc, allergy, rh, blood, additional_info, adress_real) values (@omc, @allergy, @rh, @blood, @additional_info, @adress_real);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc.Text;
                        command.Parameters.Add("@allergy", NpgsqlTypes.NpgsqlDbType.Varchar).Value = allergy.Text;
                        command.Parameters.Add("@rh", NpgsqlTypes.NpgsqlDbType.Varchar).Value = rh.SelectedValue.ToString();
                        command.Parameters.Add("@blood", NpgsqlTypes.NpgsqlDbType.Varchar).Value = blood.SelectedValue.ToString();
                        command.Parameters.Add("@additional_info", NpgsqlTypes.NpgsqlDbType.Varchar).Value = additional_info.Text;
                        command.Parameters.Add("@adress_real", NpgsqlTypes.NpgsqlDbType.Varchar).Value = adress_real.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }

            //создание паспорта
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into passport (сode_pass, data_get, number_pass, who_give, tally_pass, adress_pass, id_staff, omc) " +
                    $"values ('{code_pass.Text}', '{data_get.Value.Date}', '{number_pass.Text}', '{who_give.Text}', '{tally_pass.Text}', '{adress_pass.Text}', @id_staff, '{omc.Text}');", connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@id_staff", DBNull.Value); //null
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }

            //создание первичного осмотра
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {

                DateTime newDateTime = new DateTime(date_initial.Value.Year, date_initial.Value.Month, date_initial.Value.Day,
                    Convert.ToInt32(time_initial.Text.Split(':')[0]), Convert.ToInt32(time_initial.Text.Split(':')[1]), Convert.ToInt32(time_initial.Text.Split(':')[2]));

                string doc_receptionist_id = GetIDReceptionistByName(doc_receptionist.SelectedItem.ToString());
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into initial_inspection (omc, date_initial, time_initial, doc_receptinoist, diagnosis) values (@omc, @date_initial, @time_initial, @doc_receptinoist, @diagnosis);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc.Text;
                        command.Parameters.Add("@date_initial", NpgsqlTypes.NpgsqlDbType.Date).Value = date_initial.Value.Date;
                        command.Parameters.Add("@time_initial", NpgsqlTypes.NpgsqlDbType.Time).Value = date_initial.Value.TimeOfDay;
                        command.Parameters.Add("@doc_receptinoist", NpgsqlTypes.NpgsqlDbType.Varchar).Value = doc_receptionist_id;
                        command.Parameters.Add("@diagnosis", NpgsqlTypes.NpgsqlDbType.Varchar).Value = diagnosis.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }

        }

        public void FullUpComboboxe(ComboBox cb, List<string> list, string cbText) {
            cb.DataSource = list;
            cb.Text = cbText;
        }

        private string GetIDReceptionistByName(string name) {
            DBLogicConnection db = new DBLogicConnection();
            string ID = null;


            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select t2.id_staff from receptionist t1 join staff t2 on t1.id_staff = t2.id_staff where full_name = '{name}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ID = reader["id_staff"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            } return ID;
        }

        public void DeletePatient(string omc) {
            DBLogicConnection db = new DBLogicConnection();

            //удаление первичного осмотра
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from initial_inspection where omc = '{omc}';", connection))
                {
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from passport where omc = '{omc}';", connection))
                {
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from additional_information where omc = '{omc}';", connection))
                {
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from patient where omc = '{omc}';", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePatient(string omc, TextBox txtName, TextBox txtSnils, TextBox txtOmc, TextBox txtDiagnEnter, TextBox txtAllergy, ComboBox cbRH, ComboBox cbBlood,
            RichTextBox txtAdditional, TextBox txtLive, TextBox txtSeria, TextBox txtNumber, DateTimePicker dtpDateGetPass, TextBox txtWhoGive, TextBox txtCodePodraz,
            TextBox txtPropis, TextBox txtDiagn, ComboBox cbReceptionist, DateTimePicker dtpDateInit, TextBox txtTimeInit) {

            DBLogicConnection db = new DBLogicConnection();

            
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update patient set omc = @omc, diagnosis_enter = @diagnosis_enter, snils = @snils, full_name = @full_name where omc = '{omc}';", connection))
                {
                    try
                    {
                        command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtOmc.Text;
                        command.Parameters.Add("@diagnosis_enter", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtDiagnEnter.Text;
                        command.Parameters.Add("@snils", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtSnils.Text;
                        command.Parameters.Add("@full_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtName.Text;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"update additional_information set allergy = @allergy, rh = @rh, blood = @blood, additional_info = @additional_info, adress_real = @adress_real where omc = '{txtOmc.Text}';", connection))
                {
                    try
                    {
                        command.Parameters.Add("@allergy", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtAllergy.Text;
                        command.Parameters.Add("@rh", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cbRH.Text;
                        command.Parameters.Add("@blood", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cbBlood.Text;
                        command.Parameters.Add("@additional_info", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtAdditional.Text;
                        command.Parameters.Add("@adress_real", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtLive.Text;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"update passport set сode_pass = @сode_pass, data_get = @data_get, number_pass = @number_pass, " +
                    $"who_give = @who_give, tally_pass = @tally_pass, adress_pass = @adress_pass where omc = '{txtOmc.Text}';", connection))
                {
                    try
                    {
                        command.Parameters.Add("@сode_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtSeria.Text;
                        command.Parameters.Add("@data_get", NpgsqlTypes.NpgsqlDbType.Date).Value = dtpDateGetPass.Value.Date;
                        command.Parameters.Add("@number_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtNumber.Text;
                        command.Parameters.Add("@who_give", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtWhoGive.Text;
                        command.Parameters.Add("@tally_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtCodePodraz.Text;
                        command.Parameters.Add("@adress_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtPropis.Text;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"update initial_inspection set date_initial = @date_initial, time_initial = @time_initial, " +
                    $"doc_receptinoist = @doc_receptinoist, diagnosis = @diagnosis where omc = '{txtOmc.Text}';", connection))
                {
                    DateTime newDateTime = new DateTime(dtpDateInit.Value.Year, dtpDateInit.Value.Month, dtpDateInit.Value.Day,
                    Convert.ToInt32(txtTimeInit.Text.Split(':')[0]), Convert.ToInt32(txtTimeInit.Text.Split(':')[1]), Convert.ToInt32(txtTimeInit.Text.Split(':')[2]));

                    string doc_receptionist_id = GetIDReceptionistByName(cbReceptionist.Text);

                    try
                    {
                        command.Parameters.Add("@date_initial", NpgsqlTypes.NpgsqlDbType.Date).Value = dtpDateInit.Value.Date;
                        command.Parameters.Add("@time_initial", NpgsqlTypes.NpgsqlDbType.Time).Value = newDateTime.TimeOfDay;
                        command.Parameters.Add("@doc_receptinoist", NpgsqlTypes.NpgsqlDbType.Varchar).Value = doc_receptionist_id;
                        command.Parameters.Add("@diagnosis", NpgsqlTypes.NpgsqlDbType.Varchar).Value = txtWhoGive.Text;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LoadPatient(string omc, TextBox txtName, TextBox txtSnils, TextBox txtOmc, TextBox txtDiagnEnter, TextBox txtAllergy, ComboBox cbRH, ComboBox cbBlood,
            RichTextBox txtAdditional, TextBox txtLive, TextBox txtSeria, TextBox txtNumber, DateTimePicker dtpDateGetPass, TextBox txtWhoGive, TextBox txtCodePodraz,
            TextBox txtPropis, TextBox txtDiagn, ComboBox cbReceptionist, DateTimePicker dtpDateInit, TextBox txtTimeInit) {

            DBLogicConnection db = new DBLogicConnection();

            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from patient where omc = '{omc}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtOmc.Text = reader["omc"].ToString();
                            txtDiagnEnter.Text = reader["diagnosis_enter"].ToString();
                            txtSnils.Text = reader["snils"].ToString();
                            txtName.Text = reader["full_name"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from additional_information where omc = '{omc}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtAllergy.Text = reader["allergy"].ToString();
                            cbRH.Text = reader["rh"].ToString();
                            cbBlood.Text = reader["blood"].ToString();
                            txtAdditional.Text = reader["additional_info"].ToString();
                            txtLive.Text = reader["adress_real"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from passport where omc = '{omc}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtSeria.Text = reader["сode_pass"].ToString();
                            dtpDateGetPass.Text = reader["data_get"].ToString();
                            txtNumber.Text = reader["number_pass"].ToString();
                            txtWhoGive.Text = reader["who_give"].ToString();
                            txtCodePodraz.Text = reader["tally_pass"].ToString();
                            txtPropis.Text = reader["adress_pass"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }

                string idDoc = null;

                using (NpgsqlCommand command = new NpgsqlCommand($"select * from initial_inspection join staff on initial_inspection.doc_receptinoist = staff.id_staff where omc = '{omc}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtDiagn.Text = reader["diagnosis"].ToString();
                            idDoc = reader["doc_receptinoist"].ToString();
                            //cbReceptionist.Text
                            dtpDateInit.Text = reader["date_initial"].ToString();
                            txtTimeInit.Text = reader["time_initial"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from staff where id_staff = '{idDoc}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cbReceptionist.Text = reader["full_name"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }

            }
        }

        

    }
}
