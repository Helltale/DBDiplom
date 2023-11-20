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
        }

        //private void F2_UpdateEventHandler(object sender, FormHospital.UpdateEventArgs args);
        
        private void button1_Click(object sender, EventArgs e)
        {
            FormHospital fh = new FormHospital(this);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            ShowHirDepartmentCombobox();
        }

        private void ShowHirDepartmentCombobox() {
            listForComboBox = GetDataForComboBoxDB(db._connectionString, "select name_hir_department from Hir_hospital;");
            comboBox1.DataSource = listForComboBox;
            comboBox1.Text = "Выберите стационар";
        }
    }
}
