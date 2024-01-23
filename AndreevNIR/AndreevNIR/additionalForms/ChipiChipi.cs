using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace AndreevNIR.additionalForms
{
    public partial class ChipiChipi : Form
    {
        //...watch_popup... + enter
        const string URL = "https://www.youtube.com/embed/0tOXxuLcaog?autoplay=1&loop=1";

        public ChipiChipi()
        {
            InitializeComponent();
        }

        private async Task initialized() {
            await webView21.EnsureCoreWebView2Async(null);
        }

        private async void GetStartedBrowser() {
            await initialized();
            webView21.CoreWebView2.Navigate(URL);
            //webView21.ExecuteScriptAsync($"window.open('{URL}', '_self');");
        }

        private void ChipiChipi_Load(object sender, EventArgs e)
        {
            GetStartedBrowser();
        }
    }
}
