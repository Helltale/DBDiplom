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
        private const string STAFF_JOB_TITLE = "";
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
        }

        public string OpenReferenseQueryList(int index) {
            return ReferenseFilterQuery[index];
        }
    }
}
