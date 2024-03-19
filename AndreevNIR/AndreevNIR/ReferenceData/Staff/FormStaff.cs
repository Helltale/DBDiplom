using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.ReferenceData.Staff
{
    public partial class FormStaff : Form
    {
        CoreLogic cl = new CoreLogic();

        private string staffID = null;
        private string oldJob = null;

        public FormStaff()
        {
            InitializeComponent();
            comboBox2.Text = "Выберите отделение";
            comboBox2.Enabled = false;
            cl.LoadComboboxByQuery(comboBox1, "select name_hir_department from hir_hospital", "Выберите стационар");
        }

        public FormStaff(string staffID_)
        {
            ClassStaff cs = new ClassStaff();
            InitializeComponent();
            comboBox2.Text = "Выберите отделение";
            comboBox2.Enabled = true;
            cl.LoadComboboxByQuery(comboBox1, "select name_hir_department from hir_hospital", "Выберите стационар");
            staffID = staffID_;
            oldJob = cs.GetStaff(staffID_, textBox10, textBox11, textBox1, textBox2, textBox3, radioButton1, radioButton2, radioButton4, radioButton5, radioButton7, radioButton6, textBox9, textBox7, textBox6, textBox5,
                textBox4, dateTimePicker1, comboBox1, comboBox2);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.Enabled == false)
            {
                comboBox2.Enabled = true;
            }
            string tmpHirHospital = comboBox1.SelectedItem.ToString();
            cl.LoadComboboxByQuery(comboBox2, $"select td.name_department from department d join type_department td on d.id_department = td.id_department join hir_hospital h on d.code_hir_department = h.code_hir_department where name_hir_department = '{tmpHirHospital}'", "Выберите отделение");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox9, textBox10, textBox11);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Имя", "Фамилия", "Отчество", "Серия паспорта", "Номер паспорта", "Код подразделения", "Кем выдан паспорт", "Прописка", "Телефон врача", "Почта");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {

                var errorMessage2 = cf.CheckedRadioBtn("Не была выбрана должность сотрудника",radioButton1, radioButton2, radioButton4, radioButton5, radioButton6, radioButton7);
                if (errorMessage2 == null)
                {
                    var flag1 = cf.CheckedCombobox(comboBox1); //hir_hospital
                    var flag2 = cf.CheckedCombobox(comboBox2); //department

                    var listFill3 = new List<bool>();
                    listFill3.AddRange(new bool[] { flag1, flag2 });
                    var errorMessage3 = cf.GenerateErrorMessageEmptyComboBox(listFill3, "Стационар", "Отделение");
                    if (errorMessage3 == "Значения в следующих выпадающих меню не были выбраны: ")
                    { 
                        var flag3 = cf.LetterAndSpaceAndDash(textBox1); //имя
                        var flag4 = cf.LetterAndSpaceAndDash(textBox2); //фамилия
                        var flag5 = cf.LetterAndSpaceAndDash(textBox3); //отчество
                        var flag6 = cf.Digit(textBox4); // серия паспорта
                        var flag7 = cf.Digit(textBox5); // номер паспорта
                        var flag8 = cf.DigitAndDash(textBox6); // код подразделения
                        var flag9 = cf.LetterAndDotAndCommaAndSpace(textBox7); // кем выдан
                        var flag10 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox9); // пропискаа
                        var flag11 = cf.DigitAndDashAndOpenAndCloseAndPlus(textBox10); // телефон
                        //var flag12 = cf. //почта

                        var listFill4 = new List<bool>();
                        listFill4.AddRange(new bool[] { flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11 });
                        var errorMessage4 = cf.GenerateErrorMessageErrors(listFill4, "Имя", "Фамилия", "Отчество", "Серия паспорта", "Номер паспорта", "Код подразделения", "Кем выдан паспорт", "Прописка", "Телефон врача");
                        if (errorMessage4 == "Следующие поля были заполнены с ошибками: ")
                        {
                            ClassStaff cs = new ClassStaff();
                            if (staffID == null) //добавленине
                            {
                                cs.CreateStaff(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox9, textBox10, textBox11, dateTimePicker1,
                                    radioButton1, radioButton2, radioButton4, radioButton5, radioButton6, radioButton7, comboBox1, comboBox2);
                            }
                            else {              //редактирование
                                cs.UpdateStaff(staffID, oldJob, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox9, textBox10, textBox11, dateTimePicker1,
                                    radioButton1, radioButton2, radioButton4, radioButton5, radioButton6, radioButton7, comboBox1, comboBox2);
                            }
                            this.Close();
                        }
                        else {
                            MessageBox.Show(errorMessage4);
                        }
                    }
                    else {
                        MessageBox.Show(errorMessage3);
                    }
                }
                else {
                    MessageBox.Show(errorMessage2);
                }
            }
            else {
                MessageBox.Show(errorMessage1);
            }
        }
    }
}
