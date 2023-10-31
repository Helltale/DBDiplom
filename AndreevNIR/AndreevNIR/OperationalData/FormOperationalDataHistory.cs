using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR
{
    public partial class FormOperationalDataHistory : Form
    {
        public FormOperationalDataHistory()
        {
            InitializeComponent();
            DGVList();
            DGV();
        }

        public string currentName;

        private void FormOperationalDataHistory_Load(object sender, EventArgs e)
        {

        }

        public void DGV() {

            DBLogicConnection dBLogicConnection = new DBLogicConnection();

            string str1 = " select 'Первичный осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", " +
                "i.date_initial as \"Дата проведения\", i.time_initial as \"Время проведения\" from initial_inspection i join patient pa on pa.omc = " +
                "i.omc join staff s on s.id_staff = i.doc_receptinoist union all select 'Плановый осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", " +
                "s.full_name as \"ФИО врача\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\" from meetings me join staff s on " +
                "s.id_staff = me.id_staff join patient_in_room pai on me.id_patient = pai.id_patient join patient pa on pa.omc = pai.omc union all select " +
                "'Консервативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", co.date_procedure as \"Дата проведения\", " +
                "co.time_procedure as \"Время проведения\" from Сonservative co join patient_in_room pai on pai.id_patient = co.id_patient join patient pa on pa.omc =" +
                " pai.omc join staff s on s.id_staff = co.id_staff union all select 'Оперативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name " +
                "as \"ФИО врача\", op.date_operation as \"Дата проведения\", op.time_operation as \"Время проведения\" from operation op join patient_in_room pai on " +
                "pai.id_patient = op.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = op.id_staff union all select 'Эпикриз' as \"Мероприятие\", " +
                "pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата проведения\", e.time_extract as \"Время проведения\" from " +
                "extract_document e join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = e.id_staff " +
                "union all select 'Лист о нетрудоспособности' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата " +
                "проведения\", e.time_extract as \"Время проведения\" from list_not_working l join patient pa on l.omc = pa.omc join extract_document e on e.numb_extract = " +
                "l.numb_extract join staff s on s.id_staff = e.id_staff";

            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }

        public void DGVList()
        {
            DBLogicConnection dBLogicConnection = new DBLogicConnection();

            string str1 = "select full_name as \"ФИО пациента\" from patient";
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);

                dataGridView2.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            FormOperationalDataHistoryAdd add = new FormOperationalDataHistoryAdd();
            add.ShowDialog();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            var currentName_ = dataGridView2.CurrentCell.Value;
            currentName = currentName_.ToString();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {

        }
    }
}
