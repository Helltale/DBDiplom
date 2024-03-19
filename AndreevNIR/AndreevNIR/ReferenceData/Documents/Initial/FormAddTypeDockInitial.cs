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
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox13, textBox6);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Диагноз", "Время первичного осмотра");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.CheckedCombobox(comboBox4);

                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1 });
                var errorMessage2 = cf.GenerateErrorMessageEmptyComboBox(listFill2, "Врач приёмного покоя");
                if (errorMessage2 == "Значения в следующих выпадающих меню не были выбраны: ")
                {
                    var flag2 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox13); // диагноз
                    var flag3 = cf.DigitAndColon(textBox6); //время                                                               

                    var listFill3 = new List<bool>();
                    listFill3.AddRange(new bool[] { flag2, flag3 });
                    var errorMessage3 = cf.GenerateErrorMessageErrors(listFill3, "Диагноз", "Время первичного осмотра");
                    if (errorMessage3 == "Следующие поля были заполнены с ошибками: ")
                    {
                        ci.ChangeInitial(omc, textBox13, comboBox4, dateTimePicker1, textBox6);
                        this.Close();
                    }
                    else { MessageBox.Show(errorMessage3); }
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
