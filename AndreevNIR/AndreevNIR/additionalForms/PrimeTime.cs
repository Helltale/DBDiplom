using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.additionalForms
{
    public partial class PrimeTime : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        ClassPrimeTime cp = new ClassPrimeTime();

        public PrimeTime()
        {
            InitializeComponent();
            
            cl.LoadComboboxByQuery(comboBox1, "select name_hir_department from hir_hospital", "Выберите стационар");


            dataGridView1.DataSource = null;
        }

        private void PrimeTime_Load(object sender, EventArgs e)
        {
            cp.GetNumberOfPatients(label1);
            if (comboBox1.Text == "Выберите стационар")
            {
                label2.Text = "Кол-во пациентов в выбранном стационаре: [none]";
            }
            cl.ShowDGV("SELECT t2.name_hir_department AS \"Хирургический стационар\", COUNT(DISTINCT t1.omc) AS \"Количество пациентов\" FROM patient_in_room t1 JOIN hir_hospital t2 ON t1.code_hir_department = t2.code_hir_department GROUP BY t2.name_hir_department;", dataGridView1, db._connectionString);

            cp.GetTop5Diagnoses(chart1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null) {
                cl.ShowDGV($"SELECT DISTINCT t3.full_name AS \"ФИО пациента\", t3.omc AS \"OMC\", t5.name_department AS \"Отделение\", t1.number_room \"Номер палаты\" FROM patient_in_room t1 JOIN hir_hospital t2 ON t1.code_hir_department = t2.code_hir_department JOIN patient t3 ON t1.omc = t3.omc JOIN department t4 ON t4.id_department = t1.id_department JOIN type_department t5 ON t5.id_department = t4.id_department JOIN initial_inspection t6 ON t1.omc = t6.omc WHERE t2.name_hir_department = '{comboBox1.SelectedItem.ToString()}';", dataGridView1, db._connectionString);
                cp.GetNumberOfPatientsHirHopatal(label2, comboBox1);
            }
        }

        //reset
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "Выберите стационар";
            label2.Text = "Кол-во пациентов в выбранном стационаре: [none]";
            cl.ShowDGV("SELECT t2.name_hir_department AS \"Хирургический стационар\", COUNT(DISTINCT t1.omc) AS \"Количество пациентов\" FROM patient_in_room t1 JOIN hir_hospital t2 ON t1.code_hir_department = t2.code_hir_department GROUP BY t2.name_hir_department;", dataGridView1, db._connectionString);
        }
    }
}
