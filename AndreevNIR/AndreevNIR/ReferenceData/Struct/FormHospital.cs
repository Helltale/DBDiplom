using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using AndreevNIR.ReferenceData;
using AndreevNIR;
using AndreevNIR.ReferenceData.FormAddStruct;
using System.Diagnostics;
using AndreevNIR.ReferenceData.Struct;

namespace AndreevNIR.ReferenceData.FormAddStruct
{
    public partial class FormHospital : Form
    {
        private string staffID;
        private string idHirDepartment = null;
        private string nameHirDepartment = null;
        public List<string> listForComboBox;
        public List<string> listIDPlusName;
        CoreLogic cl = new CoreLogic();

        public FormHospital()
        {
            InitializeComponent();
            listIDPlusName = cl.CreateFullListOfShowDGV("select t1.id_staff ,t2.full_name  from Hir_hosp_Boss t1 left outer join staff t2 on t1.id_staff = t2.id_staff;", '|');
            cl.LoadComboboxByQuery(comboBox1, "select t2.full_name  from Hir_hosp_Boss t1 left outer join staff t2 on t1.id_staff = t2.id_staff;", "Выберите главного врача");
        }

        public FormHospital(string nameDepartment_)
        {
            InitializeComponent();
            nameHirDepartment = nameDepartment_;
            cl.LoadComboboxByQuery(comboBox1, "select t2.full_name  from Hir_hosp_Boss t1 left outer join staff t2 on t1.id_staff = t2.id_staff;", "Выберите главного врача");

            ClassHospital ch = new ClassHospital();
            idHirDepartment = ch.GetHospital(nameHirDepartment, textBox1, textBox2, textBox3, textBox4, comboBox1);
        }

        


        private void button8_Click(object sender, EventArgs e)
        {
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox4, textBox3, textBox1, textBox2);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Название стационара", "Адрес стационара", "Телефон регистратуры", "ОГРМ");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.CheckedCombobox(comboBox1);

                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1 });

                var errorMessage2 = cf.GenerateErrorMessageEmptyComboBox(listFill2, "Главный врач");
                if (errorMessage2 == "Значения в следующих выпадающих меню не были выбраны: ")
                {
                    var flag2 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox4); //Название
                    var flag3 = cf.LetterAndDotAndCommaAndSpaceAndDigitAndDash(textBox3); //адрес
                    var flag4 = cf.DigitAndDashAndOpenAndCloseAndPlus(textBox1); //тел регистратуры
                    var flag5 = cf.DigitAndDashAndSpace(textBox2); // огрм

                    var listFill3 = new List<bool>();
                    listFill3.AddRange(new bool[] { flag2, flag3, flag4, flag5 });
                    var errorMessage3 = cf.GenerateErrorMessageErrors(listFill3, "Название стационара", "Адрес стационара", "Телефон регистратуры", "ОГРМ");
                    if (errorMessage3 == "Следующие поля были заполнены с ошибками: ")
                    {
                        ClassHospital ch = new ClassHospital();
                        if (nameHirDepartment == null)
                        { //добавление
                            ch.CreateHospital(textBox4, textBox3, textBox1, textBox2, comboBox1);
                        }
                        else
                        { //изменение
                            ch.ChangeHospital(comboBox1, textBox1, textBox2, textBox3, textBox4, idHirDepartment);
                        }
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
