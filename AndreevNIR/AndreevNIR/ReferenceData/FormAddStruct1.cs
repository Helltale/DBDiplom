using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData.FormAddStruct;
using AndreevNIR.ReferenceData.FormAddStructF;
using Npgsql;

namespace AndreevNIR
{
    public partial class FormAddStruct1 : Form
    {
        public List<string> listForComboBox;
        DBLogicConnection db = new DBLogicConnection();
        public FormAddStruct1()
        {
            InitializeComponent();
            comboBox1.Text = "Выберите стационар";
            comboBox2.Text = "Выберите отделение";
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
            listForComboBox = GetDataForComboBoxDB(db._connectionString, "select name_department from type_department;");
            comboBox2.DataSource = listForComboBox;
            comboBox2.Text = "Выберите отделение";
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            ShowHirDepartmentCombobox();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            ShowDepartmentCombobox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormHospitalUpdate fh = new FormHospitalUpdate();
            fh.ShowDialog();
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
                        command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox1.SelectedItem;
                        command.ExecuteNonQuery();
                        comboBox1.Text = "Выберите стационар";
                        MessageBox.Show("Запись удалена");
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    
                }
            }
        }
    }
}
