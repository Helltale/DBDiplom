using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AndreevNIR.Report;

namespace AndreevNIR
{
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            var fgp = new FormGetPatient("2");
            fgp.ShowDialog();
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            var fgp = new FormGetPatient("4");
            fgp.ShowDialog();
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            var fgp = new FormGetPatient("3");
            fgp.ShowDialog();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            var fgp = new FormGetPatient("1");
            fgp.ShowDialog();
        }
    }
}
