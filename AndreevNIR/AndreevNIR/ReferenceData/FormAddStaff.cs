using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR;
using Npgsql;

namespace AndreevNIR
{
    public partial class FormAddStaff : Form
    {
        CoreLogic cl = new CoreLogic();
        public FormAddStaff()
        {
            InitializeComponent();
            comboBox2.Text = "Выберите отделение";
            comboBox2.Enabled = false;
            cl.LoadComboboxByQuery(comboBox1, "select name_hir_department from hir_hospital", "Выберите стационар");
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.Enabled == false) {
                comboBox2.Enabled = true;
            }
            string tmpHirHospital = comboBox1.SelectedItem.ToString();
            cl.LoadComboboxByQuery(comboBox2, $"select td.name_department from department d join type_department td on d.id_department = td.id_department join hir_hospital h on d.code_hir_department = h.code_hir_department where name_hir_department = '{tmpHirHospital}'", "Выберите отделение");
        }

        private void button1_Click(object sender, EventArgs e) //заполнение таблиц: персонала, паспортных данных, должности
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
            } catch (NpgsqlException ex) { MessageBox.Show("Не удаловь найти id выбранного стационара:\n" + ex.Message); }

            try {
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
            } catch (NpgsqlException ex) { MessageBox.Show("Не удаловь найти id выбранного отделения:\n" + ex.Message); }

            try {
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
            } catch (NpgsqlException ex) { MessageBox.Show("Не удалось добавить сотрудника:\n" + ex.Message); }

            try {
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
            } catch (NpgsqlException ex) { MessageBox.Show("Не удалось добавить паспортные данные для сотрудника:\n" + ex.Message); }

            try {
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
            } catch (NpgsqlException ex) { MessageBox.Show("Не удалось назначить сотрудника на должность:\n" + ex.Message); }


            //добавление в ролях доступа к приложению
            
            

            //отобразим что заполнили
            cl.ShowDGV("SELECT staff.full_name AS \"ФИО\", CASE     WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра'     WHEN therapist.id_staff IS NOT NULL THEN 'Врач'     WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя'     WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения'     WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач'     WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\" FROM staff LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON department.id_department = staff.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;", frd.dataGridView2, db._connectionString);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
