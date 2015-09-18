using System.Windows.Forms;

namespace EmployerInfo
{
    public partial class WebFormPopup : Form
    {

        public WebBrowser WebD = new WebBrowser();

        public WebFormPopup()
        {
            InitializeComponent();
            WebD.Dock = DockStyle.Fill;
            this.Controls.Add(WebD);
        }
    }
}
