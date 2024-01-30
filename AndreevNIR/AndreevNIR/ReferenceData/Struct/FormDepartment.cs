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
