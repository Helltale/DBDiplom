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

namespace AndreevNIR.ReferenceData.FormAddStruct
{
    public partial class FormDepartment : Form
    {

        public FormDepartment()
        {
            InitializeComponent();
            SetValuesInHirHospital();
            SetValuesInBoss();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string id_staff = null;
            string code_hir_department = null;


            DBLogicConnection dB = new DBLogicConnection();
            string stringLastID = dB.GetLastId(dB._connectionString, "type_department", "id_department");
            int intLastID = int.Parse(stringLastID);
            intLastID++;
            stringLastID = intLastID.ToString(); //id_department

            //создание type_department
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into type_department (id_department, name_department) values (@id, @name);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = stringLastID;
                        command.Parameters.Add("@name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox4.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }

            //получение id_заведущего по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_staff from staff where full_name = '{comboBox2.SelectedItem.ToString()}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id_staff = reader["id_staff"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //получение id_стационара по name
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{comboBox1.SelectedItem.ToString()}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            code_hir_department = reader["code_hir_department"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }

            //создание department
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into department (code_hir_department, id_department, boss_department) values (@code_hir_department, @id_department, @boss_department);", connection))
                {
                    try
                    {
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = code_hir_department;
                        command.Parameters.Add("@id_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = stringLastID;
                        command.Parameters.Add("@boss_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_staff;
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }

            this.Close();
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
