using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData;
using AndreevNIR.ReferenceData.FormAddStruct;
using AndreevNIR.ReferenceData.FormAddStructF;
using Npgsql;

namespace AndreevNIR
{
    public partial class FormAddStruct1 : Form
    {
        private string nameDepartment;
        public List<string> fullListDepartmentForComboBox;
        public List<string> listForComboBox;

        bool isUpdate = false;
        string[] dataForUpdate = new string[4];
        
        DBLogicConnection db = new DBLogicConnection();

        public FormAddStruct1()
        {
            InitializeComponent();
            comboBox1.Text = "Выберите стационар";
            comboBox2.Text = "Выберите отделение";

            comboBox2.Enabled = false;

            numericUpDown2.Minimum = 1;
            numericUpDown2.Value = 1;
        }

        public FormAddStruct1(string id_department, string code_hir_department, string number_room) {
            InitializeComponent();
            ClassStruct cs = new ClassStruct();
            dataForUpdate = cs.GetStruct(comboBox1, comboBox2, numericUpDown2, textBox1, id_department, code_hir_department, number_room);

            isUpdate = true;

            numericUpDown2.Minimum = 1;
            numericUpDown2.Value = 1;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            FormHospital fh = new FormHospital();
            fh.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public List<string> GetDataForComboBoxDB(string connectionString, string query)
        {
            List<string> data = new List<string>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> row = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row.Add(reader[i].ToString());
                            }
                            data.Add(string.Join(",", row));
                        }
                    }
                }
            }
            return data;
        }

        private void ShowHirDepartmentCombobox()
        {
            listForComboBox = GetDataForComboBoxDB(db._connectionString, "select name_hir_department from Hir_hospital;");
            comboBox1.DataSource = listForComboBox;
            comboBox1.Text = "Выберите стационар";
        }
        private void ShowDepartmentCombobox()
        {
            listForComboBox = GetDataForComboBoxDB(db._connectionString, $"select t1.name_department from type_department t1 join department t2 on t1.id_department = t2.id_department join hir_hospital t3 on t3.code_hir_department = t2.code_hir_department where name_hir_department = '{lastValue}';");
            comboBox2.DataSource = listForComboBox;
            comboBox2.Text = "Выберите отделение";
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            ShowHirDepartmentCombobox();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            try
            {
                ShowDepartmentCombobox();
            }
            catch { } //чтобы не выбивало ошибки, можно сказать пока что заглушка
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nameDepartment = comboBox1.SelectedItem.ToString();
            FormHospital fh = new FormHospital(nameDepartment);
            fh.ShowDialog();
            ShowHirDepartmentCombobox();
            comboBox2.SelectedItem = null;
            comboBox1.SelectedItem = null;
        }

        public string GetValueComboBox1() {
            if (comboBox1.SelectedItem != null)
            {
                return comboBox1.SelectedItem.ToString();
            }
            else {
                return null;
            }
        }
        public string lastValue;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            lastValue = comboBox1.SelectedItem.ToString();
            comboBox2.Enabled = true;
            ShowDepartmentCombobox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBLogicConnection dB = new DBLogicConnection();
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"DELETE FROM Hir_hospital WHERE name_hir_department = @name;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox1.SelectedItem.ToString();
                        command.ExecuteNonQuery();
                        comboBox1.Text = "Выберите стационар";
                        MessageBox.Show("Запись удалена");
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    
                }
            }
            ShowHirDepartmentCombobox();
            comboBox2.SelectedItem = null;
            comboBox1.SelectedItem = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool isAdd = true;
            FormDepartment fd = new FormDepartment(isAdd);
            fd.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool isAdd = false;
            FormDepartment fd = new FormDepartment(isAdd, comboBox2.SelectedValue.ToString());
            fd.ShowDialog();
            MessageBox.Show("Запись обновлена");
            comboBox2.SelectedItem = null;
            comboBox1.SelectedItem = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClassDepartment cd = new ClassDepartment(comboBox2.SelectedItem.ToString());
            cd.DeleteDepartment();
            MessageBox.Show("Запись удалена");
            comboBox2.SelectedItem = null;
            comboBox1.SelectedItem = null;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CheckFields cf = new CheckFields();

            var listFill1 = cf.CheckAllFields(textBox1);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Палата");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.CheckedCombobox(comboBox1); //hir_hospital
                var flag2 = cf.CheckedCombobox(comboBox2); //department

                var listFill3 = new List<bool>();
                listFill3.AddRange(new bool[] { flag1, flag2 });
                var errorMessage3 = cf.GenerateErrorMessageEmptyComboBox(listFill3, "Стационар", "Отделение");
                if (errorMessage3 == "Значения в следующих выпадающих меню не были выбраны: ")
                {
                    var flag3 = cf.Digit(textBox1); // номер палаты           

                    var listFill4 = new List<bool>();
                    listFill4.AddRange(new bool[] { flag3 });
                    var errorMessage4 = cf.GenerateErrorMessageErrors(listFill4, "Номер палаты");
                    if (errorMessage4 == "Следующие поля были заполнены с ошибками: ")
                    {
                        ClassStruct cs = new ClassStruct();
                        if (!isUpdate)
                        {
                            cs.CreateRoom(comboBox2, comboBox1, numericUpDown2, textBox1);
                        }
                        else
                        {
                            cs.UpdateStruct(dataForUpdate, comboBox2, comboBox1, numericUpDown2, textBox1);
                        }
                        this.Close();
                    }
                    else { MessageBox.Show(errorMessage4); }
                }
                else { MessageBox.Show(errorMessage3); }
            }
            else { MessageBox.Show(errorMessage1); }
            
            
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
