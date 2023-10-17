using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace AndreevNIR
{
    class Placeholders
    {
        public void PlaceholderShow(TextBox txtb, string text) {
            if (txtb.Text == "") {
                txtb.ForeColor = Color.Gray;
                txtb.Text = text;
            }
        }

        public void PlaceholderHide(TextBox txtb, string text) {
            if (txtb.Text == text) {
                txtb.ForeColor = Color.Black;
                txtb.Text = null;
            }
        }
    }
}
