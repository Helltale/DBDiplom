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
            cl.LoadComboboxByQuery(comboBox1, "select name_hir_department from hir_hospital", "Выберите стационар для более подробной информации");
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "Выберите стационар для более подробной информации";
            dataGridView1.DataSource = null;
        }

        private void PrimeTime_Load(object sender, EventArgs e)
        {
            cp.GetNumberOfPatients(label1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null) {
                string name_hir_department = comboBox1.SelectedValue.ToString();
                cl.ShowDGV($"select * from patient_in_room t1 join hir_hospital t2 on t1.code_hir_department = t2.code_hir_department where t2.name_hir_department = '{name_hir_department}' ", dataGridView1, db._connectionString);
            }
            
        }
    }
}
