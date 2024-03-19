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
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox1);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Название препарата");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox1);

                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1 });
                var errorMessage2 = cf.GenerateErrorMessageErrors(listFill2, "Название препарата");
                if (errorMessage2 == "Следующие поля были заполнены с ошибками: ")
                {
                    ClassDrug cd = new ClassDrug();
                    if (id_drug != null)
                    {
                        cd.ChangeDrug(id_drug, textBox1);
                    }
                    else
                    {
                        cd.CreateDrug(textBox1);
                    }
                    this.Close();
                }
                else { MessageBox.Show(errorMessage2); }
            }
            else { MessageBox.Show(errorMessage1); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
