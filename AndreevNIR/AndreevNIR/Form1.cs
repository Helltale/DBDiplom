using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AndreevNIR
{
    public partial class Form1 : Form
    {
        DataBaseCore dbc = new DataBaseCore();

        public Form1()
        {
            InitializeComponent();
            dbc.openConnection();
            dbc.showDGV(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || richTextBox1.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
            }
            else {
                dbc.newRecord(textBox1, textBox2, textBox3, richTextBox1);
                dbc.showDGV(dataGridView1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dbc.getListsDelete(dataGridView1);
            dbc.showDGV(dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dbc.getListEdit(dataGridView1, textBox1, textBox2, textBox3, richTextBox1);
            dbc.showDGV(dataGridView1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dbc.getReport();
        }
    }
}
