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
using AndreevNIR.authorization;

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormAutorizationSettings setting = new FormAutorizationSettings();
            setting.ShowDialog();
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            AutorizationClass ac = new AutorizationClass();
            CheckFields cf = new CheckFields();
            var listFill1 = cf.CheckAllFields(textBoxNewLogin, textBoxNewPassword, textBox1);
            var errorMessage1 = cf.GenerateErrorMessageEmptyTextBox(listFill1, "Логин", "Пароль", "Повторный пароль");
            if (errorMessage1 == "Следующие поля не были заполнены: ")
            {
                var flag1 = cf.CheckedCombobox(comboBoxNewJobTitle);
                var listFill2 = new List<bool>();
                listFill2.AddRange(new bool[] { flag1 });
                var errorMessage2 = cf.GenerateErrorMessageEmptyComboBox(listFill2, "Должность сотрудника");
                if (errorMessage2 == "Значения в следующих выпадающих меню не были выбраны: ")
                {
                    var flag2 = ac.CheckPasswordGenerateMessage(textBoxNewPassword, textBox1);
                    if (flag2 == false) {
                        //запрос на регистрацию
                        ac.CreateNewUser(textBoxNewLogin, textBoxNewPassword, comboBoxNewJobTitle);
                        MessageBox.Show("Заявка на создание аккаунта создана!\nСвяжитесь с администратором.");
                    }
                }
                else { MessageBox.Show(errorMessage2); }
            }
            else { MessageBox.Show(errorMessage1); }
        }
    }
}
