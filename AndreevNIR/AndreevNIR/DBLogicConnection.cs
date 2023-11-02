using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Data;

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
        "6" - админ?????
         */

        public string _connectionString = "Host=localhost;Username=postgres;Password=64210369;Database=postgres";
        public string conHost = "localhost";
        public string conUsername = "postgres";
        public string conPassword = "64210369";
        public string conDB = "postgres";

        public bool tmpFlag = false;

        NpgsqlConnection connection = new NpgsqlConnection();

        public void OpenSQLConnection(string connStr) {
            try
            {
                connection = new NpgsqlConnection(connStr);
                connection.Open();
                //MessageBox.Show("Подключение к PostgreSQL успешно установлено.");
            }
            catch(Exception ex) { MessageBox.Show($"Ошибка при подключении к PostgreSQL: \n{ex.Message}"); }
        }

        public void CreateQueryLogIn(string login_, string password_) {
            string queryLogIn = "select u.id_staff, login_user, password_user, role_user, full_name " +
                "from User_info u " +
                "join staff s on s.id_staff = u.id_staff " +
                "where login_user = @l and password_user = @p";
            NpgsqlCommand command = new NpgsqlCommand(queryLogIn, connection);

            command.Parameters.Add("@l", NpgsqlTypes.NpgsqlDbType.Varchar).Value = login_;
            command.Parameters.Add("@p", NpgsqlTypes.NpgsqlDbType.Varchar).Value = password_;

            NpgsqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string idStaff = reader["id_staff"].ToString();
                string roleStaff = reader["role_user"].ToString();
                string nameStaff = reader["full_name"].ToString();
                MessageBox.Show("Добро пожаловать "+ nameStaff + "!\nID сотрудника: " + idStaff);
                FormAutorization form = new FormAutorization();
                tmpFlag = true;

                SessionInformation si = new SessionInformation();
                si.userID = idStaff;
                si.userRole = roleStaff;
                si.userName = nameStaff;
            }
            else {
                MessageBox.Show("Некорректные данные входа!");
            }

        }

        private NpgsqlConnection GetConnection() {
            return new NpgsqlConnection(_connectionString);
        }

        public void Test(string name, DataGridView dgv) {
            using (var con = GetConnection()) {
                string queryCommand = "select 'Первичный осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", i.date_initial as " +
            "\"Дата проведения\", i.time_initial as \"Время проведения\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = " +
            "i.doc_receptinoist where pa.full_name = @name union all select 'Плановый осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as " +
            "\"ФИО врача\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\" from meetings me join staff s on s.id_staff = me.id_staff " +
            "join patient_in_room pai on me.id_patient = pai.id_patient join patient pa on pa.omc = pai.omc where pa.full_name = @name union all select 'Консервативное лечение' " +
            "as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", co.date_procedure as \"Дата проведения\", co.time_procedure as " +
            "\"Время проведения\" from Сonservative co join patient_in_room pai on pai.id_patient = co.id_patient join patient pa on pa.omc = pai.omc join staff s on " +
            "s.id_staff = co.id_staff where pa.full_name = @name union all select 'Оперативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as " +
            "\"ФИО врача\", op.date_operation as \"Дата проведения\", op.time_operation as \"Время проведения\" from operation op join patient_in_room pai on pai.id_patient = " +
            "op.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = op.id_staff where pa.full_name = @name union all select 'Эпикриз' as \"Мероприятие\", " +
            "pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата проведения\", e.time_extract as \"Время проведения\" from extract_document " +
            "e join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = e.id_staff where pa.full_name = " +
            "@name union all select 'Лист о нетрудоспособности' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as " +
            "\"Дата проведения\", e.time_extract as \"Время проведения\" from list_not_working l join patient pa on l.omc = pa.omc join extract_document e on " +
            "e.numb_extract = l.numb_extract join staff s on s.id_staff = e.id_staff where pa.full_name = @name";

                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(queryCommand, con);
                cmd.Parameters.AddWithValue("name", name);
                    
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgv.DataSource = table.DefaultView;
            }
        }

        public void OperationalHistory(string name, DataGridView dgv)
        {
            







                string queryCommand = "select 'Первичный осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", i.date_initial as " +
                    "\"Дата проведения\", i.time_initial as \"Время проведения\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = " +
                    "i.doc_receptinoist where pa.full_name = '@name' union all select 'Плановый осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as " +
                    "\"ФИО врача\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\" from meetings me join staff s on s.id_staff = me.id_staff " +
                    "join patient_in_room pai on me.id_patient = pai.id_patient join patient pa on pa.omc = pai.omc where pa.full_name = '@name' union all select 'Консервативное лечение' " +
                    "as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", co.date_procedure as \"Дата проведения\", co.time_procedure as " +
                    "\"Время проведения\" from Сonservative co join patient_in_room pai on pai.id_patient = co.id_patient join patient pa on pa.omc = pai.omc join staff s on " +
                    "s.id_staff = co.id_staff where pa.full_name = '@name' union all select 'Оперативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as " +
                    "\"ФИО врача\", op.date_operation as \"Дата проведения\", op.time_operation as \"Время проведения\" from operation op join patient_in_room pai on pai.id_patient = " +
                    "op.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = op.id_staff where pa.full_name = '@name' union all select 'Эпикриз' as \"Мероприятие\", " +
                    "pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата проведения\", e.time_extract as \"Время проведения\" from extract_document " +
                    "e join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = e.id_staff where pa.full_name = " +
                    "'@name' union all select 'Лист о нетрудоспособности' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as " +
                    "\"Дата проведения\", e.time_extract as \"Время проведения\" from list_not_working l join patient pa on l.omc = pa.omc join extract_document e on " +
                    "e.numb_extract = l.numb_extract join staff s on s.id_staff = e.id_staff where pa.full_name = '@name'";
            NpgsqlCommand command = new NpgsqlCommand(queryCommand, connection);

            command.Parameters.Add("@n", NpgsqlTypes.NpgsqlDbType.Varchar).Value = name;
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(command.ToString(), connection);

                try
                {
                    DataTable table = new DataTable();
                    adapter1.Fill(table);

                    dgv.DataSource = table;
                }
                catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
           
        }

        public string SetNewConnectionString(string host, string user, string password, string db) { //ручная настройка //всё ещё не работает
            string connectionString = "Host="+ host + ";Username="+ user + ";Password="+ password + ";Database="+ db;
            return connectionString;
        }
        public void SetNewConnectionStringDefault() //дефолт
        {
           
        }
    }
}
