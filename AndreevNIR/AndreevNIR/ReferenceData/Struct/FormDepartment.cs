using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData.FormAddStructF;
using Npgsql;

namespace AndreevNIR.ReferenceData.FormAddStruct
{
    public partial class FormDepartment : Form
    {
        private bool isAdd;
        private string nameDepartment;
        private string idDepartment;

        public FormDepartment(bool isAdd_)
        {
            InitializeComponent();
            isAdd = isAdd_;
            PreparationForm();
        }

        public FormDepartment(bool isAdd_, string nameDepartment_)
        {
            InitializeComponent();
            isAdd = isAdd_;
            PreparationForm();
            nameDepartment = nameDepartment_;
            ClassDepartment cd = new ClassDepartment(nameDepartment_);
            idDepartment = cd.GetDepartment(textBox4, comboBox2, comboBox1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox4);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Название отделения");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.CheckedCombobox(comboBox1); //стационар
                var flag2 = cf.CheckedCombobox(comboBox2); //заведующий отделением

                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1, flag2 });
                var errorMessage2 = cf.GenerateErrorMessageEmptyComboBox(listFill2, "Стационар", "Заведующий отделением");
                if (errorMessage2 == "Значения в следующих выпадающих меню не были выбраны: ")
                {
                    var flag3 = cf.LetterAndSpace(textBox4); // название отделения

                    var listFill4 = new List<bool>();
                    listFill4.AddRange(new bool[] { flag3 });
                    var errorMessage4 = cf.GenerateErrorMessageErrors(listFill4, "Название отделения");
                    if (errorMessage4 == "Следующие поля были заполнены с ошибками: ")
                    {

                        if (isAdd)
                        {
                            ClassDepartment cd = new ClassDepartment();
                            cd.CreateDepartment(textBox4, comboBox2, comboBox1);
                        }
                        else
                        {
                            ClassDepartment cd = new ClassDepartment(nameDepartment);
                            cd.UpdateDepartment(textBox4, comboBox2, comboBox1, idDepartment);
                        }
                        this.Close();
                    }
                    else { MessageBox.Show(errorMessage4); }
                }
                else { MessageBox.Show(errorMessage2); }
            }
            else { MessageBox.Show(errorMessage1); }
        }

        private void PreparationForm() {
            if (!isAdd)
            {
                button8.Text = "Обновить";
            }
            SetValuesInHirHospital();
            SetValuesInBoss();
        }

        private void SetValuesInHirHospital() {
            CoreLogic cl = new CoreLogic();
            cl.LoadComboboxByQuery(comboBox1, "select name_hir_department from hir_hospital;", "Выберите стационар");
        }

        private void SetValuesInBoss()
        {
            CoreLogic cl = new CoreLogic();
            cl.LoadComboboxByQuery(comboBox2, "select full_name from dep_boss t1 join staff t2 on t1.id_staff = t2.id_staff;", "Выберите заведущего");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
