using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace AndreevNIR
{
    internal class DataBaseCore
    {
        SqlConnection conn = new SqlConnection(@"Data Source=HELLTALE\SQLEXPRESS;Initial Catalog=NIRnew1;Integrated Security=True");
        public void openConnection() {
            if (conn.State == System.Data.ConnectionState.Closed) { 
                conn.Open();
            }
        }

        public void closeConnection() {
            if (conn.State == System.Data.ConnectionState.Open) {
                conn.Close();
            }
        }

        public SqlConnection getConnection() {
            return conn;
        }

        public void showDGV(DataGridView dgv) {
            var select = "select fioPatient as 'ФИО пациента', omc as 'ОМС', snils as 'СНИЛС', diagnosisIn as 'Диагноз' from Patient";
            var dataAdapter = new SqlDataAdapter(select, conn);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dgv.ReadOnly = true;
            dgv.DataSource = ds.Tables[0];
        }

        public void newRecord(System.Windows.Forms.TextBox txtb1, System.Windows.Forms.TextBox txtb2, System.Windows.Forms.TextBox txtb3, RichTextBox rtxtb1) {
            var insert = "insert into Patient values(@omc,@diagnosisIn,@snils,@fioPatient)";
            using (SqlCommand cmd = new SqlCommand(insert, conn))
            {
                // Create and set the parameters values 
                cmd.Parameters.Add("@fioPatient", SqlDbType.NVarChar).Value = txtb1.Text;
                cmd.Parameters.Add("@omc", SqlDbType.NVarChar).Value = txtb2.Text;
                cmd.Parameters.Add("@snils", SqlDbType.NVarChar).Value = txtb3.Text;
                cmd.Parameters.Add("@diagnosisIn", SqlDbType.NVarChar).Value = rtxtb1.Text;

                // Let's ask the db to execute the query
                int rowsAdded = cmd.ExecuteNonQuery();
                if (rowsAdded > 0)
                    MessageBox.Show("Row inserted!!");
                else
                    // Well this should never really happen
                    MessageBox.Show("No row inserted");
            }
        }
        
        public void deleteRecord(List<string> lis) {
            var delete = "delete from Patient where omc='"+ lis[0] +"'";
            using (SqlCommand cmd = new SqlCommand(delete, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void getListsDelete(DataGridView dgv) {
            try
            {
                List<string> dFioPatient = new List<string>();
                List<string> dOmc = new List<string>();
                List<string> dSnils = new List<string>();
                List<string> dDiagnosisIn = new List<string>();


                foreach (DataGridViewRow r in dgv.SelectedRows)
                {
                    dFioPatient.Add(r.Cells[0].Value.ToString());
                    dOmc.Add(r.Cells[1].Value.ToString());
                    dSnils.Add(r.Cells[2].Value.ToString());
                    dDiagnosisIn.Add(r.Cells[3].Value.ToString());
                }
                deleteRecord(dOmc);
            }
            catch {
                MessageBox.Show("Choose row!");
            }
            
        }
        
        public void editRecord(List<string> dOmc, System.Windows.Forms.TextBox txtb1, System.Windows.Forms.TextBox txtb2, System.Windows.Forms.TextBox txtb3, RichTextBox rtxtb1) {
            var update = "update Patient set omc = '"+txtb1.Text+"' , diagnosisIn = '"+ rtxtb1.Text + "' , snils= '"+ txtb3.Text + "', fioPatient = '"+ txtb1.Text + "' where omc = '" + dOmc[0] +"'";
            using (SqlCommand cmd = new SqlCommand(update, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void getListEdit(DataGridView dgv, 
            System.Windows.Forms.TextBox txtb1, System.Windows.Forms.TextBox txtb2, System.Windows.Forms.TextBox txtb3, RichTextBox rtxtb1) {
            
                List<string> dFioPatient = new List<string>();
                List<string> dOmc = new List<string>();
                List<string> dSnils = new List<string>();
                List<string> dDiagnosisIn = new List<string>();


                foreach (DataGridViewRow r in dgv.SelectedRows)
                {
                    dFioPatient.Add(r.Cells[0].Value.ToString());
                    dOmc.Add(r.Cells[1].Value.ToString());
                    dSnils.Add(r.Cells[2].Value.ToString());
                    dDiagnosisIn.Add(r.Cells[3].Value.ToString());
                }
                editRecord(dOmc, txtb1, txtb2, txtb3, rtxtb1);
            
        }

        public void getReport() {
            Form2 fr2 = new Form2();
            fr2.Show();

            string select = "select * from Patient";
            SqlDataAdapter sda = new SqlDataAdapter(select, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "Patient");
            CrystalReport1 report1 = new CrystalReport1();
            report1.SetDataSource(ds);
            fr2.crystalReportViewer1.ReportSource = report1;
            fr2.crystalReportViewer1.Refresh();
        }
    }
}
