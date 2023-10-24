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
            TextboxLoad(dB.conHost, dB.conUsername, dB.conPassword, dB.conDB);
        }
        DBLogicConnection dB = new DBLogicConnection();

        private void TextboxLoad(string str1, string str2, string str3, string str4) {
            textBox1.Text = str1;
            textBox2.Text = str2;
            textBox3.Text = str3;
            textBox4.Text = str4;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dB.SetNewConnectionStringDefault();
            richTextBox1.Text = dB._connectionString;
            TextboxLoad(dB.conHost, dB.conUsername, dB.conPassword, dB.conDB);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dB.SetNewConnectionString(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            richTextBox1.Text = dB._connectionString;
            TextboxLoad(dB.conHost, dB.conUsername, dB.conPassword, dB.conDB);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
