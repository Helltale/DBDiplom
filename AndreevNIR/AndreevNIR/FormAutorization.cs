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

        string connStr = "Host=localhost;Username=postgres;Password=64210369;Database=postgres";


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
            DBLogicConnection connetcionClass = new DBLogicConnection();
            connetcionClass.ConnectToPostgres();



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

        private void FormAutorization_Load(object sender, EventArgs e)
        {

        }
    }
}
