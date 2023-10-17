using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.SqlClient;
using Npgsql;

namespace AndreevNIR
{
    public partial class FormReferenceData : Form
    {
        DBLogicConnection dBLogicConnection = new DBLogicConnection();


        public FormReferenceData()
        {
            InitializeComponent();
            LoadGridPersonal();
        }

        public void LoadGridPersonal() {
            
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("SELECT * FROM test1", dBLogicConnection._connectionString);

            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView2.DataSource = table;
        }


        private void buttonBack_Click(object sender, EventArgs e)
        {
            FormIndex2 formIndex2 = new FormIndex2();
            formIndex2.richTextBoxPrimeTime.Show();
            this.Hide();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void FormReferenceData_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReferenceDataAdd add = new FormReferenceDataAdd();
            add.ShowDialog();
        }
    }
}
