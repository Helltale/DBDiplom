using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Roles
{
    public partial class RoleF : Form
    {
        Role r = new Role();
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();

        private string selectedCell = null;

        public RoleF(string login)
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox1.Text = login;
            r.GetUser(login, comboBox1, comboBox2);
            cl.ShowDGV("SELECT staff.id_staff, staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;", dataGridView1, db._connectionString);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cl.ShowDGV($"SELECT staff.id_staff, staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff where staff.full_name like '%{textBox2.Text}%';", dataGridView1, db._connectionString);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cl.ShowDGV("SELECT staff.id_staff, staff.full_name AS \"ФИО\",  CASE WHEN nurce.id_staff IS NOT NULL THEN 'Медицинская сестра' WHEN therapist.id_staff IS NOT NULL THEN 'Врач' WHEN receptionist.id_staff IS NOT NULL THEN 'Врач приёмного покоя' WHEN dep_boss.id_staff IS NOT NULL THEN 'Заведующий отделения' WHEN hir_hosp_boss.id_staff IS NOT NULL THEN 'Главный врач' WHEN big_boss.id_staff IS NOT NULL THEN 'Начальник больницы' END AS \"Должность\", type_department.name_department AS \"Название отделения\", hir_hospital.name_hir_department AS \"Название стационара\" FROM staff  LEFT JOIN user_info ON staff.id_staff = user_info.id_staff LEFT JOIN receptionist ON receptionist.id_staff = staff.id_staff LEFT JOIN dep_boss ON dep_boss.id_staff = staff.id_staff LEFT JOIN hir_hosp_boss ON hir_hosp_boss.id_staff = staff.id_staff LEFT JOIN big_boss ON big_boss.id_staff = staff.id_staff LEFT JOIN therapist ON therapist.id_staff = staff.id_staff LEFT JOIN department ON staff.code_hir_department = department.code_hir_department and staff.id_department = department.id_department LEFT JOIN type_department ON department.id_department = type_department.id_department  LEFT JOIN hir_hospital ON staff.code_hir_department = hir_hospital.code_hir_department LEFT JOIN nurce ON nurce.id_staff = staff.id_staff;", dataGridView1, db._connectionString);
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Role r = new Role();
            r.UpdateUser(textBox1.Text, selectedCell, comboBox1, comboBox2);
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { selectedCell = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); } catch { }
        }
    }
}
