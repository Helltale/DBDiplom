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
        CoreLogic cl = new CoreLogic();
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
            CheckFields cf = new CheckFields();
            var listFill1 = cf.CheckAllFields(textBox1, textBox2, textBox3, textBox4,textBox11, textBox9, textBox10, textBox7, textBox12, textBox5, textBox8, textBox13, textBox6);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "ФИО", "СНИЛС", "ОМС", "Диагноз", "Серия паспорта", "Номер паспорта", "Кем выдан", "Код паспорта", "Прописка", "Аллергии", "Место жительства",
                "Диагноз", "Время первичного осмотра");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                bool flagE1 = cf.LetterAndSpace(textBox1); //фио
                bool flagE2 = cf.DigitAndDash(textBox2); //снилс
                bool flagE3 = cf.DigitAndDash(textBox4); //омс
                //bool flagE4 = cf.Anything(textBox3); //диагноз при поступлении
                bool flagE5 = cf.Digit(textBox11); //серия паспорта
                bool flagE6 = cf.Digit(textBox9); //номер паспорта
                bool flagE7 = cf.LetterAndDotAndCommaAndSpace(textBox10);  //кем выдан
                bool flagE8 = cf.DigitAndDash(textBox7); //код паспорта
                bool flagE9 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox12); //прописка
                bool flagE10 = cf.LetterAndSpaceAndPunctuation(textBox5); //аллергии
                bool flagE11 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox8); //место жительства
                //bool flagE12 = cf.Anything(textBox13); //диагноз
                bool flagE13 = cf.DigitAndColon(textBox6); //время первичного осмотра
                bool flagEC = cf.CheckedCombobox(comboBox1, comboBox2, comboBox4); //выбраны значения в cb

                //заполнение листа для генерации ошибки
                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flagE1, flagE2, flagE3, flagE5, flagE6, flagE7, flagE8, flagE9, flagE10, flagE11, flagE13 });

                var errorMessage2 = cf.GenerateErrorMessageErrors(listFill2, "ФИО", "СНИЛС", "ОМС", "Серия паспорта", "Номер паспорта", "Кем выдан", "Код паспорта", "Прописка", "Аллергии", "Место жительства", "Время первичного осмотра", 
                    "Не выбраны пункты выпадающего меню");

                //проверка на корректность
                if (errorMessage2 == "Следующие поля были заполнены с ошибками: ")
                {
                    if (isChange)
                    {
                        cp.UpdatePatient(omc, textBox1, textBox2, textBox4, textBox3, textBox5, comboBox1, comboBox2, richTextBox1, textBox8, textBox11, textBox9, dateTimePicker2,
                            textBox10, textBox7, textBox12, textBox13, comboBox4, dateTimePicker1, textBox6);
                    }
                    else
                    {
                        cp.CreatePatient(textBox4, textBox3, textBox2, textBox1, textBox5, comboBox1, comboBox2, richTextBox1, textBox8, textBox9, dateTimePicker2,
                            textBox11, textBox10, textBox7, textBox12, dateTimePicker1, textBox6, comboBox4, textBox13);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage2); //найдены ошибки
                }
            }
            else {
                MessageBox.Show(errorMessage1); //не были заполнены нужные строки
            }

            
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
