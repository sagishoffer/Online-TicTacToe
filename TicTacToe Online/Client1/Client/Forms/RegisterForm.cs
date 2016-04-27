// Oren Yulzary - 200887008 & Sagi Shoffer - 300989241

using Client.TicTacToeService;
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
    public partial class registerForm : Form
    {
        private MainForm mainForm;
        private ErrorProvider ep = new ErrorProvider();
        private PlayerObject[] advisers;
        private ChampsObject[] champs;

        // Register form constructor. Method gets mainForm instance.
        public registerForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }


        // Method sets the window alignment to the parent center
        private void registerForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);

            rankCB.SelectedIndex = 1;
        }

        // Method takes care of register client on the server
        private void regBT_Click(object sender, EventArgs e)
        {
            if (userNameTB.Text.Length == 0)
                ep.SetError(userNameTB, "User Name can not be empty");
            if (firstNameTB.Text.Length == 0)
                ep.SetError(firstNameTB, "First Name can not be empty");
            if (userNameTB.Text.Length != 0 && firstNameTB.Text.Length != 0)
            {
                PlayerObject player = new PlayerObject {UserName = userNameTB.Text, FirstName = firstNameTB.Text, LastName = LastNameTB.Text, CreatedAt = DateTime.Now, Rank = rankCB.SelectedItem as string }; 
                List<int> lstAdvisers = getAdvisersId();
                List<int> lstChamps = getChampsId();

                mainForm.ClientService.Register(player, lstAdvisers.ToArray(), lstChamps.ToArray());
            }
        }

        // Method set advisers error icon
        public void setAdvisersValidate(string str)
        {
            ep.SetError(adviserCheckedList, str);
        }

        // Method set user name error icon
        public void setUserNameValidate(string str)
        {
            ep.SetError(userNameTB, str);
        }

        // Method return ids list of checked advisers
        private List<int> getAdvisersId()
        {
            List<int> lst = new List<int>();
            foreach (int i in adviserCheckedList.CheckedIndices)
            {
                lst.Add(advisers[i].Id);
            }

            return lst;
        }

        // Method return ids list of checked champions
        private List<int> getChampsId()
        {
            List<int> lst = new List<int>();
            foreach (int i in champsCheckedListBox.CheckedIndices)
            {
                lst.Add(champs[i].Id);
            }

            return lst;
        }

        // Method validates user name input
        private void userNameTB_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex("^[a-zA-Z]+[a-zA-Z0-9]*$");
            var text = userNameTB.Text;

            if (!reg.IsMatch(text) || text.Length == 0)
            {
                ep.SetError(userNameTB, "UserName must start with char");
                e.Cancel = true;
            }

            else
            {
                ep.Clear();
                userNameTB.Text = text;
            }
        }

        // Method validates first name input
        private void firstNameTB_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex("^[A-Z][a-z]*$");
            var text = firstNameTB.Text;

            if (!reg.IsMatch(text) || text.Length == 0)
            {
                ep.SetError(firstNameTB, "First name must start with capital letter and contains only characters");
                e.Cancel = true;
            }

            else
            {
                ep.Clear();
                firstNameTB.Text = text;
            }
        }

        // Method validates last name input
        private void LastNameTB_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex("^[A-Z][a-z]*$");
            var text = LastNameTB.Text;

            if (!reg.IsMatch(text) && text.Length > 0)
            {
                ep.SetError(LastNameTB, "Last name must start with capital letter and contains only characters");
                e.Cancel = true;
            }

            else
            {
                ep.Clear();
                LastNameTB.Text = text;
            }
        }

        // Method gets advisers array and sets those advisers into checked list
        public void setAdvisersList(PlayerObject[] advisers)
        {
            adviserCheckedList.Items.Clear();
            this.advisers = advisers;

            var x =
                from a in advisers
                select a.FirstName + " " + a.LastName;

            adviserCheckedList.Items.AddRange(x.ToArray());          
        }

        // Method gets champions array and sets those champions into checked list box
        public void setChampsList(ChampsObject[] champs)
        {
            this.champs = champs;
            champsCheckedListBox.Items.Clear();

            var x =
                from c in champs
                select (new champData { champ = c }).ToString();

            champsCheckedListBox.Items.AddRange(x.ToArray());
        }

        // Method open edit dialog of the selected champion
        private void editChampBT_Click(object sender, EventArgs e)
        {
            int index = champsCheckedListBox.SelectedIndex;
            if (index != -1)
            {
                ChampForm cf = new ChampForm(champs[index], mainForm.ClientService, this);
                cf.ShowDialog();
            }
        }

        // Method open dialog for create new champion
        private void addChampBT_Click(object sender, EventArgs e)
        {
            ChampForm cf = new ChampForm(mainForm.ClientService, this);
            cf.ShowDialog();
        }

        // Class is responsible for formatting data of the championships
        private class champData
        {
            public ChampsObject champ { get; set; }
            public override string ToString()
            {
                return string.Format("{0} {1} - {2}", champ.Name + ",", champ.City, champ.Date.ToString("dd/MM/yyyy"));
            }
        }
    }
}
