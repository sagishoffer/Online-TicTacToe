// Oren Yulzary - 200887008 & Sagi Shoffer - 300989241

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LogInForm : Form
    {
        private MainForm mainForm;
        private ErrorProvider ep = new ErrorProvider();

        // Log in form constructor. Method gets mainForm instance.
        public LogInForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        // Method sets the window alignment to the parent center
        private void LogInForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
        }

        // Method takes care of login client on the server
        private void logInBT_Click(object sender, EventArgs e)
        {
            try
            {
                if (userNameTB.Text.Length == 0)
                    setError("User Name can not be empty");
                else
                    mainForm.ClientService.LogIn(userNameTB.Text, null);
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException ex)
            {
                // Server Not response
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                // No Server
            }
        }

        // Method validates user name input
        private void userNameTB_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex("^[a-zA-Z]+[a-zA-Z0-9]*$");
            var text = userNameTB.Text;

            if (text.Length == 0)
            {
                setError("User Name can not be empty");
                e.Cancel = true;
            }
            else if (!reg.IsMatch(text))
            {
                setError("User Name must start with char");
                e.Cancel = true;
            }
            else
            {
                ep.Clear();
                userNameTB.Text = text;
            }
        }

        // Method set user name error icon
        public void setError(string message)
        {
            ep.SetError(userNameTB, message);
        }
    }
}
