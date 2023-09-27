using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AndreevNIR
{
    public partial class FormAutorization : Form
    {
        Thread th; 

        public FormAutorization()
        {
            InitializeComponent();

            textBoxLogin.Text = "admin";
            textBoxPassword.Text = "123";
        }

        public void open(object obj) { 
            Application.Run(new FormIndex2());
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == "admin" && textBoxPassword.Text == "123")
            {
                this.Close();
                th = new Thread(open);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else {
                MessageBox.Show("Некорректные данные для входа");
            }
        }
    }
}
