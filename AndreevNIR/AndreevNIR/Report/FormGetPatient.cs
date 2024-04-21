using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.Report
{
    public partial class FormGetPatient : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        string selectedCell1 = null;
        private string selection = null;

        public FormGetPatient(string _selection)
        {
            InitializeComponent();
            cl.ShowDGV("select t1.id_patient, t4.full_name \"ФИО пациента\", t1.omc \"OMC\", t1.date_room \"Дата попадания в стационар\", t2.name_hir_department \"Стационар\", t3.name_department \"Отделение\", t1.number_room \"Палата\" from patient_in_room t1 join hir_hospital t2 on t1.code_hir_department = t2.code_hir_department join type_department t3 on t1.id_department = t3.id_department join patient t4 on t4.omc=t1.omc", dataGridView1, db._connectionString);
            dataGridView1.Columns[0].Visible = false;
            selection = _selection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id_meeting = null;
            var saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Word document|*.docx";
            saveFileDialog1.Title = "Сохраните отчет";
            saveFileDialog1.DefaultExt = "Extraction";

            string path = null;

            switch (selection) {
                case "1":
                    {
                        
                        path = "D:\\diplom\\AndreevNIR\\AndreevNIR\\Report\\meeting.docx";
                        GetMeetingForm gmf = new GetMeetingForm(selectedCell1);
                        gmf.ShowDialog();

                    }
                    break;
                case "2":
                    {
                        path = "D:\\diplom\\AndreevNIR\\AndreevNIR\\Report\\extruct.docx";
                    }
                    break;
                case "3":
                    {
                        path = "D:\\diplom\\AndreevNIR\\AndreevNIR\\Report\\operation.docx";
                    }
                    break;
                case "4":
                    {
                        path = "D:\\diplom\\AndreevNIR\\AndreevNIR\\Report\\initial.docx";
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Некорретное значение при выборе отчета");
                    }
                    break;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var newFileName = saveFileDialog1.FileName; // Получаем полный путь и имя выбранного файла

                var helper = new WordHelper(path, newFileName);
                var items = getInformationForDocument(selection, selectedCell1);

                helper.Process(items);
            }
            MessageBox.Show("Отчет сгенерирован");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { selectedCell1 = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); } 
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private Dictionary<string, string> getInformationForDocument(string selection, string id_patient) {
            var result = new Dictionary<string, string>();
            switch (selection) {
                case "1": //meeting
                    {

                        //получение общих сведений
                        using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                        {
                            connection.Open();

                            string query = "SELECT     s.full_name AS \"Лечащий врач\",    m.date_meeting AS \"Дата осмотра\", " +
                                "m.time_meeting AS \"Время осмотра\",    ai.additional_info AS \"Дополнительная информация\", ii.diagnosis AS \"Диагноз\",    " +
                                "ii.date_initial AS \"Дата первичного осмотра\",    ii.time_initial AS \"Время первичного осмотра\",    " +
                                "p.full_name AS \"ФИО пациента\" FROM     meetings m JOIN     Patient_in_room pir ON m.id_patient = pir.id_patient " +
                                "JOIN     Initial_inspection ii ON pir.omc = ii.omc JOIN     Additional_information ai ON ii.omc = ai.omc " +
                                "JOIN     Staff s ON m.id_staff = s.id_staff JOIN     Patient_in_room pir2 ON m.id_patient = pir2.id_patient " +
                                "JOIN     Patient p ON pir2.omc = p.omc " +
                                "where m.id_meeting = '{}';";
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                using (NpgsqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        result.Add("<FIO_THERAPIST>", reader["ogrm"].ToString());
                                        result.Add("<DATE_MEETING>", reader["hir_hospital_name"].ToString());
                                        result.Add("<TIME_MEETING>", reader["hir_hospital_adress"].ToString());
                                        result.Add("<FIO_PATIENT>", reader["number_extract"].ToString());
                                        result.Add("<ADDITIONAL_INFORMATION>", reader["fio_patient"].ToString());
                                        result.Add("<DESCRIPTION_MEETING>", reader["patient_registration"].ToString());
                                        result.Add("<INITIAL_DIAGNOSIS>", reader["patietn_factical_live"].ToString());
                                        result.Add("<DATE_INITIAL>", reader["date_in"].ToString());
                                        result.Add("<TIME_INITIAL>", reader["date_out"].ToString());
                                       
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "2": //получение инфо для ВЫПИСКИ
                    {
                        //получение общих сведений
                        using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                        {
                            connection.Open();

                            string query = "SELECT     t1.id_patient as id_patient,   t2.ogrm_hir_department as ogrm,    " +
                                "t2.name_hir_department as hir_hospital_name,    t2.adress_hir_department as hir_hospital_adress,    " +
                                "t5.numb_extract as number_extract,    t10.full_name as fio_patient,    t12.adress_pass as patient_registration,    " +
                                "t11.adress_real as patietn_factical_live,    t1.date_room as date_in,    t5.date_extract as date_out,    " +
                                "t5.date_extract - t1.date_room as date_count,    t11.omc as omc,    t11.allergy as allergy,    t11.rh as rh,    " +
                                "t11.blood,    t5.diagnosis_extract as diagnosis_out,    t9.diagnosis as diagnosis_in,    t5.recomendations,    " +
                                "t18.full_name as fio_therapist,    t14.full_name as fio_dep_boss,     t5.date_extract FROM patient_in_room t1 " +
                                "JOIN  hir_hospital t2 ON t1.code_hir_department = t2.code_hir_department " +
                                "JOIN type_department t4 ON t1.id_department = t4.id_department JOIN department t3 ON t3.id_department = t1.id_department " +
                                "AND t3.code_hir_department = t2.code_hir_department JOIN extract_document t5 ON t5.id_patient = t1.id_patient " +
                                "JOIN operation t6 ON t6.id_patient = t1.id_patient JOIN staff t7 ON t7.id_staff = t6.id_staff " +
                                "JOIN list_not_working t8 ON t8.omc = t1.omc JOIN initial_inspection t9 ON t9.omc = t1.omc " +
                                "JOIN patient t10 ON t10.omc = t1.omc JOIN public.additional_information t11 ON t11.omc = t1.omc " +
                                "JOIN public.passport t12 ON t12.omc = t1.omc JOIN dep_boss t13 ON t13.id_staff = t3.boss_department " +
                                "JOIN staff t14 ON t14.id_staff = t13.id_staff JOIN hir_hosp_boss t15 ON t15.id_staff = t2.boss_hir_department " +
                                "JOIN staff t16 ON t16.id_staff = t15.id_staff JOIN doc_patient t17 ON t17.id_patient = t1.id_patient " +
                                $"join staff t18 on t17.id_staff = t18.id_staff WHERE t1.id_patient = '{id_patient}';";
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                using (NpgsqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        result.Add("<OGRN>", reader["ogrm"].ToString());
                                        result.Add("<HIR_HOSPITAL_NAME>", reader["hir_hospital_name"].ToString());
                                        result.Add("<HIR_HOSPITAL_ADRESS>", reader["hir_hospital_adress"].ToString());
                                        result.Add("<NUMBER_EXTRACT>", reader["number_extract"].ToString());
                                        result.Add("<FIO_PATIENT>", reader["fio_patient"].ToString());
                                        result.Add("<PATIENT_REGISTRATION>", reader["patient_registration"].ToString());
                                        result.Add("<PATIENT_FACTICAL_LIVE>", reader["patietn_factical_live"].ToString());
                                        result.Add("<DATE_IN>", reader["date_in"].ToString());
                                        result.Add("<DATE_OUT>", reader["date_out"].ToString());
                                        result.Add("<DATE_COUNT>", reader["date_count"].ToString());
                                        result.Add("<PATIENT_ADDITIONAL>", "\nOMC: "+ reader["omc"].ToString() + 
                                            " \nАллергия(-и): " + reader["allergy"].ToString() +
                                            " \nРезус фактор: " + reader["rh"].ToString() +
                                            " \nТип крови: " + reader["blood"].ToString());
                                        result.Add("<DIAGNOSIS_OUT>", reader["diagnosis_out"].ToString());
                                        result.Add("<DIAGNOSIS_IN>", reader["diagnosis_in"].ToString());
                                        result.Add("<RECOMMENDATION>", reader["recomendations"].ToString());
                                        result.Add("<FIO_THERAPIST>", reader["fio_therapist"].ToString());
                                        result.Add("<FIO_DEP_BOSS>", reader["fio_dep_boss"].ToString());
                                        result.Add("<DATE_EXTRACT>", reader["date_extract"].ToString());
                                    }
                                }
                            }
                        }

                        //получение данных об операциях
                        using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                        {
                            connection.Open();

                            var listOperation = new List<string>();
                            var listOperationBad = new List<string>();

                            string query = $"select * from operation WHERE id_patient = '{id_patient}';";
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                using (NpgsqlDataReader reader = command.ExecuteReader())
                                {
                                    int counting = 0;
                                    while(reader.Read())
                                    {
                                        
                                        counting++;
                                        listOperation.Add($"{counting}. Наименование операции: {reader["name_operation"].ToString()}, " +
                                            $"дата проведения: {reader["date_operation"].ToString()}, время проведения: {reader["time_operation"].ToString()}");

                                        listOperationBad.Add($"{counting}. Осложнения: {reader["discriptionary_bad"].ToString()}");
                                    }
                                }
                            }
                            string note = null;
                            foreach (var tmp in listOperation) {
                                note += tmp;
                            }
                            result.Add("<OPERATION>", note);
                            note = null;
                            foreach (var tmp in listOperationBad)
                            {
                                note += tmp;
                            }
                            result.Add("<OPERATION_BAD>", note);
                        }

                        //получение данных об консервативе
                        using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                        {
                            connection.Open();

                            var listDrug = new List<string>();

                            string query = $"SELECT DISTINCT drug.name_drug, procedures_.value_drug, procedures_.value_name FROM \"Сonservative\" t1 " +
                                $"JOIN procedures_ ON t1.id_procedure = procedures_.id_procedure JOIN drug ON procedures_.id_drug = drug.id_drug " +
                                $"WHERE t1.id_patient = '{id_patient}';";
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                using (NpgsqlDataReader reader = command.ExecuteReader())
                                {
                                    int counting = 0;
                                    while (reader.Read())
                                    {
                                        counting++;
                                        listDrug.Add($"{counting}. Название препарата: {reader["name_drug"].ToString()} {reader["value_drug"].ToString()} " +
                                            $"{reader["value_name"].ToString()}");
                                    }
                                }
                            }
                            string note = null;
                            foreach (var tmp in listDrug)
                            {
                                note += tmp;
                            }
                            result.Add("<CONSERVATIVE>", note);
                        }
                    }
                    break;
                case "3": //получение инфо для ОПЕРАЦИИ
                    {
                        //получение общих сведений
                        using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                        {
                            connection.Open();

                            string query = "SELECT     o.date_operation AS \"Дата операции\",     o.time_operation AS \"Время операции\", p.full_name AS \"ФИО пациента\"," +
                                "    o.name_operation AS \"Наименование операции\", ii.diagnosis AS \"Диагноз до операции\",    ai.blood AS \"Группа крови\",    " +
                                "ai.rh AS \"Резус - фактор\", o.discriptionary_operation AS \"Описание операции\",    o.discriptionary_bad AS \"Осложнения операции\"," +
                                "    s.full_name AS \"ФИО оперирующего врача\" FROM     Operation o JOIN     Patient_in_room pir ON o.id_patient = pir.id_patient JOIN     Initial_inspection ii ON pir.omc = ii.omc JOIN     Patient p ON pir.omc = p.omc JOIN     Additional_information ai ON p.omc = ai.omc JOIN     " +
                                $"Staff s ON o.id_staff = s.id_staff where pir.id_patient = '{id_patient}';";
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                using (NpgsqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        DateTime date = Convert.ToDateTime(reader["Дата операции"]).Date;
                                        result.Add("<DATE_OPERATION>", date.ToString());
                                        result.Add("<TIME_OPERATION>", reader["Время операции"].ToString());
                                        result.Add("<FIO_PATIENT>", reader["ФИО пациента"].ToString());
                                        result.Add("<OPERATION_DATE>", reader["Дата операции"].ToString());
                                        result.Add("<OPERATION_TIME>", reader["Время операции"].ToString());
                                        result.Add("<OPERATION_NAME>", reader["Наименование операции"].ToString());
                                        result.Add("<INITIAL_DIAGNOSIS>", reader["Диагноз до операции"].ToString());
                                        result.Add("<BLOOD>", reader["Группа крови"].ToString());
                                        result.Add("<RH>", reader["Резус - фактор"].ToString());
                                        result.Add("<OPERATION_DESCRIPTION>", reader["Описание операции"].ToString());
                                        result.Add("<OPERATION_BAD>", reader["Осложнения операции"].ToString());
                                        result.Add("<FIO_DOC>", reader["ФИО оперирующего врача"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "4": //получение инфо для ПЕРВИЧКИ
                    {
                        //получение общих сведений
                        using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
                        {
                            connection.Open();

                            string query = "SELECT     ii.date_initial AS \"Дата первичного осмотра\",    ii.time_initial AS \"Время первичного осмотра\",    " +
                                "p.full_name AS \"ФИО пациента\",    hh.name_hir_department AS \"Название стационара\",    r.number_room AS \"Номер палаты\",    " +
                                "td.name_department AS \"Название отделения\",    p.omc AS \"ОМС пациента\",    ai.additional_info AS \"Дополнительная информация\",    " +
                                "ai.allergy AS \"Аллергии пациента\",    ai.blood AS \"Группа крови\",     ai.rh AS \"Резус - фактор\",     ii.diagnosis AS \"Диагноз\",   " +
                                "  s.full_name AS \"ФИО врача приемного покоя\" FROM     Initial_inspection ii JOIN     Patient p ON ii.omc = p.omc " +
                                "JOIN Patient_in_room pir ON p.omc = pir.omc AND ii.date_initial = pir.date_room JOIN  Room r ON pir.number_room = r.number_room " +
                                "JOIN     Type_department td ON r.id_department = td.id_department " +
                                "JOIN     Hir_hospital hh ON r.code_hir_department = hh.code_hir_department JOIN     " +
                                "Additional_information ai ON p.omc = ai.omc JOIN     Receptionist rec ON ii.doc_receptinoist = rec.id_staff " +
                                "JOIN     Staff s ON rec.id_staff = s.id_staff " +
                                $"where pir.id_department = '{id_patient}';";
                            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                            {
                                using (NpgsqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        DateTime date = Convert.ToDateTime(reader["Дата первичного осмотра"]).Date;
                                        result.Add("<DATE_INITIAL>", date.ToString());
                                        result.Add("<TIME_INITIAL>", reader["Время первичного осмотра"].ToString());
                                        result.Add("<FIO_PATIENT>", reader["ФИО пациента"].ToString());
                                        result.Add("<NAME_HIR_HOSPITAL>", reader["Название стационара"].ToString());
                                        result.Add("<DEPARTMENT>", reader["Название отделения"].ToString());
                                        result.Add("<OMC_PATIENT>", reader["ОМС пациента"].ToString());
                                        result.Add("<ADDITIONAL_INFO>", reader["Дополнительная информация"].ToString());
                                        result.Add("<PATIENT_ALLERGY>", reader["Аллергии пациента"].ToString());
                                        result.Add("<BLOOD>", reader["Группа крови"].ToString());
                                        result.Add("<RH>", reader["Резус - фактор"].ToString());
                                        result.Add("<DIAGNOSIS>", reader["Диагноз"].ToString());
                                        result.Add("<INITIAL_DOC>", reader["ФИО врача приемного покоя"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    {
                        Console.WriteLine("Некорретное значение при выборе отчета");
                    }
                    break;
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
