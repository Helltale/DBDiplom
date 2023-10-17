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
    public partial class FormAddStaff : Form
    {
        public FormAddStaff()
        {
            InitializeComponent();

            plch.PlaceholderShow(textBox1, str1); plch.PlaceholderShow(textBox2, str2); plch.PlaceholderShow(textBox3, str3); plch.PlaceholderShow(textBox4, str4);
            plch.PlaceholderShow(textBox5, str5); plch.PlaceholderShow(textBox6, str6); plch.PlaceholderShow(textBox7, str7); plch.PlaceholderShow(textBox8, str8);
            plch.PlaceholderShow(textBox9, str9); plch.PlaceholderShow(textBox10, str10); plch.PlaceholderShow(textBox11, str11); plch.PlaceholderShow(textBox12, str12);
        }

        Placeholders plch = new Placeholders();
        string str1 = "Имя сотрудника";
        string str2 = "Фамилия сотрудника";
        string str3 = "Отчество сотрудника";
        string str4 = "Серию паспотра";
        string str5 = "Номер паспорта";
        string str6 = "Выдачи паспорта";
        string str7 = "Кем выдан паспорт";
        string str8 = "Код подразделения";
        string str9 = "потерялось(";
        string str10 = "Телефон персонала";
        string str11 = "Личную почту сотрудника";
        string str12 = "Корпоративную почту сотрудника";

        private void textBox1_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox1, str1);}  //паспортные данные
        private void textBox1_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox1, str1);}
        private void textBox2_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox2, str2);}
        private void textBox2_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox2, str2);}
        private void textBox3_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox3, str3);}
        private void textBox3_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox3, str3);}
        private void textBox4_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox4, str4);}
        private void textBox4_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox4, str4);}
        private void textBox5_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox5, str5);}
        private void textBox5_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox5, str5);}
        private void textBox6_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox6, str6);}
        private void textBox6_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox6, str6);}
        private void textBox7_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox7, str7);}
        private void textBox7_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox7, str7);}
        private void textBox8_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox8, str8);}
        private void textBox8_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox8, str8);}
        private void textBox9_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox9, str9);}
        private void textBox9_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox9, str9);}

        private void textBox10_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox10, str10);} //контактная информация
        private void textBox10_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox10, str10);}
        private void textBox11_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox11, str11);}
        private void textBox11_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox11, str11);}
        private void textBox12_Enter(object sender, EventArgs e) {plch.PlaceholderHide(textBox12, str12);}
        private void textBox12_Leave(object sender, EventArgs e) {plch.PlaceholderShow(textBox12, str12);}

    }
}
