﻿using System;
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

        private void ShowDGV(string strQuery, DataGridView dgv, string connStr) {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(strQuery, connStr);
            try
            {
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgv.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }

        private void FillComboBox(ComboBox cb, List<string> li) {
            cb.DataSource = li;
            cb.Text = "Параметр";        
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (tabControl1.SelectedIndex) {
                case 0: //информация о персонале больницы
                    string str0 = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                    "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                    "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                    "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                    "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                    "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                    "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                    "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                    "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department";
                    ShowDGV(str0, dataGridView2, dBLogicConnection._connectionString);

                    List<string> list = new List<string>();
                    list.Add("ФИО"); list.Add("Должность"); list.Add("Название отделения"); list.Add("Название стационара"); list.Add("Телефон сотрудника"); list.Add("Почта сотрудника");
                    list.Add("Уровень доступа"); list.Add("Начальник отделения"); list.Add("Начальник стационара");
                    FillComboBox(comboBox1, list);



                    break;

                case 1: //структура больницы
                    string str1 = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", " +
                        "(select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", " +
                        "(select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" " +
                        "FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department " +
                        "JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff;";
                    ShowDGV(str1, dataGridView3, dBLogicConnection._connectionString);

                    List<string> list1 = new List<string>();
                    list1.Add("Название стационара"); list1.Add("Адрес"); list1.Add("Телефон регистратуры"); list1.Add("ОГРМ"); list1.Add("Главный врач"); list1.Add("Отделение");
                    list1.Add("Заведущий отделением"); list1.Add("Палата");
                    FillComboBox(comboBox1, list1);
                    break;

                case 2:
                    comboBox3.SelectedIndex = 0; //логика в 3 комбобокс
                    break;

                case 3:
                    comboBox4.SelectedIndex = 0; //логика в 4 комбобокс
                    break;

                case 4: 
                    string str4 = "select procedures_.name_drocedure \"Название процедуры\", drug.name_drug \"Название препарата\", procedures_.value_drug \"Количество\", procedures_.value_name \"Тип\" from procedures_ join drug on procedures_.id_drug = drug.id_drug";
                    ShowDGV(str4, dataGridView6, dBLogicConnection._connectionString);

                    List<string> list4 = new List<string>();
                    list4.Add("Название процедуры"); list4.Add("Название препарата"); list4.Add("Количество препарата"); list4.Add("Тип значения");
                    FillComboBox(comboBox1, list4);
                    break;

                case 5:
                    string str5 = "select staff.full_name ФИО, user_info.login_user Логин, user_info.role_user \"Уровень доступа\" from staff join user_info on staff.id_staff = user_info.id_staff";
                    ShowDGV(str5, dataGridView5, dBLogicConnection._connectionString);

                    List<string> list5 = new List<string>();
                    list5.Add("ФИО сотрудника"); list5.Add("Логин"); list5.Add("Уровень роли");
                    FillComboBox(comboBox1, list5);
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
                    string str23 = " select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", " +
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
            switch (comboBox4.SelectedIndex) {
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

    }
}
