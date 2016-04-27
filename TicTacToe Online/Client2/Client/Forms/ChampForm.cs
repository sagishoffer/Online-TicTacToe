// Oren Yulzary - 200887008 & Sagi Shoffer - 300989241

using Client.TicTacToeService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ChampForm : Form
    {
        private ChampsObject champ;
        private bool update;
        private GameClient clientService;
        private string ImageName = "";
        private ErrorProvider ep = new ErrorProvider();

        // Constructor of create new champion form
        // Gets service instance and sets the window alignment to the parent center
        public ChampForm(GameClient clientService, Form parent)
        {
            InitializeComponent();
            this.clientService = clientService;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(parent.Location.X + (parent.Width - this.Width) / 2, parent.Location.Y + (parent.Height - this.Height) / 2);

            this.update = false;
        }

        // Constructor of edit exist champion form
        // Gets service instance, parent form, champ for edit. Sets the window alignment to the parent center
        public ChampForm(ChampsObject champ, GameClient clientService, Form parent)
        {
            InitializeComponent();
            this.clientService = clientService;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(parent.Location.X + (parent.Width - this.Width) / 2, parent.Location.Y + (parent.Height - this.Height) / 2);

            this.update = true;
            this.champ = champ;
            nameChampTB.Text = champ.Name;
            cityChampTB.Text = champ.City;
            dateChampDP.Value = champ.Date;
            try
            {
                if (champ.Image.Length > 0)
                    pictureBox.Load(champ.Image);
            }
            catch
            {
                pictureBox.Image = pictureBox.ErrorImage;
            }
        }
        
        // Method saves the champion
        private void saveChampBT_Click(object sender, EventArgs e)
        {
            if (nameChampTB.Text.Length != 0)
            {
                ChampsObject newChamp = new ChampsObject();

                newChamp.Id = (champ == null) ? newChamp.Id = -1 : champ.Id;
                newChamp.Name = nameChampTB.Text;
                newChamp.City = cityChampTB.Text;
                newChamp.Date = dateChampDP.Value;
                newChamp.Image = ImageName;

                clientService.updateInsertChamptionQuery(newChamp, update);

                this.Close();
            }
            else
                ep.SetError(nameChampTB, "Champion's name must start with char");
        }

        // Method open file chooser for select image
        private void openBT_Click(object sender, EventArgs e)
        {
            string[] fileType = { ".jpeg", ".png", ".jpg", ".gif" };
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            DialogResult result = openFileDialog.ShowDialog();

            // Process input if the user clicked OK.
            if (result == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                string type = Path.GetExtension(path);
                if (fileType.Contains(type))
                {
                    pictureBox.Image.Dispose();
                    pictureBox.Image = Image.FromFile(path);

                    var uri = new System.Uri(path);
                    var converted = uri.AbsoluteUri;
                    ImageName = converted;
                }
            }
        }

        // Method validates champion name input
        private void nameChampTB_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex("^[A-Z][a-z]*([ ][A-Z][a-z]*)*$");
            var text = nameChampTB.Text;

            if (!reg.IsMatch(text))
            {
                ep.SetError(nameChampTB, "Champion's name can contain only letters and should start with a capital letter");
                e.Cancel = true;
            }

            else
            {
                ep.Clear();
                nameChampTB.Text = text;
            }
        }

        // Method validates champion city input
        private void cityChampTB_Validating(object sender, CancelEventArgs e)
        {
            Regex reg = new Regex("^[A-Z][a-z]*([ ][A-Z][a-z]*)*$");
            var text = cityChampTB.Text;

            if (!reg.IsMatch(text) && text.Length > 0)
            {
                ep.SetError(cityChampTB, "City's name can contains only letters");
                e.Cancel = true;
            }

            else
            {
                ep.Clear();
                cityChampTB.Text = text;
            }
        }
    }
}
