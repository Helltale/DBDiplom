using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR
{
    public partial class FormOperationalDataHistory : Form
    {
        public FormOperationalDataHistory()
        {
            InitializeComponent();
            DGVList();
        }

        public string currentName;

        private void FormOperationalDataHistory_Load(object sender, EventArgs e)
        {

        }

        public void DGV() {

            DBLogicConnection dBLogicConnection = new DBLogicConnection();

            string str1 = "";
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);

                dataGridView2.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }

        public void DGVList()
        {
            DBLogicConnection dBLogicConnection = new DBLogicConnection();

            string str1 = "select full_name as \"ФИО пациента\" from patient";
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);

                dataGridView2.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            FormOperationalDataHistoryAdd add = new FormOperationalDataHistoryAdd();
            add.ShowDialog();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            var currentName_ = dataGridView2.CurrentCell.Value;
            currentName = currentName_.ToString();
        }
    }
}
