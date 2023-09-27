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
    public partial class FormOperationalDataHistory : Form
    {
        public FormOperationalDataHistory()
        {
            InitializeComponent();
        }

        private void FormOperationalDataHistory_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            FormOperationalDataHistoryAdd add = new FormOperationalDataHistoryAdd();
            add.ShowDialog();
        }
    }
}
