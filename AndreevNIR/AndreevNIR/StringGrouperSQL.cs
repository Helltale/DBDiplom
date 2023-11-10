using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreevNIR
{
    class StringGrouperSQL
    {
        
        //filter
        private const string STAFF_FIO = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", (SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", (SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department where staff.full_name = @find";
        private const string STAFF_JOB_TITLE = ""; //проблемный
        private const string STAFF_NAME_DEPARTMENT = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                                "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                                "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                                "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                                "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department where type_department.name_department = @find";
        private const string STAFF_NAME_HIR_HOSPITAL = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                                "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                                "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                                "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                                "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department where hir_hospital.name_hir_department = @find";
        private const string STAFF_PHONE = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                                "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                                "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                                "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                                "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department where staff.phone = @find";
        private const string STAFF_MAIL = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                                    "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                                    "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                                    "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                                    "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                                    "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                                    "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                                    "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                                    "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department where staff.mail = @find";
        private const string STAFF_USER_TITLE = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                                "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                                "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                                "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                                "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department where user_info.role_user = @find";
        private const string STAFF_BOSS_DEPARTMENT = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                                "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                                "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                                "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                                "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department WHERE department.boss_department = (SELECT id_staff FROM staff WHERE full_name = @find);";
        private const string STAFF_BOSS_HIR_HOSPITAL = "SELECT staff.full_name AS \"ФИО\", CASE WHEN guard_nurse.id_staff IS NOT NULL THEN 'Постовая мед сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' " +
                                "WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' END AS \"Должность\", type_department.name_department AS \"Название отделения\", " +
                                "hir_hospital.name_hir_department AS \"Название стационара\", staff.phone AS \"Телефон\", staff.mail AS \"Почта\", user_info.role_user AS \"Уровень доступа\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = department.boss_department) AS \"Начальник отделения\", " +
                                "(SELECT full_name FROM staff WHERE id_staff = hir_hospital.boss_hir_department) AS \"Начальник стационара\" FROM staff " +
                                "FULL OUTER JOIN user_info ON staff.id_staff = user_info.id_staff FULL OUTER JOIN receptionist ON receptionist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN guard_nurse ON guard_nurse.id_staff = staff.id_staff FULL OUTER JOIN therapist ON therapist.id_staff = staff.id_staff " +
                                "FULL OUTER JOIN department ON department.id_department = staff.id_department FULL OUTER JOIN type_department ON department.id_department = type_department.id_department " +
                                "FULL OUTER JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department WHERE hir_hospital.boss_hir_department = (SELECT id_staff FROM staff WHERE full_name = @find);";
        
        private const string STRUCTURE_NAME_HIR_DEPARTMENT = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff where name_hir_department = @find;";
        private const string STRUCTURE_ADRESS = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff where adress_hir_department = @find;";
        private const string STRUCTURE_PHONE = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff where phone_hir_department = @find;";
        private const string STRUCTURE_OGRM = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff where ogrm_hir_department = @find;";
        private const string STRUCTURE_BOSS_HIR_DEPARTMENT = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff WHERE hir_hospital.boss_hir_department = (SELECT id_staff FROM staff WHERE full_name = @find);";
        private const string STRUCTURE_DEPARTMENT = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff where name_department = @find;";
        private const string STRUCTURE_BOSS_DEPARTMENT = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff WHERE department.boss_department = (SELECT id_staff FROM staff WHERE full_name = @find);";
        private const string STRUCTURE_ROOM = "SELECT name_hir_department as \"Стационар\", adress_hir_department as \"Адрес стационара\", phone_hir_department as \"Телефон регистратуры\", ogrm_hir_department as \"ОГРМ\", (select full_name from staff where id_staff = hir_hospital.boss_hir_department) as \"Главный врач\",name_department as \"Отделение\", (select full_name from staff where id_staff = department.boss_department) as \"Заведующий отделением\", number_room as \"Палата\" FROM type_department JOIN department ON type_department.id_department = department.id_department JOIN hir_hospital ON department.code_hir_department = hir_hospital.code_hir_department JOIN room ON department.id_department = room.id_department join staff on hir_hospital.boss_hir_department = staff.id_staff where number_room = @find;";

        private const string TREATMENT_PLAN_NAME_DOC = "select s.full_name as \"ФИО врача\", pa.full_name as \"ФИО пациента\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\", me.discription_meeting as \"Описание осмотра\", me.operation_control as \"Послеоперационный осмотр\" from meetings me inner join patient_in_room pai on me.id_patient = pai.id_patient inner join patient pa on pa.omc = pai.omc inner join staff s on s.id_staff = me.id_staff where s.full_name = @find";
        private const string TREATMENT_PLAN_NAME_PATIENT = "select s.full_name as \"ФИО врача\", pa.full_name as \"ФИО пациента\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\", me.discription_meeting as \"Описание осмотра\", me.operation_control as \"Послеоперационный осмотр\" from meetings me inner join patient_in_room pai on me.id_patient = pai.id_patient inner join patient pa on pa.omc = pai.omc inner join staff s on s.id_staff = me.id_staff where pa.full_name = @find";
        private const string TREATMENT_PLAN_DATE = "select s.full_name as \"ФИО врача\", pa.full_name as \"ФИО пациента\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\", me.discription_meeting as \"Описание осмотра\", me.operation_control as \"Послеоперационный осмотр\" from meetings me inner join patient_in_room pai on me.id_patient = pai.id_patient inner join patient pa on pa.omc = pai.omc inner join staff s on s.id_staff = me.id_staff where me.date_meeting = @find";
        private const string TREATMENT_PLAN_TIME = "select s.full_name as \"ФИО врача\", pa.full_name as \"ФИО пациента\", me.date_meeting as \"Дата проведения\", me.time_meeting as \"Время проведения\", me.discription_meeting as \"Описание осмотра\", me.operation_control as \"Послеоперационный осмотр\" from meetings me inner join patient_in_room pai on me.id_patient = pai.id_patient inner join patient pa on pa.omc = pai.omc inner join staff s on s.id_staff = me.id_staff where me.time_meeting = @find";

        private const string TREATMENT_CONSERV_NAME_PATIENT = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce where pa.full_name = @find;";
        private const string TREATMENT_CONSERV_NAME_DOC = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce where s.full_name = @find;";
        private const string TREATMENT_CONSERV_DATE = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce where co.date_procedure = @find;";
        private const string TREATMENT_CONSERV_TIME = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce where co.time_procedure = @find;";
        private const string TREATMENT_CONSERV_NAME_PROC = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce where pr.name_drocedure = @find;";
        private const string TREATMENT_CONSERV_NAME_NURCE = "SELECT pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врач\", pr.name_drocedure as \"Название процедуры\", n.full_name as \"ФИО мед сестры\", co.date_procedure as \"Дата проведения\", co.time_procedure as \"Время проведения\" FROM Сonservative co JOIN staff s ON co.id_staff = s.id_staff JOIN staff n ON n.id_staff = co.id_staff_nurce JOIN patient_in_room pai ON pai.id_patient = co.id_patient JOIN patient pa ON pa.omc = pai.omc JOIN procedures_ pr ON pr.id_procedure = co.id_procedure JOIN guard_nurse gn ON gn.id_staff = co.id_staff_nurce where n.full_name = @find;";

        private const string TREATMENT_OPERATION_NAME_PATIENT = "select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc where pa.full_name = @find";
        private const string TREATMENT_OPERATION_NAME_DOC = "select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc where s.full_name = @find";
        private const string TREATMENT_OPERATION_DATE = "select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc where o.date_operation = @find";
        private const string TREATMENT_OPERATION_TIME = "select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc where o.time_operation = @find";
        private const string TREATMENT_OPERATION_NAME = "select pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", o.name_operation as \"Название операции\", o.date_operation as \"Дата проведения\", o.time_operation as \"Время проведения\", o.discriptionary_operation as \"Описание\", o.discriptionary_bad as \"Описание осложнений\" from operation o join staff s on s.id_staff = o.id_staff join patient_in_room dir on dir.id_patient = o.id_patient join patient pa on pa.omc = dir.omc where o.name_operation = @find";

        private const string DOCUMENT_EXTRACT_NUMBER = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc where e.numb_extract = @find";
        private const string DOCUMENT_EXTRACT_NAME_PATIENT = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc where pa.full_name = @find";
        private const string DOCUMENT_EXTRACT_NAME_DOC = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc where s.full_name = @find";
        private const string DOCUMENT_EXTRACT_DATE = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc where e.date_extract = @find";
        private const string DOCUMENT_EXTRACT_DIAGNOSIS = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc where e.diagnosis_extract = @find";
        private const string DOCUMENT_EXTRACT_DEATH = "select e.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", s.full_name as \"ФИО врача\", e.date_extract as \"Дата выписки\", e.diagnosis_extract as \"Диагноз\", e.recomendations as \"Рекомендации\", e.death_mark as \"Летальный исход\" from extract_document e join staff s on s.id_staff = e.id_staff join patient_in_room pai on pai.id_patient = e.id_patient join patient pa on pa.omc = pai.omc where e.death_mark = @find";

        private const string DOCUMENT_INITIAL_NAME_PATIENT = "select pa.full_name as \"ФИО пациента\", i.date_initial as \"Дата первичного осмотра\", s.full_name as \"ФИО врача приёмного покоя\", i.diagnosis as \"Диагноз\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist where pa.full_name = @find;";
        private const string DOCUMENT_INITIAL_DATE = "select pa.full_name as \"ФИО пациента\", i.date_initial as \"Дата первичного осмотра\", s.full_name as \"ФИО врача приёмного покоя\", i.diagnosis as \"Диагноз\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist where i.date_initial = @find;";
        private const string DOCUMENT_INITIAL_NAME_DOC = "select pa.full_name as \"ФИО пациента\", i.date_initial as \"Дата первичного осмотра\", s.full_name as \"ФИО врача приёмного покоя\", i.diagnosis as \"Диагноз\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist where s.full_name = @find;";
        private const string DOCUMENT_INITIAL_DIAGNOSIS = "select pa.full_name as \"ФИО пациента\", i.date_initial as \"Дата первичного осмотра\", s.full_name as \"ФИО врача приёмного покоя\", i.diagnosis as \"Диагноз\" from initial_inspection i join patient pa on pa.omc = i.omc join staff s on s.id_staff = i.doc_receptinoist where i.diagnosis = @find;";

        private const string DOCUMENT_NOT_WORKING_NAME_PATIENT = "select l.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", l.date_in as \"Дата поступления\" from list_not_working l join patient pa on pa.omc = l.omc where pa.full_name = @find";
        private const string DOCUMENT_NOT_WORKING_NUMBER = "select l.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", l.date_in as \"Дата поступления\" from list_not_working l join patient pa on pa.omc = l.omc where l.numb_extract = @find";
        private const string DOCUMENT_NOT_WORKING_DATE = "select l.numb_extract as \"Номер выписки\", pa.full_name as \"ФИО пациента\", l.date_in as \"Дата поступления\" from list_not_working l join patient pa on pa.omc = l.omc where l.date_in = @find";

        private const string PROCEDURE_NAME_PROC = "select procedures_.name_drocedure \"Название процедуры\", drug.name_drug \"Название препарата\", procedures_.value_drug \"Количество\", procedures_.value_name \"Тип\" from procedures_ join drug on procedures_.id_drug = drug.id_drug where procedures_.name_drocedure = @find";
        private const string PROCEDURE_NAME_DRUG = "select procedures_.name_drocedure \"Название процедуры\", drug.name_drug \"Название препарата\", procedures_.value_drug \"Количество\", procedures_.value_name \"Тип\" from procedures_ join drug on procedures_.id_drug = drug.id_drug where drug.name_drug = @find";
        private const string PROCEDURE_COUNT_DRUG = "select procedures_.name_drocedure \"Название процедуры\", drug.name_drug \"Название препарата\", procedures_.value_drug \"Количество\", procedures_.value_name \"Тип\" from procedures_ join drug on procedures_.id_drug = drug.id_drug where procedures_.value_drug = @find";
        private const string PROCEDURE_TYPE_VALUE = "select procedures_.name_drocedure \"Название процедуры\", drug.name_drug \"Название препарата\", procedures_.value_drug \"Количество\", procedures_.value_name \"Тип\" from procedures_ join drug on procedures_.id_drug = drug.id_drug where procedures_.value_name = @find";

        private const string ROLE_NAME_STAFF = "select staff.full_name ФИО, user_info.login_user Логин, user_info.role_user \"Уровень доступа\" from staff join user_info on staff.id_staff = user_info.id_staff where staff.full_name = @find";
        private const string ROLE_LOGIN = "select staff.full_name ФИО, user_info.login_user Логин, user_info.role_user \"Уровень доступа\" from staff join user_info on staff.id_staff = user_info.id_staff where user_info.login_user = @find";
        private const string ROLE_ROLE_LEVEL = "select staff.full_name ФИО, user_info.login_user Логин, user_info.role_user \"Уровень доступа\" from staff join user_info on staff.id_staff = user_info.id_staff where user_info.role_user = @find";


        private List<string> ReferenseFilterQuery = new List<string>();
        public void CreateReferenseQueryList() {
            ReferenseFilterQuery.Add(STAFF_FIO);
            ReferenseFilterQuery.Add(STAFF_JOB_TITLE);
            ReferenseFilterQuery.Add(STAFF_NAME_DEPARTMENT);
            ReferenseFilterQuery.Add(STAFF_NAME_HIR_HOSPITAL);
            ReferenseFilterQuery.Add(STAFF_PHONE);
            ReferenseFilterQuery.Add(STAFF_MAIL);
            ReferenseFilterQuery.Add(STAFF_USER_TITLE);
            ReferenseFilterQuery.Add(STAFF_BOSS_DEPARTMENT);
            ReferenseFilterQuery.Add(STAFF_BOSS_HIR_HOSPITAL);

            ReferenseFilterQuery.Add(STRUCTURE_NAME_HIR_DEPARTMENT);
            ReferenseFilterQuery.Add(STRUCTURE_ADRESS);
            ReferenseFilterQuery.Add(STRUCTURE_PHONE);
            ReferenseFilterQuery.Add(STRUCTURE_OGRM);
            ReferenseFilterQuery.Add(STRUCTURE_BOSS_HIR_DEPARTMENT);
            ReferenseFilterQuery.Add(STRUCTURE_DEPARTMENT);
            ReferenseFilterQuery.Add(STRUCTURE_BOSS_DEPARTMENT);
            ReferenseFilterQuery.Add(STRUCTURE_ROOM);

            ReferenseFilterQuery.Add(TREATMENT_PLAN_NAME_DOC);
            ReferenseFilterQuery.Add(TREATMENT_PLAN_NAME_PATIENT);
            ReferenseFilterQuery.Add(TREATMENT_PLAN_DATE);
            ReferenseFilterQuery.Add(TREATMENT_PLAN_TIME);

            ReferenseFilterQuery.Add(TREATMENT_CONSERV_NAME_PATIENT);
            ReferenseFilterQuery.Add(TREATMENT_CONSERV_NAME_DOC);
            ReferenseFilterQuery.Add(TREATMENT_CONSERV_DATE);
            ReferenseFilterQuery.Add(TREATMENT_CONSERV_TIME);
            ReferenseFilterQuery.Add(TREATMENT_CONSERV_NAME_PROC);
            ReferenseFilterQuery.Add(TREATMENT_CONSERV_NAME_NURCE);

            ReferenseFilterQuery.Add(TREATMENT_OPERATION_NAME_PATIENT);
            ReferenseFilterQuery.Add(TREATMENT_OPERATION_NAME_DOC);
            ReferenseFilterQuery.Add(TREATMENT_OPERATION_DATE);
            ReferenseFilterQuery.Add(TREATMENT_OPERATION_TIME);
            ReferenseFilterQuery.Add(TREATMENT_OPERATION_NAME);

            ReferenseFilterQuery.Add(DOCUMENT_EXTRACT_NUMBER);
            ReferenseFilterQuery.Add(DOCUMENT_EXTRACT_NAME_PATIENT);
            ReferenseFilterQuery.Add(DOCUMENT_EXTRACT_NAME_DOC);
            ReferenseFilterQuery.Add(DOCUMENT_EXTRACT_DATE);
            ReferenseFilterQuery.Add(DOCUMENT_EXTRACT_DIAGNOSIS);
            ReferenseFilterQuery.Add(DOCUMENT_EXTRACT_DEATH);

            ReferenseFilterQuery.Add(DOCUMENT_INITIAL_NAME_PATIENT);
            ReferenseFilterQuery.Add(DOCUMENT_INITIAL_DATE);
            ReferenseFilterQuery.Add(DOCUMENT_INITIAL_NAME_DOC);
            ReferenseFilterQuery.Add(DOCUMENT_INITIAL_DIAGNOSIS);

            ReferenseFilterQuery.Add(DOCUMENT_NOT_WORKING_NAME_PATIENT);
            ReferenseFilterQuery.Add(DOCUMENT_NOT_WORKING_NUMBER);
            ReferenseFilterQuery.Add(DOCUMENT_NOT_WORKING_DATE);

            ReferenseFilterQuery.Add(PROCEDURE_NAME_PROC);
            ReferenseFilterQuery.Add(PROCEDURE_NAME_DRUG);
            ReferenseFilterQuery.Add(PROCEDURE_COUNT_DRUG);
            ReferenseFilterQuery.Add(PROCEDURE_TYPE_VALUE);

            ReferenseFilterQuery.Add(ROLE_NAME_STAFF);
            ReferenseFilterQuery.Add(ROLE_LOGIN);
            ReferenseFilterQuery.Add(ROLE_ROLE_LEVEL);
        }

        public string OpenReferenseQueryList(int index) {
            return ReferenseFilterQuery[index];
        }
    }
}
