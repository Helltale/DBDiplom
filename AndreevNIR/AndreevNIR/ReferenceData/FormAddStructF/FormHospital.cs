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

namespace AndreevNIR.ReferenceData.FormAddStruct
{
    public partial class FormHospital : Form
    {
        private string staffID;
        public List<string> listForComboBox;
        public List<string> listIDPlusName;
        CoreLogic cl = new CoreLogic();

        public FormHospital()
        {
            InitializeComponent();
            cl.CreateFullListOfShowDGV("select t1.id_staff ,t2.full_name  from Hir_hosp_Boss t1 left outer join staff t2 on t1.id_staff = t2.id_staff;", listForComboBox, '|');
            cl.LoadComboboxByQuery(comboBox1, "select t2.full_name  from Hir_hosp_Boss t1 left outer join staff t2 on t1.id_staff = t2.id_staff;", "Выберите главного врача");
        }

        


        private void button8_Click(object sender, EventArgs e)
        {
            DBLogicConnection dB = new DBLogicConnection();
            string stringLastID = dB.GetLastId(dB._connectionString, "Hir_hospital", "code_hir_department");
            int intLastID = int.Parse(stringLastID);
            intLastID++;
            stringLastID = intLastID.ToString();

            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO Hir_hospital (code_hir_department, name_hir_department, adress_hir_department, boss_hir_department, phone_hir_department, ogrm_hir_department) VALUES (@code_hir_department, @name_hir_department, @adress_hir_department, @boss_hir_department, @phone_hir_department, @ogrm_hir_department)", connection))
                {
                    try
                    {
                        command.Parameters.Add("@code_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = stringLastID;
                        command.Parameters.Add("@name_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox4.Text;
                        command.Parameters.Add("@adress_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox3.Text;

                        //staffID = cl.GetElementFromListOfShowDGV(listIDPlusName, '|', selectedMouseRowID, 0);



                        command.Parameters.Add("@boss_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = comboBox1.SelectedItem.ToString();
                        command.Parameters.Add("@phone_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox1.Text;
                        command.Parameters.Add("@ogrm_hir_department", NpgsqlTypes.NpgsqlDbType.Varchar).Value = textBox2.Text;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Запись добавлена");
                    }
                    catch (Exception ex){ MessageBox.Show(""+ex); }
                    this.Close();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
