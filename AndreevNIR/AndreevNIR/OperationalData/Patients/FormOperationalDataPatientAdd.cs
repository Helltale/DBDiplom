using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.OperationalData.Patients;

namespace AndreevNIR
{
    public partial class FormOperationalDataPatientAdd : Form
    {
        private string strQuery = "select t1.omc, t1.full_name, t2.date_initial, to_char(t2.time_initial, 'HH24:MI:SS') as \"time\", t2.diagnosis, t4.full_name from patient t1 join initial_inspection t2 on t1.omc = t2.omc join receptionist t3 on t3.id_staff = t2.doc_receptinoist join staff t4 on t3.id_staff = t4.id_staff";
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        private string omc;

        public FormOperationalDataPatientAdd()
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            comboBox2.Enabled = false;
            comboBox2.Text = "Параметр";
            comboBox3.Enabled = false;
            comboBox3.Text = "Параметр";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAddPatient fap = new FormAddPatient();
            fap.ShowDialog();
            cl.ShowDGV(strQuery, dataGridView1, db._connectionString);
        }

        private void FormOperationalDataPatientAdd_Load(object sender, EventArgs e)
        {
            cl.ShowDGV(strQuery, dataGridView1, db._connectionString);
            dataGridView1.Columns[0].Visible = false;
            cl.LoadComboboxByQuery(comboBox1, "select name_hir_department from hir_hospital", "Параметр"); //хир стат
            cl.LoadComboboxByQuery(comboBox6, "select t2.full_name from therapist t1 join staff t2 on t1.id_staff = t2.id_staff", "Параметр"); //леч врачи
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.CurrentCell != null && e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Вы хотите удалить запись?",
                                                        "Удаление записи",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Information,
                                                        MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    ClassPatientAddLogic cp = new ClassPatientAddLogic();
                    DataGridViewRow row = dataGridView1.CurrentRow;
                    DataGridViewCell cell = row.Cells[0];

                    cp.DeletePatient(cell.Value.ToString());
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите изменить запись?",
                                                        "Изменение записи",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Information,
                                                        MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                
                DataGridViewRow row = dataGridView1.CurrentRow;
                DataGridViewCell cell = row.Cells[0];

                bool isChange = true;
                FormAddPatient fap = new FormAddPatient(cell.Value.ToString());
                fap.ShowDialog();

                cl.ShowDGV(strQuery, dataGridView1, db._connectionString);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassPatientInRoom cp = new ClassPatientInRoom();
            cp.CreatePatietnInRoom(omc, comboBox1, comboBox2, comboBox3, comboBox6);
            this.Close();
        }

        private void LoadComboboxes() {
            
            
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try { cl.LoadComboboxByQuery(comboBox2, $"select name_department from type_department t1 join department t2 on t1.id_department = t2.id_department " +
                $"join hir_hospital t3 on t2.code_hir_department = t3.code_hir_department " +
                $"where name_hir_department = '{comboBox1.SelectedItem.ToString()}'", "Параметр"); //отделение

                comboBox2.Enabled = true;
            }
            catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                DataGridViewRow row = dataGridView1.CurrentRow;
                DataGridViewCell cell = row.Cells[0];
                omc = cell.Value.ToString();
            } catch { }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                
                comboBox3.Enabled = true;
            }
            catch { }
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            cl.LoadComboboxByQuery(comboBox3, $"select number_room from type_department t1 join department t2 on t1.id_department = t2.id_department " +
                    $"join hir_hospital t3 on t2.code_hir_department = t3.code_hir_department join room t4 on t2.id_department = t4.id_department and " +
                    $"t4.code_hir_department = t3.code_hir_department " +
              $"where name_hir_department = '{comboBox1.SelectedItem.ToString()}' and name_department = '{comboBox2.SelectedItem.ToString()}'", "Параметр"); //отделение
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
