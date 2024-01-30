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

namespace AndreevNIR.ReferenceData.FormAddStructF
{
    public partial class FormHospitalUpdate : Form
    {
        private string idHirDepartment = null;
        private string nameHirDepartment = null;
        DBLogicConnection dB = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();

        public FormHospitalUpdate(string nameDepartment_)
        {
            InitializeComponent();
            nameHirDepartment = nameDepartment_;
            cl.LoadComboboxByQuery(comboBox1, "select t2.full_name  from Hir_hosp_Boss t1 left outer join staff t2 on t1.id_staff = t2.id_staff;", "Выберите главного врача");
            GetDataForUpdate();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void GetDataForUpdate()
        {
            string idHirDepartment_ = null;
            string idBossHirDepartment = null;

            //получение id стационара по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select code_hir_department from hir_hospital where name_hir_department = '{nameHirDepartment}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idHirDepartment_ = reader["code_hir_department"].ToString();
                        }
                    }
                }
            }

            idHirDepartment = idHirDepartment_;

            //вывод всех значений в существующие поля
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from hir_hospital where code_hir_department = '{idHirDepartment}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            textBox4.Text = reader["name_hir_department"].ToString();
                            textBox3.Text = reader["adress_hir_department"].ToString(); 
                            textBox1.Text = reader["phone_hir_department"].ToString();
                            idBossHirDepartment = reader["boss_hir_department"].ToString();
                            textBox2.Text = reader["ogrm_hir_department"].ToString();
                        }
                    }
                }

            }

            //вывод вывод босса в комбобокс по его id
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select full_name from staff where id_staff = '{idBossHirDepartment}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            comboBox1.Text = reader["full_name"].ToString();
                        }
                    }
                }

            }
        }

        public void SendDataForUpdate() {
            FormAddStruct1 fas1 = new FormAddStruct1();
            string idBossHirDepartment = null;

            //получим id управленца по имени
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select id_Staff from staff where full_name = '{comboBox1.Text}'", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idBossHirDepartment = reader["id_Staff"].ToString();
                        }
                    }
                }
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update hir_hospital set name_hir_department = @name_hir_department, adress_hir_department = @adress_hir_department, boss_hir_department = @boss_hir_department, phone_hir_department = @phone_hir_department, ogrm_hir_department = @ogrm_hir_department where code_hir_department = @id", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox4.Text;
                        command.Parameters.Add("@adress_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox3.Text;
                        command.Parameters.Add("@boss_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idBossHirDepartment;
                        command.Parameters.Add("@phone_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox1.Text;
                        command.Parameters.Add("@ogrm_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox2.Text;
                        
                        command.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = idHirDepartment;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Запись обновлена");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    this.Close();
                }
            }
        }


        private string GetLastValue() {
            FormAddStruct1 fas1 = new FormAddStruct1();
            return fas1.lastValue.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SendDataForUpdate();
        }
    }
}
