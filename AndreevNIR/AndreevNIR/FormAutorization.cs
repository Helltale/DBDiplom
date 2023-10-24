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

            textBoxLogin.Text = "john.smith";
            textBoxPassword.Text = "123";
        }

        public void open(object obj) { 
            Application.Run(new FormIndex2());
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            

            DBLogicConnection connetcionClass = new DBLogicConnection();
            connetcionClass.OpenSQLConnection(connetcionClass._connectionString); //открывается подключение

            connetcionClass.CreateQueryLogIn(textBoxLogin.Text, textBoxPassword.Text); //поиск пользователя
            bool flag = connetcionClass.tmpFlag;

            if (flag)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormAutorizationSettings setting = new FormAutorizationSettings();
            setting.ShowDialog();
        }
    }
}
