using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Staff
{
    class ClassStaff
    {
        CoreLogic cl = new CoreLogic();

        public void CreateStaff(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7, TextBox textBox9, TextBox textBox10, TextBox textBox11, DateTimePicker dateTimePicker1,
            RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton4, RadioButton radioButton5, RadioButton radioButton6, RadioButton radioButton7, ComboBox comboBox1, ComboBox comboBox2)
        {
            FormReferenceData frd = new FormReferenceData();
            DBLogicConnection db = new DBLogicConnection();

            string lastID = cl.GetLastIdFromQuery("Staff", "id_staff");
            string fio = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
            string pass_seria = textBox4.Text;
            string pass_number = textBox5.Text;
            string pass_tally = textBox6.Text;
            string pass_who_give = textBox7.Text;
            string pass_adrr = textBox9.Text;
            string pho = textBox10.Text;
            string mai = textBox11.Text;

            DateTime pass_when_give = dateTimePicker1.Value;
            DateTime pass_when_give_new = cl.SetDatetimeOnlyDateForDB(pass_when_give);

            string job = null;
            if (radioButton1.Checked) { job = "therapist"; }
            if (radioButton2.Checked) { job = "receptionist"; }
            if (radioButton4.Checked) { job = "nurce"; }
            if (radioButton5.Checked) { job = "dep_boss"; }
            if (radioButton6.Checked) { job = "hir_hosp_boss"; }
            if (radioButton7.Checked) { job = "big_boss"; }

            string hir = null;
            string hir_id = null;
            try { hir = comboBox1.SelectedItem.ToString(); }
            catch { MessageBox.Show("Выберите стационар"); }

            string dep = null;
            string dep_id = null;
            try { dep = comboBox2.SelectedItem.ToString(); }
            catch { MessageBox.Show("Выберите отделение"); }

            //================основные методы================
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = @name_hir_department", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = hir;
                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    hir_id = reader["code_hir_department"].ToString();
                                }
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //получение code_hir_dep
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удаловь найти id выбранного стационара:\n" + ex.Message); }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"select * from type_department where name_department = @name_department", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@name_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = dep;
                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    dep_id = reader["id_department"].ToString();
                                }
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //получение dep_id
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удаловь найти id выбранного отделения:\n" + ex.Message); }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO Staff (id_staff, full_name, phone, id_department, code_hir_department, mail) VALUES (@id_staff, @full_name, @phone, @id_department, @code_hir_department, @mail)", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lastID;
                            command.Parameters.Add("@full_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fio;
                            command.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pho;
                            command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = dep_id;
                            command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = hir_id;
                            command.Parameters.Add("@mail", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mai;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //добавление сотрудника
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось добавить сотрудника:\n" + ex.Message); }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO passport (сode_pass, data_get, number_pass, who_give, tally_pass, adress_pass, id_staff, omc) VALUES (@сode_pass, @data_get, @number_pass, @who_give, @tally_pass, @adress_pass, @id_staff, null)", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@сode_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_number;
                            command.Parameters.Add("@data_get", NpgsqlTypes.NpgsqlDbType.Date).Value = pass_when_give_new;
                            command.Parameters.Add("@number_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_seria;
                            command.Parameters.Add("@who_give", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_who_give;
                            command.Parameters.Add("@tally_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_tally;
                            command.Parameters.Add("@adress_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_adrr;
                            command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lastID;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //добавление создание для него паспорта
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось добавить паспортные данные для сотрудника:\n" + ex.Message); }

            try
            {
                if (job != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                    {
                        connection.Open();
                        using (NpgsqlCommand command = new NpgsqlCommand($"Insert into {job} Values (@id_staff)", connection))
                        {
                            try
                            {
                                command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lastID;
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex) { MessageBox.Show("" + ex); }
                        }
                    } //добавление в таблицу с должностями
                }
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось назначить сотрудника на должность:\n" + ex.Message); }
        }

        public void DeleteStaff(string staffID) {

            DBLogicConnection dB = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM staff WHERE id_staff = @staffID;", connection))
                {
                    command.Parameters.Add("@staffID", NpgsqlTypes.NpgsqlDbType.Varchar).Value = staffID;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    MessageBox.Show("Сотрудник удалён");
                }
            }
        }

        public string GetStaff(string idStaff, TextBox textBox10, TextBox textBox11, TextBox textBox1, TextBox textBox2, TextBox textBox3, RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton4, RadioButton radioButton5,
            RadioButton radioButton7, RadioButton radioButton6, TextBox  textBox9, TextBox textBox7, TextBox textBox6, TextBox textBox5, TextBox textBox4, DateTimePicker dateTimePicker1, ComboBox comboBox1, ComboBox comboBox2)
        {
            string oldJob = null;
            DBLogicConnection db = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();

                //общая
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT staff.id_staff as \"ID\", staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff WHERE staff.id_staff = '{idStaff}';", connection))
                {
                    try
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            string fio = null;
                            string job = null;
                            if (reader.Read())
                            {
                                fio = reader["ФИО"].ToString();
                                textBox10.Text = reader["Телефон"].ToString();
                                textBox11.Text = reader["Почта"].ToString();
                                job = reader["Должность"].ToString();
                            }
                            string[] fioParts = fio.Split(' ');
                            textBox1.Text = fioParts[0];
                            textBox2.Text = fioParts[1];
                            try { textBox3.Text = fioParts[2]; } catch { }
                            switch (job)
                            {
                                case "Врач":
                                    radioButton1.Checked = true;
                                    oldJob = "therapist";
                                    break;
                                case "Врач приёмного покоя":
                                    radioButton2.Checked = true;
                                    oldJob = "receptionist";
                                    break;
                                case "Медицинская сестра":
                                    radioButton4.Checked = true;
                                    oldJob = "nurce";
                                    break;
                                case "Заведующий отделения":
                                    radioButton5.Checked = true;
                                    oldJob = "dep_boss";
                                    break;
                                case "Главный врач":
                                    radioButton7.Checked = true;
                                    oldJob = "hir_hosp_boss";
                                    break;
                                case "Начальник больницы":
                                    radioButton6.Checked = true;
                                    oldJob = "big_boss";
                                    break;
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }

                //паспорт
                using (NpgsqlCommand command = new NpgsqlCommand($"select data_get, number_pass, сode_pass as \"code1\", who_give, tally_pass, adress_pass, id_staff from passport where id_staff = '{idStaff}';", connection))
                {
                    try
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                textBox4.Text = reader["number_pass"].ToString();
                                textBox5.Text = reader["code1"].ToString();
                                textBox6.Text = reader["tally_pass"].ToString();
                                textBox7.Text = reader["who_give"].ToString();
                                dateTimePicker1.Value = reader.GetDateTime(0);
                                textBox9.Text = reader["adress_pass"].ToString();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }

                //место работы
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT name_hir_department, name_department FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff WHERE staff.id_staff = '{idStaff}';", connection))
                {
                    try
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.SelectedItem = reader["name_hir_department"].ToString();
                                //cl.LoadComboboxByQuery(comboBox2, "select td.name_department from department d join type_department td on d.id_department = td.id_department join hir_hospital h on d.code_hir_department = h.code_hir_department where name_hir_department = '{tmpHirHospital}'", "Выберите отделение");
                                comboBox2.Text = reader["name_department"].ToString();
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                }
            }
            return oldJob;
        }

        public void UpdateStaff(string idStaff,string oldJob, TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7, TextBox textBox9, TextBox textBox10, TextBox textBox11, DateTimePicker dateTimePicker1, 
            RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton4, RadioButton radioButton5, RadioButton radioButton6, RadioButton radioButton7, ComboBox comboBox1, ComboBox comboBox2)
        {
            FormReferenceData frd = new FormReferenceData();
            DBLogicConnection db = new DBLogicConnection();

            string fio = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
            string pass_seria = textBox4.Text;
            string pass_number = textBox5.Text;
            string pass_tally = textBox6.Text;
            string pass_who_give = textBox7.Text;
            string pass_adrr = textBox9.Text;
            string pho = textBox10.Text;
            string mai = textBox11.Text;

            DateTime pass_when_give = dateTimePicker1.Value;
            DateTime pass_when_give_new = cl.SetDatetimeOnlyDateForDB(pass_when_give);

            string job = null;
            if (radioButton1.Checked) { job = "therapist"; }
            if (radioButton2.Checked) { job = "receptionist"; }
            if (radioButton4.Checked) { job = "nurce"; }
            if (radioButton5.Checked) { job = "dep_boss"; }
            if (radioButton6.Checked) { job = "hir_hosp_boss"; }
            if (radioButton7.Checked) { job = "big_boss"; }

            string hir = null;
            string hir_id = null;
            try { hir = comboBox1.SelectedItem.ToString(); }
            catch { MessageBox.Show("Выберите стационар"); }

            string dep = null;
            string dep_id = null;
            try { dep = comboBox2.Text; }
            catch { MessageBox.Show("Выберите стационар"); }

            //================основные методы================
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = @name_hir_department", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = hir;
                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    hir_id = reader["code_hir_department"].ToString();
                                }
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //получение code_hir_dep
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удаловь найти id выбранного стационара:\n" + ex.Message); }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"select * from type_department where name_department = @name_department", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@name_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = dep;
                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    dep_id = reader["id_department"].ToString();
                                }
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //получение dep_id
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удаловь найти id выбранного отделения:\n" + ex.Message); }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"update Staff set full_name = @full_name, phone = @phone, id_department = @id_department, code_hir_department = @code_hir_department, mail = @mail where id_staff = @id_staff", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idStaff;
                            command.Parameters.Add("@full_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = fio;
                            command.Parameters.Add("@phone", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pho;
                            command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = dep_id;
                            command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = hir_id;
                            command.Parameters.Add("@mail", NpgsqlTypes.NpgsqlDbType.Varchar).Value = mai;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //добавление сотрудника
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось изменить данные о сотруднике:\n" + ex.Message); }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand($"update passport set сode_pass = @сode_pass, data_get = @data_get, number_pass = @number_pass, who_give = @who_give, tally_pass = @tally_pass, adress_pass = @adress_pass where id_staff = @id_staff", connection))
                    {
                        try
                        {
                            command.Parameters.Add("@сode_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_number;
                            command.Parameters.Add("@data_get", NpgsqlTypes.NpgsqlDbType.Date).Value = pass_when_give_new;
                            command.Parameters.Add("@number_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_seria;
                            command.Parameters.Add("@who_give", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_who_give;
                            command.Parameters.Add("@tally_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_tally;
                            command.Parameters.Add("@adress_pass", NpgsqlTypes.NpgsqlDbType.Varchar).Value = pass_adrr;
                            command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idStaff;
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { MessageBox.Show("" + ex); }
                    }
                } //добавление создание для него паспорта
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось добавить паспортные данные для сотрудника:\n" + ex.Message); }

            try
            {
                if (job != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                    {
                        connection.Open();
                        using (NpgsqlCommand command = new NpgsqlCommand($"delete from {oldJob} where id_staff = @id_staff", connection))
                        {
                            try
                            {
                                command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idStaff;
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex) { MessageBox.Show("" + ex); }
                        }
                    } //удаление со старой должности
                }
                else { MessageBox.Show("Ошибка места работы"); }
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось получить должность сотрудника:\n" + ex.Message); }

            try
            {
                if (job != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                    {
                        connection.Open();
                        using (NpgsqlCommand command = new NpgsqlCommand($"insert into {job} (id_staff) values (@id_staff);", connection))
                        {
                            try
                            {
                                command.Parameters.Add("@id_staff", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idStaff;
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex) { MessageBox.Show("" + ex); }
                        }
                    } //добавление в новую таблицу должности
                }
                else { MessageBox.Show("Не выбрана должность"); }
            }
            catch (NpgsqlException ex) { MessageBox.Show("Не удалось изменить должность сотрудника:\n" + ex.Message); }

        }
    }
}
