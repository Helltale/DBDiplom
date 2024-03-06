using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.ReferenceData.Procedures;

namespace AndreevNIR
{
    public partial class FormAddTypeProc : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        string id_procedure = null;
        string selectedMouseDrugID;

        public FormAddTypeProc()
        {
            InitializeComponent();
            LoadDGV(dataGridView1);
            cl.FillComboBox(comboBox1, "Значение", "Грамм", "Миллиграмм", "Микрогамм", "Литр", "Миллилитр", "%");
        }

        public FormAddTypeProc(string _id_procedure)
        {
            ClassProcedures cp = new ClassProcedures();

            InitializeComponent();
            LoadDGV(dataGridView1);
            cl.FillComboBox(comboBox1, "Значение", "Грамм", "Миллиграмм", "Микрогамм", "Литр", "Миллилитр", "%");
            id_procedure = _id_procedure;

            cp.GetProcedure(_id_procedure, dataGridView1, textBox3, textBox2, comboBox1);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAddDrug fad = new FormAddDrug();
            fad.ShowDialog();
            cl.ShowDGV("select * from drug", dataGridView1, db._connectionString);
        }

        private void LoadDGV(DataGridView dgv) {
            cl.ShowDGV("select * from drug", dgv, db._connectionString);
            dgv.Columns[0].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try { selectedMouseDrugID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); } catch { }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы хотите изменить запись?",
                                                        "Изменение записи",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Information,
                                                        MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {

                DataGridViewRow row = dataGridView1.CurrentRow;
                DataGridViewCell cell = row.Cells[0];

                FormAddDrug fad = new FormAddDrug(cell.Value.ToString());
                fad.ShowDialog();

                cl.ShowDGV("select * from drug", dataGridView1, db._connectionString);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ClassProcedures cp = new ClassProcedures();
            if (id_procedure != null)
            {
                cp.SetProcedure(id_procedure, selectedMouseDrugID, textBox3, textBox2, comboBox1);
            }
            else {
                cp.CreateProcedures(selectedMouseDrugID, textBox3, textBox2, comboBox1);
            }
            this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.CurrentCell != null && e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Вы хотите удалить запись?",
                                                        "Удаление записи",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Information,
                                                        MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    
                    DataGridViewRow row = dataGridView1.CurrentRow;
                    DataGridViewCell cell = row.Cells[0];

                    ClassDrug cd = new ClassDrug();
                    cd.DeleteDrug(cell.Value.ToString());

                    cl.ShowDGV("select * from drug", dataGridView1, db._connectionString);
                }
            }
        }


        
    }
}
