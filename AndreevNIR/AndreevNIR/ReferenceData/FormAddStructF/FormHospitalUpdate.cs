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
        DBLogicConnection dB = new DBLogicConnection();
        public FormHospitalUpdate()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetLastValue() {
            FormAddStruct1 fas1 = new FormAddStruct1();
            return fas1.lastValue.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormAddStruct1 fas1 = new FormAddStruct1();
            
            //UPDATE Hir_hospital SET name_hir_department = 'новое_имя_департамента', adress_hir_department = 'новый_адрес_департамента', boss_hir_department = 'новый_руководитель_департамента', phone_hir_department = 'новый_телефон_департамента', ogrm_hir_department = 'новый_ОГРН_департамента' WHERE name_hir_department = 'входящий_name_hir_department';
            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"UPDATE Hir_hospital SET name_hir_department = @name_hir_department, adress_hir_department = @adress_hir_department, boss_hir_department = @boss_hir_department, phone_hir_department = @phone_hir_department, ogrm_hir_department = @ogrm_hir_department WHERE name_hir_department = @old_name;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox4.Text;
                        command.Parameters.Add("@adress_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox3.Text;
                        command.Parameters.Add(new NpgsqlParameter<string>("@boss_hir_department", null));
                        command.Parameters.Add("@phone_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox1.Text;
                        command.Parameters.Add("@ogrm_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox2.Text;
                        string tmp = GetLastValue();
                        command.Parameters.Add("@old_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tmp;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Запись обновлена");
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    this.Close();
                }
            }

        }
    }
}
