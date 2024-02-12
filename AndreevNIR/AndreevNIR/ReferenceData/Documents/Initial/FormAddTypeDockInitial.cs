using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData.Documents.Initial;

namespace AndreevNIR.ReferenceData
{
    public partial class FormAddTypeDockInitial : Form
    {
        ClassInitial ci = new ClassInitial();
        CoreLogic cl = new CoreLogic();
        private string omc;

        public FormAddTypeDockInitial(string _omc)
        {
            InitializeComponent();
            cl.LoadComboboxByQuery(comboBox4, "select t2.full_name from receptionist t1 join staff t2 on t1.id_staff = t2.id_staff", "Врач приёмного покоя");
            omc = _omc;
            
            ci.GetInitial(_omc, textBox1, textBox13, comboBox4, dateTimePicker1, textBox6);
            textBox1.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ci.ChangeInitial(omc, textBox13, comboBox4, dateTimePicker1, textBox6);
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
