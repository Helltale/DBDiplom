using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Procedures
{
    public partial class FormAddDrug : Form
    {
        private string id_drug = null;

        public FormAddDrug()
        {
            InitializeComponent();
        }

        public FormAddDrug(string _id_drug)
        {
            InitializeComponent();
            ClassDrug cd = new ClassDrug();
            cd.GetDrug(_id_drug, textBox1);
            id_drug = _id_drug;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClassDrug cd = new ClassDrug();
            if (id_drug != null)
            {
                cd.ChangeDrug(id_drug, textBox1);
            }
            else {
                cd.CreateDrug(textBox1);
            }
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
