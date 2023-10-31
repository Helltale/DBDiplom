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
    public partial class FormOperationalDataList : Form
    {
        public FormOperationalDataList()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormOperationalDataListAdd add = new FormOperationalDataListAdd();
            add.ShowDialog();
        }

        public void DGV()
        {
            DBLogicConnection dBLogicConnection = new DBLogicConnection();

            string str1 = "";
            NpgsqlDataAdapter adapter1 = new NpgsqlDataAdapter(str1, dBLogicConnection._connectionString);
            try
            {
                DataTable table = new DataTable();
                adapter1.Fill(table);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex); }
        }
    }
}
