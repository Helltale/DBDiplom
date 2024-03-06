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
    public partial class FormAutorizationSettings : Form
    {
        public FormAutorizationSettings()
        {
            InitializeComponent();
            DBLogicConnection dB = new DBLogicConnection();
            richTextBox1.Text = dB._connectionString;
            
        }
        DBLogicConnection dB = new DBLogicConnection();


        private void button2_Click(object sender, EventArgs e) //тоже самое.
        {
            dB.SetNewConnectionStringDefault();
            richTextBox1.Text = dB._connectionString;
            
        }

        private void button1_Click(object sender, EventArgs e) //проблема с перезаписью пути, потом доделать.
        {
            string tmp = dB.SetNewConnectionString(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            dB._connectionString = tmp;

            richTextBox1.Text = dB._connectionString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
