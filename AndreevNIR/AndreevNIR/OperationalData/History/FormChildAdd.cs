using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData;
using AndreevNIR.ReferenceData.TypeHeal;

namespace AndreevNIR.OperationalData.History
{
    public partial class FormChildAdd : Form
    {
        string[] tmp;
        CoreLogic cl = new CoreLogic();
        public FormChildAdd(string[] patient_data)
        {
            InitializeComponent();
            cl.FillComboBox(comboBox1, "Выберите параметр", "Плановый осмотр", "Консервативное лечение", "Оперативное лечение", "Выписной лист", "Лист нетрудоспособности");
            tmp = patient_data;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: //плановый
                    {
                        FormAddTypeHealExamination fa = new FormAddTypeHealExamination();
                        fa.ShowDialog();
                        this.Close();
                    }
                    break;
                case 1: //консервативное
                    {
                        FormAddTypeHealConservative fa = new FormAddTypeHealConservative();
                        fa.ShowDialog();
                        this.Close();
                    }
                    break;
                case 2: //оперативное
                    {
                        FormAddTypeHealOperations fa = new FormAddTypeHealOperations();
                        fa.ShowDialog();
                        this.Close();
                    }
                    break;
                case 3: //выписной лист
                    {
                        FormAddTypeDockExtract fa = new FormAddTypeDockExtract();
                        fa.ShowDialog();
                        this.Close();
                    }
                    break;
                case 4: //лист нетрудоспособности
                    {
                        FormAddTypeDockNotWorking fa = new FormAddTypeDockNotWorking();
                        fa.ShowDialog();
                        this.Close();
                    }
                    break;
            }
        }
    }
}
