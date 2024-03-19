using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreevNIR
{
    class SqlStringWrapper
    {
        private Dictionary<string, string> sqlCommands;

        public SqlStringWrapper() {
            sqlCommands = new Dictionary<string, string>();

            
            //CORE Operative Data
            sqlCommands.Add("Patients", "select t1.id_patient, t1.omc, t2.full_name \"ФИО пацента\", t6.full_name \"ФИО врача\", t1.date_room \"Дата попадания\", t7.date_extract \"Дата выписки\", t3.name_hir_department \"Стационар\", t4.name_department \"Отделение\", t1.number_room \"Палата\" from patient_in_room t1 join patient t2 on t1.omc = t2.omc join hir_hospital t3 on t3.code_hir_department = t1.code_hir_department join type_department t4 on t4.id_department = t1.id_department join doc_patient t5 on t5.id_patient = t1.id_patient join staff t6 on t5.id_staff = t6.id_staff full outer join extract_document t7 on t7.id_patient = t5.id_patient and t7.id_staff = t5.id_staff");
            sqlCommands.Add("Extraction", "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc");
            //sqlCommands.Add("HistoryPatient", "select 'Первичный осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", i.date_initial as \"Дата проведения\", i.time_initial as \"Время проведения\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist where pa.full_name = '{selectedMouseCell1}' union all select 'Плановый осмотр' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\" from meetings me join staff s on s.id_staff = me.id_staff join patient_in_room pai on me.id_patient = pai.id_patient join patient pa on pa.omc = pai.omc where pa.full_name = '{selectedMouseCell1}' union all select 'Консервативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" from Сonservative co join patient_in_room pai on pai.id_patient = co.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = co.id_staff where pa.full_name = '{selectedMouseCell1}' union all select 'Оперативное лечение' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", op.date_operation as \"Дата проведения\", op.time_operation as \"Время проведения\" from operation op join patient_in_room pai on pai.id_patient = op.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = op.id_staff where pa.full_name = '{selectedMouseCell1}' union all select 'Эпикриз' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата проведения\", e.time_extract as \"Время проведения\" from extract_document e join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc join staff s on s.id_staff = e.id_staff where pa.full_name = '{selectedMouseCell1}' union all select 'Лист о нетрудоспособности' as \"Мероприятие\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата проведения\", e.time_extract as \"Время проведения\" from list_not_working l join patient pa on l.omc = pa.omc join extract_document e on e.numb_extract = l.numb_extract join staff s on s.id_staff = e.id_staff where pa.full_name = '{selectedMouseCell1}'"); 

        }

        public string GetQuery(string key)
        {
            if (sqlCommands.ContainsKey(key))
            {
                return sqlCommands[key];
            }
            else
            {
                throw new KeyNotFoundException($"Запроса '{key}' не найдено.");
            }
        }
    }
}
