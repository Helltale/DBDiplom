using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR.Report
{
    public partial class GetMeetingForm : Form
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();
        public string SelectedValue { get; private set; }

        public GetMeetingForm(string id_patient)
        {
            InitializeComponent();
            textBox1.Enabled = false;
            cl.ShowDGV($"select t1.id_meeting, t2.full_name, t4.full_name, t1.discription_meeting, t1.date_meeting, t1.time_meeting, t1.operation_control from meetings t1 join staff t2 on t1.id_staff = t2.id_staff join patient_in_room t3 on t1.id_patient = t3.id_patient join patient t4 on t3.omc = t4.omc where t1.id_staff = '{id_patient}'",dataGridView1, db._connectionString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
