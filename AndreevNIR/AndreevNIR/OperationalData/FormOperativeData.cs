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
using AndreevNIR.OperationalData.Patients;
using AndreevNIR.ReferenceData.Documents.Extraction;
using AndreevNIR.ReferenceData;
using AndreevNIR.OperationalData.History;

namespace AndreevNIR.OperationalData
{
    public partial class FormOperativeData : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        private string queryPatient = "select t1.id_patient, t1.omc, t2.full_name \"ФИО пацента\", t6.full_name \"ФИО врача\", t1.date_room \"Дата попадания\", t7.date_extract \"Дата выписки\", t3.name_hir_department \"Стационар\", t4.name_department \"Отделение\", t1.number_room \"Палата\" from patient_in_room t1 join patient t2 on t1.omc = t2.omc join hir_hospital t3 on t3.code_hir_department = t1.code_hir_department join type_department t4 on t4.id_department = t1.id_department join doc_patient t5 on t5.id_patient = t1.id_patient join staff t6 on t5.id_staff = t6.id_staff full outer join extract_document t7 on t7.id_patient = t5.id_patient and t7.id_staff = t5.id_staff";
        private string queryListOut = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc";
        private string queryHistoryNames = "select omc, full_name as \"ФИО пациента\" from patient";
        
        private string selectedMouseCell0;
        private string selectedMouseCell1;
        private string selectedMouseCell2;
        private string selectedMouseCell3;
        private string selectedMouseCell4;

        private string id_patient = null;

        public FormOperativeData()
        {
            InitializeComponent();

            cl.ShowDGV(queryPatient, dataGridView1, db._connectionString);
            dataGridView1.Columns[0].Visible = false; //id_patient
            dataGridView1.Columns[1].Visible = false; //omc

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void OperationalDataPatient()
        {
            //DBLogicConnection dBLogicConnection = new DBLogicConnection();


            //NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            //try
            //{
            //    DataTable table = new DataTable();
            //    adapter1.Fill(table);
            //    dataGridView1.DataSource = table;

            //    //List<string> list = new List<string>();
            //    //list.Add("ФИО пациента"); list.Add("Номер палаты"); list.Add("Отделение"); list.Add("Хирургический стационар");
            //    //FillComboBox(comboBox2, list);
            //}
            //catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
            //dataGridView1.Columns[0].Visible = false; //id_patient
            //dataGridView1.Columns[1].Visible = false; //omc
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) {
                case 0: //учет пациентов
                    { 
                        cl.ShowDGV(queryPatient, dataGridView1, db._connectionString);
                    }
                    break;
                case 1:
                    {
                        cl.ShowDGV(queryListOut, dataGridView2, db._connectionString);
                    }
                    break;
                case 2: //история болезни
                    {
                        cl.ShowDGV(queryHistoryNames, dataGridView3, db._connectionString);
                        dataGridView3.Columns[0].Visible = false; //omc
                    }
                    break;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) {
                case 0: //учет пациентов
                    {
                        FormOperationalDataPatientAdd fo = new FormOperationalDataPatientAdd();
                        fo.ShowDialog();
                        cl.ShowDGV(queryPatient, dataGridView1, db._connectionString);
                    }
                    break;
                case 1: //выписные листы
                    {
                        FormAddTypeDockExtract fa = new FormAddTypeDockExtract();
                        fa.ShowDialog();
                        cl.ShowDGV(queryListOut, dataGridView2, db._connectionString);
                    }
                    break;
                case 2:  //история
                    {
                        if (dataGridView3.SelectedRows.Count != 1)
                        {
                            MessageBox.Show("Выберите пациента");
                        }
                        else {
                            HistoryClass h = new HistoryClass();
                            string[] data = h.getOmcAndID(selectedMouseCell0);
                            FormChildAdd fc = new FormChildAdd(data);
                            fc.ShowDialog();
                        }
                    }
                    break;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                selectedMouseCell0 = dataGridView3.SelectedRows[0].Cells[0].Value.ToString(); //omc
                selectedMouseCell1 = dataGridView3.SelectedRows[0].Cells[1].Value.ToString(); //name
            } catch { }
            string queryHistoryData = $"select 'Первичный осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", i.date_initial as \"Дата проведения\", i.time_initial as \"Время проведения\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist where pa.full_name = '{selectedMouseCell1}' union all select 'Плановый осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\" from meetings me join staff s on s.id_staff = me.id_staff join patient_in_room pai on me.id_patient = pai.id_patient join patient pa on pa.omc = pai.omc where pa.full_name = '{selectedMouseCell1}' union all select 'Консервативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" from Сonservative co join patient_in_room pai on pai.id_patient = co.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = co.id_staff where pa.full_name = '{selectedMouseCell1}' union all select 'Оперативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", op.date_operation as \"Дата проведения\", op.time_operation as \"Время проведения\" from operation op join patient_in_room pai on pai.id_patient = op.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = op.id_staff where pa.full_name = '{selectedMouseCell1}' union all select 'Эпикриз' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата проведения\", e.time_extract as \"Время проведения\" from extract_document e join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = e.id_staff where pa.full_name = '{selectedMouseCell1}' union all select 'Лист о нетрудоспособности' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата проведения\", e.time_extract as \"Время проведения\" from list_not_working l join patient pa on l.omc = pa.omc join extract_document e on e.numb_extract = l.numb_extract join staff s on s.id_staff = e.id_staff where pa.full_name = '{selectedMouseCell1}'";
            cl.ShowDGV(queryHistoryData, dataGridView4, db._connectionString);
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) {
                case 0: //учет пациентов
                    {
                        bool flag = true;
                        FormOperationalDataPatientAdd add = new FormOperationalDataPatientAdd(flag, selectedMouseCell0, selectedMouseCell1, selectedMouseCell2, selectedMouseCell3, selectedMouseCell4);
                        add.ShowDialog();
                        cl.ShowDGV(queryPatient, dataGridView1, db._connectionString);
                    }
                    break;
                case 1: //выписные листы
                    {
                        FormAddTypeDockExtract fa = new FormAddTypeDockExtract(selectedMouseCell0);
                        fa.ShowDialog();
                        cl.ShowDGV(queryListOut, dataGridView2, db._connectionString);
                    }
                    break;
                case 2:
                    break;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                selectedMouseCell0 = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); //id_patient
                selectedMouseCell1 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString(); //omc
                selectedMouseCell2 = dataGridView1.SelectedRows[0].Cells[6].Value.ToString(); //hir_hosp
                selectedMouseCell3 = dataGridView1.SelectedRows[0].Cells[7].Value.ToString(); //department
                selectedMouseCell4 = dataGridView1.SelectedRows[0].Cells[8].Value.ToString(); //room
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonDelele_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex) {
                case 0: //учет пациентов
                    {
                        ClassPatientInRoom cp = new ClassPatientInRoom();
                        cp.DeletePatientInRoom(selectedMouseCell0);
                        cl.ShowDGV(queryPatient, dataGridView1, db._connectionString);
                    }
                    break;
                case 1: //выписные листы
                    {
                        ClassLogicExtraction cle = new ClassLogicExtraction();
                        cle.DeleteExtraction(selectedMouseCell0);
                        cl.ShowDGV(queryListOut, dataGridView2, db._connectionString);
                    }
                    break;
                case 2:
                    break;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { selectedMouseCell0 = dataGridView2.SelectedRows[0].Cells[0].Value.ToString(); } catch { }
        }
    }
}
