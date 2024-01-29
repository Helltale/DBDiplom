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
        }

        public FormAddStruct1(string id_department, string code_hir_department, string number_room) {
            InitializeComponent();
            ClassStruct cs = new ClassStruct();
            dataForUpdate = cs.GetStruct(comboBox1, comboBox2, numericUpDown2, textBox1, id_department, code_hir_department, number_room);

            isUpdate = true;
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
            FormHospitalUpdate fh = new FormHospitalUpdate(nameDepartment);
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
            if (!isUpdate)
            {
                ClassStruct cs = new ClassStruct();
                cs.CreateRoom(comboBox2, comboBox1, numericUpDown2, textBox1);
                this.Close();
            }
            else {
                ClassStruct cs = new ClassStruct();
                
                cs.UpdateStruct(dataForUpdate, comboBox2.Text, comboBox1.Text, numericUpDown2, textBox1);
            }
            
        }


    }
}
