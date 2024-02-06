using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.OperationalData.Patients
{
    public partial class FormAddPatient : Form
    {
        bool isChange = false;
        string omc = null;
        public FormAddPatient()
        {
            InitializeComponent();
            FullUpComboboxes();
            CoreLogic cl = new CoreLogic();
            cl.LoadComboboxByQuery(comboBox4, "select t2.full_name from receptionist t1 join staff t2 on t1.id_staff = t2.id_staff", "Врач приёмного покоя");
        }

        public FormAddPatient(string _omc) {
            isChange = true;
            omc = _omc;
            InitializeComponent();

            CoreLogic cl = new CoreLogic();
            cl.LoadComboboxByQuery(comboBox4, "select t2.full_name from receptionist t1 join staff t2 on t1.id_staff = t2.id_staff", "Врач приёмного покоя");
            FullUpComboboxes();

            ClassPatientAddLogic cp = new ClassPatientAddLogic();
            cp.LoadPatient(_omc, textBox1, textBox2, textBox4, textBox3, textBox5, comboBox1, comboBox2, richTextBox1, textBox8, textBox11, textBox9, dateTimePicker2, 
                textBox10, textBox7, textBox12, textBox13, comboBox4, dateTimePicker1, textBox6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClassPatientAddLogic cp = new ClassPatientAddLogic();
            if (isChange) {
                cp.UpdatePatient(omc, textBox1, textBox2, textBox4, textBox3, textBox5, comboBox1, comboBox2, richTextBox1, textBox8, textBox11, textBox9, dateTimePicker2,
                textBox10, textBox7, textBox12, textBox13, comboBox4, dateTimePicker1, textBox6);
            }
            else {
                
                cp.CreatePatient(textBox4, textBox3, textBox2, textBox1, textBox5, comboBox1, comboBox2, richTextBox1, textBox8, textBox9, dateTimePicker2,
                    textBox11, textBox10, textBox7, textBox12, dateTimePicker1, textBox6, comboBox4, textBox13);
            }
            this.Close();
        }

        private void FullUpComboboxes() {
            {
                List<string> list = new List<string>();
                list.Add("+"); list.Add("-");
                ClassPatientAddLogic cp = new ClassPatientAddLogic();
                cp.FullUpComboboxe(comboBox1, list, "Параметр");
            }
            {
                List<string> list = new List<string>();
                list.Add("1"); list.Add("2"); list.Add("3"); list.Add("4");
                ClassPatientAddLogic cp = new ClassPatientAddLogic();
                cp.FullUpComboboxe(comboBox2, list, "Параметр");
            }
        }

        

        private void FormAddPatient_Load(object sender, EventArgs e)
        {
            
        }
    }
}
