using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace AndreevNIR
{
    public partial class FormReferenceData : Form
    {
        DBLogicConnection dBLogicConnection = new DBLogicConnection();


        public FormReferenceData()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormIndex2 formIndex2 = new FormIndex2();
            formIndex2.richTextBoxPrimeTime.Show();
            this.Hide();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReferenceDataAdd add = new FormReferenceDataAdd();
            add.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (tabControl1.SelectedIndex) {
                case 0: //информация о персонале больницы
                    string str0 = @"SELECT staff.full_name AS ""ФИО"", " +
                    @"CASE " +
                    @"WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' " +
                    @"WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                    @"WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' " +
                    @"END AS ""Должность"", " +
                    @"type_department.name_department AS ""Название отделения"", " +
                    @"hir_hospital.name_hir_department AS ""Название стационара"", " +
                    @"staff.phone AS ""Телефон"", " +
                    @"staff.mail AS ""Почта"", " +
                    @"user_info.role_user AS ""Уровень доступа"", " +
                    @"(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS ""Начальник отделения"", " +
                    @"(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS ""Начальник стационара"" " +
                    @"FROM staff " +
                    @"FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff " +
                    @"FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                    @"FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff " +
                    @"FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                    @"FULL OUTER JOIN department ON department.id_department = staff.id_department " +
                    @"FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                    @"FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department";

                    NpgsqlDataAdapter adapter0 = new NpgsqlDataAdapter(str0, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter0.Fill(table);

                        dataGridView2.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;

                case 1: //структура больницы, id поменять на фио докторов, через подзапрос
                    string str1 = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", " +
                        "(select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", " +
                        "(select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" " +
                        "FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department " +
                        "JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;";
                    NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
                    try{
                        DataTable table = new DataTable();
                        adapter1.Fill(table);

                        dataGridView3.DataSource = table;
                    }
                    catch(Exception ex) { MessageBox.Show("Ошибка: "+ ex); }
                    break;

                case 2:
                    comboBox3.SelectedIndex = 0; //логика в 3 комбобокс
                    break;

                case 3:
                    comboBox4.SelectedIndex = 0; //логика в 4 комбобокс
                    break;

                case 4: 
                    string str4 = "select procedures_.name_drocedure \"Название процедуры\", drug.name_drug \"Название препарата\", procedures_.value_drug \"Количество\", procedures_.value_name \"Тип\" from procedures_ join drug on procedures_.id_drug = drug.id_drug";
                    NpgsqlDataAdapter adapter4 = new NpgsqlDataAdapter(str4, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter4.Fill(table);

                        dataGridView6.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;

                case 5:
                    string str5 = "select staff.full_name ФИО, user_info.login_user Логин, user_info.role_user \"Уровень доступа\" from staff join user_info on staff.id_staff = user_info.id_staff";
                    NpgsqlDataAdapter adapter5 = new NpgsqlDataAdapter(str5, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter5.Fill(table);

                        dataGridView5.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex) {
                case 0:
                    string str21 = "select s.full_name as \"ФИО врача\", pa.full_name as \"ФИО пациента\", me.date_meeting as \"Дата проведения\", " +
                        "me.time_meeting as \"Время проведения\", me.discription_meeting as \"Описание осмотра\", me.operation_control as \"Послеоперационный осмотр\" " +
                        "from meetings me inner join patient_in_room pai on me.id_patient = pai.id_patient inner join patient pa on pa.omc = pai.omc inner " +
                        "join staff s on s.id_staff = me.id_staff";
                    NpgsqlDataAdapter adapter21 = new NpgsqlDataAdapter(str21, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter21.Fill(table);

                        dataGridView4.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;
                case 1:
                    string str22 = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", " +
                        "n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" " +
                        "FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce " +
                        "JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc " +
                        "JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce;";
                    NpgsqlDataAdapter adapter22 = new NpgsqlDataAdapter(str22, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter22.Fill(table);

                        dataGridView4.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;
                case 2:
                    string str23 = " select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", " +
                        "o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", " +
                        "o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff " +
                        "join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc";
                    NpgsqlDataAdapter adapter23 = new NpgsqlDataAdapter(str23, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter23.Fill(table);

                        dataGridView4.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;
                default:
                    MessageBox.Show("Ошибка выбора");
                    break;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedIndex) {
                case 0:
                    string str31 = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as " +
                        "\"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" " +
                        "from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on " +
                        "pa.omc = pai.omc";
                    NpgsqlDataAdapter adapter31 = new NpgsqlDataAdapter(str31, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter31.Fill(table);

                        dataGridView1.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;

                case 1:
                    string str32 = "select pa.full_name as \"ФИО пациента\", i.date_initial as \"Дата первичного осмотра\", s.full_name as \"ФИО врача приёмного покоя\", " +
                        "i.diagnosis as \"Диагноз\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist;";
                    NpgsqlDataAdapter adapter32 = new NpgsqlDataAdapter(str32, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter32.Fill(table);

                        dataGridView1.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;

                case 2:
                    string str33 = "select l.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", l.date_in as \"Дата поступления\" from list_not_working l " +
                        "join patient pa on pa.omc = l.omc";
                    NpgsqlDataAdapter adapter33 = new NpgsqlDataAdapter(str33, dBLogicConnection._connectionString);
                    try
                    {
                        DataTable table = new DataTable();
                        adapter33.Fill(table);

                        dataGridView1.DataSource = table;
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
                    break;
            }
        }

    }
}
