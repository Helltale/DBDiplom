using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData;
using Npgsql;

namespace AndreevNIR
{
    public partial class FormReferenceData : Form
    {
        CoreLogic cl = new CoreLogic();
        StringGrouperSQL sgs = new StringGrouperSQL();
        DBLogicConnection dBLogicConnection = new DBLogicConnection();
        Placeholders pl = new Placeholders();

        string strPlc = "Значение для фильтра";
        public List<string> ShowDGVFullList = new List<string>();
        public int selectedMouseRowID;
        public string staffID;

        public FormReferenceData()
        {
            InitializeComponent();
            pl.PlaceholderShow(textBox1, strPlc);
            sgs.CreateReferenseQueryList();
            InitialLoadForFirstPage();

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormIndex2 formIndex2 = new FormIndex2();
            formIndex2.richTextBoxPrimeTime.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: //персонал
                    FormAddStaff fad = new FormAddStaff();
                    fad.ShowDialog();
                    break;
                case 1: //структура больницы
                    {
                        FormAddStruct1 fas = new FormAddStruct1();
                        fas.ShowDialog();
                        string str1 = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;";
                        ShowDGV(str1, dataGridView3, dBLogicConnection._connectionString);
                    }
                    break;
                case 2: //вид лечения
                    switch (comboBox3.SelectedIndex)
                    {
                        case 0:
                            {
                                FormAddTypeHealExamination fathe = new FormAddTypeHealExamination();
                                fathe.ShowDialog();
                            }
                            break;
                        case 1:
                            {
                                FormAddTypeHealConservative fathc = new FormAddTypeHealConservative();
                                fathc.ShowDialog();
                            }
                            break;
                        case 2:
                            {
                                FormAddTypeHealOperations fatho = new FormAddTypeHealOperations();
                                fatho.ShowDialog();
                            }
                            break;
                    }
                    break;
                case 3: //вид документов
                    switch (comboBox4.SelectedIndex)
                    {
                        case 0:
                            {
                                FormAddTypeDockExtract fatde = new FormAddTypeDockExtract();
                                fatde.ShowDialog();
                            }
                            break;
                        case 1:
                            {
                                FormAddTypeDockInitial fatdi = new FormAddTypeDockInitial();
                                fatdi.ShowDialog();
                            }
                            break;
                        case 2:
                            {
                                FormAddTypeDockNotWorking fatdw = new FormAddTypeDockNotWorking();
                                fatdw.ShowDialog();
                            }
                            break;
                    }
                    break;
                case 4: //вид процедур
                    FormAddTypeProc fatp = new FormAddTypeProc();
                    fatp.ShowDialog();
                    break;
                case 5: //роли
                    MessageBox.Show("Невозможно добавить новые роли!");
                    break;
            }
        } //добавление

        private void ShowDGV(string strQuery, DataGridView dgv, string connStr)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(strQuery, connStr);
            try
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgv.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }

        private void FillComboBox(ComboBox cb, List<string> li)
        {
            cb.DataSource = li;
            cb.Text = "Параметр";
        }

        public void InitialLoadForFirstPage() {
            string str0 = "SELECT staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;";
            ShowDGV(str0, dataGridView2, dBLogicConnection._connectionString);
            cl.CreateFullListOfShowDGV("SELECT staff.id_staff as \"ID\", staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;", ShowDGVFullList, '|');

            List<string> list = new List<string>();
            list.Add("ФИО"); list.Add("Должность"); list.Add("Название отделения"); list.Add("Название стационара"); list.Add("Телефон сотрудника"); list.Add("Почта сотрудника");
            list.Add("Уровень доступа"); list.Add("Начальник отделения"); list.Add("Начальник стационара");
            FillComboBox(comboBox1, list);
        }

        private void OpenTabControl()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: //персонал
                    string str0 = "SELECT staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;";
                    ShowDGV(str0, dataGridView2, dBLogicConnection._connectionString);
                    cl.CreateFullListOfShowDGV("SELECT staff.id_staff as \"ID\", staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;", ShowDGVFullList, '|');

                    List<string> list = new List<string>();
                    list.Add("ФИО"); list.Add("Должность"); list.Add("Название отделения"); list.Add("Название стационара"); list.Add("Телефон сотрудника"); list.Add("Почта сотрудника");
                    list.Add("Уровень доступа"); list.Add("Начальник отделения"); list.Add("Начальник стационара");
                    FillComboBox(comboBox1, list);

                    groupBox1.Show();
                    groupBox2.Show();
                    groupBox3.Show();
                    break;

                case 1: //структура больницы
                    string str1 = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;";
                    ShowDGV(str1, dataGridView3, dBLogicConnection._connectionString);
                    ShowDGVFullList = cl.CreateFullListOfShowDGV1("SELECT name_hir_department as \"Стационар\", hir_hospital.code_hir_department, adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\", name_department as \"Отделение\",department.id_department, (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\", room.sit_empt FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;", ShowDGVFullList, '|');

                    List<string> list1 = new List<string>();
                    list1.Add("Название стационара"); list1.Add("Адрес"); list1.Add("Телефон регистратуры"); list1.Add("ОГРМ"); list1.Add("Главный врач"); list1.Add("Отделение");
                    list1.Add("Заведущий отделением"); list1.Add("Палата");
                    FillComboBox(comboBox1, list1);

                    groupBox1.Show();
                    groupBox2.Show();
                    groupBox3.Show();
                    break;

                case 2:
                    comboBox3.SelectedIndex = 0; //вид лечения (логика в 3 комбобокс)

                    groupBox1.Show();
                    groupBox2.Show();
                    groupBox3.Show();
                    break;

                case 3:
                    comboBox4.SelectedIndex = 0; //вид документов (логика в 4 комбобокс)

                    groupBox1.Show();
                    groupBox2.Show();
                    groupBox3.Show();
                    break;

                case 4: //вид процедур
                    string str4 = "select procedures_.name_drocedure \"Название процедуры\", drug.name_drug \"Название препарата\", procedures_.value_drug \"Количество\", procedures_.value_name \"Тип\" from procedures_ join drug on procedures_.id_drug = drug.id_drug";
                    ShowDGV(str4, dataGridView6, dBLogicConnection._connectionString);

                    List<string> list4 = new List<string>();
                    list4.Add("Название процедуры"); list4.Add("Название препарата"); list4.Add("Количество препарата"); list4.Add("Тип значения");
                    FillComboBox(comboBox1, list4);

                    groupBox1.Show();
                    groupBox2.Show();
                    groupBox3.Show();
                    break;

                case 5: //роли
                    string str5 = "select staff.full_name ФИО, user_info.login_user Логин, user_info.role_user \"Уровень доступа\" from staff join user_info on staff.id_staff = user_info.id_staff";
                    ShowDGV(str5, dataGridView5, dBLogicConnection._connectionString);

                    List<string> list5 = new List<string>();
                    list5.Add("ФИО сотрудника"); list5.Add("Логин"); list5.Add("Уровень роли");
                    FillComboBox(comboBox1, list5);

                    groupBox1.Hide();
                    groupBox2.Hide();
                    groupBox3.Hide();
                    break;
            }
        } //отображение данных в dgv при смене вкладки

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenTabControl();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    string str21 = "select s.full_name as \"ФИО врача\", pa.full_name as \"ФИО пациента\", me.date_meeting as \"Дата проведения\", " +
                        "me.time_meeting as \"Время проведения\", me.discription_meeting as \"Описание осмотра\", me.operation_control as \"Послеоперационный осмотр\" " +
                        "from meetings me inner join patient_in_room pai on me.id_patient = pai.id_patient inner join patient pa on pa.omc = pai.omc inner " +
                        "join staff s on s.id_staff = me.id_staff";
                    ShowDGV(str21, dataGridView4, dBLogicConnection._connectionString);

                    List<string> list21 = new List<string>();
                    list21.Add("ФИО врача"); list21.Add("ФИО пациента"); list21.Add("Дата проведения"); list21.Add("Время проведения");
                    FillComboBox(comboBox1, list21);
                    break;
                case 1:
                    string str22 = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", " +
                        "n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" " +
                        "FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce " +
                        "JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc " +
                        "JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce;";
                    ShowDGV(str22, dataGridView4, dBLogicConnection._connectionString);

                    List<string> list22 = new List<string>();
                    list22.Add("ФИО пациента"); list22.Add("ФИО врача"); list22.Add("Дата проведения"); list22.Add("Время проведения"); list22.Add("Название процедуры");
                    list22.Add("ФИО мед. сестры");
                    FillComboBox(comboBox1, list22);

                    break;
                case 2:
                    string str23 = "select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", " +
                        "o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", " +
                        "o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff " +
                        "join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc";
                    ShowDGV(str23, dataGridView4, dBLogicConnection._connectionString);

                    List<string> list23 = new List<string>();
                    list23.Add("ФИО пациента"); list23.Add("ФИО врача"); list23.Add("Дата проведения"); list23.Add("Время проведения"); list23.Add("Название операции");
                    FillComboBox(comboBox1, list23);

                    break;
                default:
                    MessageBox.Show("Ошибка выбора");
                    break;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    string str31 = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as " +
                        "\"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" " +
                        "from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on " +
                        "pa.omc = pai.omc";
                    ShowDGV(str31, dataGridView1, dBLogicConnection._connectionString);

                    List<string> list31 = new List<string>();
                    list31.Add("Номер выписки"); list31.Add("ФИО пациента"); list31.Add("ФИО врача"); list31.Add("Дата выписки"); list31.Add("Диагноз при выписке"); list31.Add("Летальность");
                    FillComboBox(comboBox1, list31);
                    break;

                case 1:
                    string str32 = "select pa.full_name as \"ФИО пациента\", i.date_initial as \"Дата первичного осмотра\", s.full_name as \"ФИО врача приёмного покоя\", " +
                        "i.diagnosis as \"Диагноз\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist;";
                    ShowDGV(str32, dataGridView1, dBLogicConnection._connectionString);

                    List<string> list32 = new List<string>();
                    list32.Add("ФИО пациента"); list32.Add("Дата первичного осмотра"); list32.Add("ФИО врача приёмного покоя"); list32.Add("Диагноз");
                    FillComboBox(comboBox1, list32);
                    break;

                case 2:
                    string str33 = "select l.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", l.date_in as \"Дата поступления\" from list_not_working l " +
                        "join patient pa on pa.omc = l.omc";
                    ShowDGV(str33, dataGridView1, dBLogicConnection._connectionString);

                    List<string> list33 = new List<string>();
                    list33.Add("ФИО пациента"); list33.Add("Номер выписки"); list33.Add("Дата поступления");
                    FillComboBox(comboBox1, list33);
                    break;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e) { pl.PlaceholderHide(textBox1, strPlc); }

        private void textBox1_Leave(object sender, EventArgs e) { pl.PlaceholderShow(textBox1, strPlc); }

        private void FilterGridCore(string queryCommandNEW, DataGridView dgv, string find)
        {
            using (var con = dBLogicConnection.GetConnection())
            {
                string queryCommand = "";
                queryCommand = queryCommandNEW;

                con.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(queryCommand, con);
                    cmd.Parameters.AddWithValue("find", find);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgv.DataSource = table.DefaultView;
                }
                catch (Exception ex) { MessageBox.Show("Непредвиденная ошибка: " + ex); }
            }
        } //поиск по стринге

        private void FilterGridTime(string queryCommandNEW, DataGridView dgv, string time)
        {
            DateTime formattedDate = DateTime.ParseExact(time, "H:m:s", CultureInfo.InvariantCulture);
            TimeSpan completeTime = formattedDate.TimeOfDay;

            using (var con = dBLogicConnection.GetConnection())
            {
                string queryCommand = "";
                queryCommand = queryCommandNEW;

                con.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(queryCommand, con);
                    cmd.Parameters.Add("find", NpgsqlTypes.NpgsqlDbType.Time).Value = completeTime;
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgv.DataSource = table.DefaultView;
                }
                catch (Exception ex) { MessageBox.Show("Непредвиденная ошибка: " + ex); }
            }
        } //поиск по времени

        private void FilterGridDate(string queryCommandNEW, DataGridView dgv, string date)
        {

            string formattedDate = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-dd-MM");
            DateTime completeDate = Convert.ToDateTime(formattedDate);

            using (var con = dBLogicConnection.GetConnection())
            {
                string queryCommand = "";
                queryCommand = queryCommandNEW;

                con.Open();
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(queryCommand, con);
                    cmd.Parameters.Add("find", NpgsqlTypes.NpgsqlDbType.Date).Value = completeDate;
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgv.DataSource = table.DefaultView;
                }
                catch (Exception ex) { MessageBox.Show("Непредвиденная ошибка: " + ex); }
            }
        } //поиск по дате

        private string FilterGrid2(string queryCommandNEW, DataGridView dgv, string textTxtBox)
        { //NOSONAR
            using (var con = dBLogicConnection.GetConnection())
            {
                string tmpTable = "";
                con.Open();
                try
                {
                    switch (textTxtBox)
                    {
                        case "Врач":
                            tmpTable = "therapist.id_staff";
                            break;

                        case "Постовая мед сестра":
                            tmpTable = "guard_nurse.id_staff";
                            break;

                        case "Врач приёмного покоя":
                            tmpTable = "receptionist.id_staff";
                            break;
                    }
                    string queryCommand = "";
                    queryCommand = queryCommandNEW;

                    NpgsqlCommand cmd = new NpgsqlCommand(queryCommand, con);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgv.DataSource = table.DefaultView;
                }
                catch (Exception ex) { MessageBox.Show("Непредвиденная ошибка: " + ex); }
                return tmpTable;
            }
        } //сложный поиск (пока не работает)

        private void button1_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: //персонал
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0: //фио
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(0);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 1: //должность
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(1);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 2: //название отделения
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(2);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 3: //название стационара
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(3);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 4: //телефон сотрудника
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(4);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 5: //почта сотрудника
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(5);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 6: //уровень доступа
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(6);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 7: //начальник отделения
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(7);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                        case 8: //начальник стационара
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(8);
                                FilterGridCore(queryCommand, dataGridView2, textBox1.Text);
                            }
                            break;
                    }
                    break;
                case 1: //структура больницы
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0: //название стационара
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(9);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                        case 1: //адрес стационара
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(10);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                        case 2: //телефон регистратуры
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(11);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                        case 3: //ОГРМ
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(12);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                        case 4: //главный врач
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(13);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                        case 5: //отделение
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(14);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                        case 6: //заведущий отделением
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(15);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                        case 7: //комната
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(16);
                                FilterGridCore(queryCommand, dataGridView3, textBox1.Text);
                            }
                            break;
                    }
                    break;
                case 2: //вид лечения
                    switch (comboBox3.SelectedIndex)
                    {
                        case 0: //плановые осмотры
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0: //фио врача
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(17);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 1: //фио пациента
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(18);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 2: //дата проведения
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(19);
                                        FilterGridDate(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 3: //время проведения
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(20);
                                        FilterGridTime(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                            }
                            break;
                        case 1: //консервативное лечение
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0: //фио пациента
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(21);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 1: //фио врача
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(22);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 2: //дата проведения
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(23);
                                        FilterGridDate(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 3: //время проведения
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(24);
                                        FilterGridTime(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 4: //название процедуры
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(25);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 5: //фио мед сестры
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(26);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                            }
                            break;
                        case 2: //операции
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0:
                                    { //фио пациента
                                        string queryCommand = sgs.OpenReferenseQueryList(27);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 1: //фио врача
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(28);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 2: //дата проведения
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(29);
                                        FilterGridDate(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 3: //время проведения
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(30);
                                        FilterGridTime(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                                case 4: //название операции
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(31);
                                        FilterGridCore(queryCommand, dataGridView4, textBox1.Text);
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case 3: //вид документов
                    switch (comboBox4.SelectedIndex)
                    {
                        case 0: //выписка
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0: //номер выписки
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(32);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 1: //фио пациента
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(33);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 2: //фио врача
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(34);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 3: //дата выписки
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(35);
                                        FilterGridDate(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 4: //диагноз при выписке
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(36);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 5: //летальность
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(37);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                            }
                            break;
                        case 1: //первичный осмотр
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0: //фио пациента
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(38);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 1: //дата первичного осмотра
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(39);
                                        FilterGridDate(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 2: //фио врача приёмного покоя
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(40);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 3: //диагноз
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(41);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                            }
                            break;
                        case 2: //нетрудоспособность
                            switch (comboBox1.SelectedIndex)
                            {
                                case 0: //фио пациента
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(42);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 1: //номер выписки
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(43);
                                        FilterGridCore(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                                case 2: //дата поступления
                                    {
                                        string queryCommand = sgs.OpenReferenseQueryList(44);
                                        FilterGridDate(queryCommand, dataGridView1, textBox1.Text);
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case 4: //вид процедур
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0: //название процедуры
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(45);
                                FilterGridCore(queryCommand, dataGridView6, textBox1.Text);
                            }
                            break;
                        case 1: //название препарата
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(46);
                                FilterGridCore(queryCommand, dataGridView6, textBox1.Text);
                            }
                            break;
                        case 2: //количество препарата
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(47);
                                FilterGridCore(queryCommand, dataGridView6, textBox1.Text);
                            }
                            break;
                        case 3: //тип значения
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(48);
                                FilterGridCore(queryCommand, dataGridView6, textBox1.Text);
                            }
                            break;
                    }
                    break;
                case 5: //роли
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0: //фио сотрудника
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(49);
                                FilterGridCore(queryCommand, dataGridView6, textBox1.Text);
                            }
                            break;
                        case 1: //логин
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(50);
                                FilterGridCore(queryCommand, dataGridView6, textBox1.Text);
                            }
                            break;
                        case 2: //уровень доступа
                            {
                                string queryCommand = sgs.OpenReferenseQueryList(51);
                                FilterGridCore(queryCommand, dataGridView6, textBox1.Text);
                            }
                            break;
                    }
                    break;
            } //свич в свиче просто безобразие, потом переделать
        } //фильтры

        private void button2_Click(object sender, EventArgs e) //сброс фильтров
        {
            textBox1.Text = null;
            switch (tabControl1.SelectedIndex)
            {
                case 2: //вид лечения
                    switch (comboBox3.SelectedIndex)
                    {
                        case 0://
                            string str21 = "select s.full_name as \"ФИО врача\", pa.full_name as \"ФИО пациента\", me.date_meeting as \"Дата проведения\", " +
                            "me.time_meeting as \"Время проведения\", me.discription_meeting as \"Описание осмотра\", me.operation_control as \"Послеоперационный осмотр\" " +
                            "from meetings me inner join patient_in_room pai on me.id_patient = pai.id_patient inner join patient pa on pa.omc = pai.omc inner " +
                            "join staff s on s.id_staff = me.id_staff";
                            ShowDGV(str21, dataGridView4, dBLogicConnection._connectionString);
                            break;
                        case 1:
                            string str22 = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", " +
                            "n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" " +
                            "FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce " +
                            "JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc " +
                            "JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce;";
                            ShowDGV(str22, dataGridView4, dBLogicConnection._connectionString);
                            break;
                        case 2:
                            string str23 = "select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", " +
                            "o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", " +
                            "o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff " +
                            "join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc";
                            ShowDGV(str23, dataGridView4, dBLogicConnection._connectionString);
                            break;
                    }
                    break;
                case 3: //вид документов
                    switch (comboBox4.SelectedIndex)
                    {
                        case 0:
                            string str31 = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as " +
                            "\"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" " +
                            "from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on " +
                            "pa.omc = pai.omc";
                            ShowDGV(str31, dataGridView1, dBLogicConnection._connectionString);
                            break;
                        case 1:
                            string str32 = "select pa.full_name as \"ФИО пациента\", i.date_initial as \"Дата первичного осмотра\", s.full_name as \"ФИО врача приёмного покоя\", " +
                            "i.diagnosis as \"Диагноз\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist;";
                            ShowDGV(str32, dataGridView1, dBLogicConnection._connectionString);
                            break;
                        case 2:
                            string str33 = "select l.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", l.date_in as \"Дата поступления\" from list_not_working l " +
                            "join patient pa on pa.omc = l.omc";
                            ShowDGV(str33, dataGridView1, dBLogicConnection._connectionString);
                            break;
                    }
                    break;
                default: //всё остальное
                    OpenTabControl();
                    break;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) {
                case 0: //персонал
                    {
                        //получение id сотрудника
                        staffID = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 0); 

                        DBLogicConnection dB = new DBLogicConnection();
                        using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM staff WHERE id_staff = '{staffID}';", connection))
                            {
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception ex) { MessageBox.Show("" + ex); }
                                MessageBox.Show("Сотрудник удалён");
                            }
                        }
                        string str = "SELECT staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;";
                        ShowDGV(str, dataGridView2, dBLogicConnection._connectionString);
                    }
                    break;
                case 1: //стуктура больницы
                    {
                        string numberRoom = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 9);
                        string codeHirDep = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 1);
                        string idDepartment = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 7);

                        DBLogicConnection dB = new DBLogicConnection();
                        using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
                        {
                            connection.Open();
                            using (NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM room WHERE number_room = '{numberRoom}' and code_hir_department = '{codeHirDep}' and id_department = '{idDepartment}';", connection))
                            {
                                try
                                {
                                    command.ExecuteNonQuery();
                                }
                                catch (Exception ex) { MessageBox.Show("" + ex); }
                                MessageBox.Show("Запись удалёна");
                            }
                        }
                        string str1 = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;";
                        ShowDGV(str1, dataGridView3, dBLogicConnection._connectionString);
                    }
                    break;
                case 2: //вид лечения
                    break;
                case 3: //вид документов
                    break;
                case 4: // вид процедур
                    break;
                case 5: // роли
                    break;

            }
            //MessageBox.Show("Tab: " + tabControl1.SelectedIndex);
        } //удаление

        private void button5_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0: //персонал
                    staffID = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 0);
                    FormUpdateStaff fud = new FormUpdateStaff(staffID);
                    fud.ShowDialog();
                    break;
                case 1: //структура
                    string number_room = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 9);
                    string code_hir_department = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 1);
                    string id_department = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 7);

                    FormAddStruct1 fas = new FormAddStruct1(id_department, code_hir_department, number_room);
                    fas.ShowDialog();

                    string str1 = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;";
                    ShowDGV(str1, dataGridView3, dBLogicConnection._connectionString);
                    break;
            }
        } //изменение

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { selectedMouseRowID = dataGridView2.SelectedRows[0].Index; } catch { }
            
        } //для получения строки по клику

        private void FormReferenceData_Load(object sender, EventArgs e)
        {
            try {

                switch (tabControl1.SelectedIndex) {
                    case 0:
                        selectedMouseRowID = dataGridView2.SelectedRows[0].Index;
                        staffID = cl.GetElementFromListOfShowDGV(ShowDGVFullList, '|', selectedMouseRowID, 0);
                        break;
                }
            } 
            catch { }
        } //при использовании экземпляра класса FormReferenceData

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                ShowDGVFullList = cl.CreateFullListOfShowDGV1("SELECT name_hir_department as \"Стационар\", hir_hospital.code_hir_department, adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\", name_department as \"Отделение\",department.id_department, (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\", room.sit_empt FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;", ShowDGVFullList, '|');
                selectedMouseRowID = dataGridView3.SelectedRows[0].Index; 
            } catch { }
        }
    }
}
