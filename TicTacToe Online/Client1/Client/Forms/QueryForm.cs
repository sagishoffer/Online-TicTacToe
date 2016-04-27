// Oren Yulzary - 200887008 & Sagi Shoffer - 300989241

using Client.TicTacToeService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Client
{
    public partial class QueryForm : Form
    {
        private MainForm mainForm;
        private PlayerObject[] players;
        private GameObject[] games;
        private ChampsObject[] champs;
        private object[] obj;
        private QueryResultUserControl queryDataGrid;

        // Query form constructor. Method gets mainForm instance.
        public QueryForm(MainForm gameForm)
        {
            InitializeComponent();
            this.mainForm = gameForm;
        }

        // Method sets the window alignment to the parent center and initial the table data
        private void QueryForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);

            GameClient clientService = mainForm.ClientService;
            clientService.getAllPlayersDetails(mainForm.UserName, delayCB.Checked);
            queryCB.SelectedIndex = 0;
        }

        // Method inits the table data
        public void setData(object[] obj)
        {
            this.obj = obj;
            string[] headers = null;
            string[] types = null;
            bool[] readOnly = null;
            bool[] allowNull = null;

            if (obj is AdviserObject[])
            {
                headers = new string[] { "UserName", "FirstName", "LastName", "Rank", "CreatedAt", "AdviseTo" };
                types = new string[] { "charint", "char", "char", "char", "date", "charint" };
                readOnly = new bool[] { true, false, false, false, true, false };
                allowNull = new bool[] { false, false, true, false, true, true };
            }
            else if (obj is PlayersGamesObject[])
            {
                headers = new string[] { "UserName", "NumOfGames" };
                types = new string[] { "charint", "int" };
                readOnly = new bool[] { true, true };
                allowNull = new bool[] { false, false };
            }
            else if (obj is PlayerObject[])
            {
                headers = new string[] { "UserName", "FirstName", "LastName", "Rank", "CreatedAt" };
                types = new string[] { "charint", "char", "char", "comboBox", "datetime" };
                readOnly = new bool[] { true, false, false, false, true };
                allowNull = new bool[] { false, false, true, false, true };
            }
            else if (obj is GameObject[])
            {
                headers = new string[] { "BoardName", "BoardSize", "Player1", "Player2", "Date" };
                types = new string[] { "string", "int", "char", "charint", "charint", "datetime" };
                readOnly = new bool[] { true, true, true, true, true, true };
                allowNull = new bool[] { false, false, false, false, false, false };
            }
            else if (obj is ChampsObject[])
            {
                headers = new string[] { "Name", "City", "Date", "Image" };
                types = new string[] { "words", "words", "date", "image" };
                readOnly = new bool[] { false, false, false, false };
                allowNull = new bool[] { false, true, false, true };
            }
            else if (obj is CityObject[])
            {
                headers = new string[] { "Name", "NumOfChamps" };
                types = new string[] { "char", "int"};
                readOnly = new bool[] { true, true };
                allowNull = new bool[] { false, false };
            }

            this.queryDataGrid = new QueryResultUserControl(obj, headers, types, readOnly, allowNull);
            elementHost.Child = queryDataGrid;
        }

        // Method sets data into the filter comboBox
        public void setValuesCB(object[] obj)
        {
            if (obj is PlayerObject[])
            {
                players = (PlayerObject[])obj;
                var x =
                    from player in players
                    select player.UserName + " - " + player.Id;
                filterCB.DataSource = x.ToArray();
            }

            else if (obj is GameObject[])
            {
                games = (GameObject[])obj;
                var x =
                    from game in games
                    select game.BoardName + " - " + game.Date;
                filterCB.DataSource = x.ToArray();
            }

            else if (obj is ChampsObject[])
            {
                champs = (ChampsObject[])obj;
                var x =
                    from champ in champs
                    select champ.Name + ", " + champ.City + " - " + champ.Date;
                filterCB.DataSource = x.ToArray();
            }
        }

        // Method displays the selected query by the selected comboBox index, and gets the filter data (if needed).
        private void queryCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;
            int queryIndex = queryCB.SelectedIndex + 1;
            try
            {
                switch (queryIndex)
                {
                    case 1:
                        queryDescLB.Text = "All players with their details";
                        clientService.getAllPlayersDetails(mainForm.UserName, delayCB.Checked);
                        showButtons(true, false);
                        break;
                    case 2:
                        queryDescLB.Text = "All games with their details";
                        clientService.getAllGamesDetails(mainForm.UserName, delayCB.Checked);
                        showButtons(false, false);
                        break;
                    case 3:
                        queryDescLB.Text = "All champions with their details";
                        clientService.getAllChampsDetails(mainForm.UserName, delayCB.Checked);
                        showButtons(true, false);
                        break;
                    case 4:
                        queryDescLB.Text = "List of games played by the chosen player";
                        clientService.getAllPlayersDetailsToCB(mainForm.UserName);
                        showButtons(false);
                        break;
                    case 5:
                        queryDescLB.Text = "List of participated championships by the chosen player ";
                        clientService.getAllPlayersDetailsToCB(mainForm.UserName);
                        showButtons(true);
                        break;
                    case 6:
                        queryDescLB.Text = "List of players who played in the chosen game";
                        clientService.getAllGamesDetailsToCB(mainForm.UserName);
                        showButtons(true);
                        break;
                    case 7:
                        queryDescLB.Text = "List of advisers who advised to players in the chosen game";
                        clientService.getAllGamesDetailsToCB(mainForm.UserName);
                        showButtons(true);
                        break;
                    case 8:
                        queryDescLB.Text = "List of players who played in the chosen championship";
                        clientService.getAllChampsDetailsToCB(mainForm.UserName);
                        showButtons(true);
                        break;
                    case 9:
                        queryDescLB.Text = "The number of games played by every player";
                        clientService.getPlayersNumOfGames(mainForm.UserName, delayCB.Checked);
                        showButtons(false, false);
                        break;
                    case 10:
                        queryDescLB.Text = "The number of championships hosted by every city";
                        clientService.getCityNumOfChamps(mainForm.UserName, delayCB.Checked);
                        showButtons(false, false);
                        break;
                    default:
                        break;
                }
            }
            catch (TimeoutException ex)
            {
                //Server Off
                mainForm.logIn(null);
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException ex)
            {
                mainForm.logIn(null);
            }
        }

        // Method displays the information filtered on the table
        private void filterCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;
            int queryIndex = queryCB.SelectedIndex + 1;

            switch (queryIndex)
            {
                case 4:
                    if (players != null)
                        clientService.getGamesByPlayerDetails(mainForm.UserName,
                            players[filterCB.SelectedIndex].UserName, delayCB.Checked);
                    break;
                case 5:
                    if (players != null)
                        clientService.getChampsByPlayerDetails(mainForm.UserName,
                            players[filterCB.SelectedIndex].UserName, delayCB.Checked);
                    break;
                case 6:
                    if (games != null)
                        clientService.getPlayersByGameDetails(mainForm.UserName,
                            games[filterCB.SelectedIndex], delayCB.Checked);
                    break;
                case 7:
                    if (games != null)
                        clientService.getAdvisersByGameDetails(mainForm.UserName,
                            games[filterCB.SelectedIndex], delayCB.Checked);
                    break;
                case 8:
                    if (champs != null)
                        clientService.getPlayersByChampDetails(mainForm.UserName,
                            champs[filterCB.SelectedIndex], delayCB.Checked);
                    break;
                default:
                    break;
            }
        }

        /** Commits update action to all selected records */
        private void saveBT_Click(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;
            List<Object> lst = queryDataGrid.getRowsChanged<Object>();

            if (lst.Any()) // check if any row selected
            {
                if (obj is AdviserObject[])
                {
                    IEnumerable<AdviserObject> adlst = lst.Cast<AdviserObject>();
                    clientService.updateAdviserDB(adlst.ToArray(), mainForm.UserName);
                }
                else if (obj is PlayerObject[])
                {
                    IEnumerable<PlayerObject> plst = lst.Cast<PlayerObject>();
                    clientService.updatePlayersDB(plst.ToArray(), mainForm.UserName);       
                }
                else if (obj is ChampsObject[])
                {
                    IEnumerable<ChampsObject> clst = lst.Cast<ChampsObject>();
                    clientService.updateChampsDB(clst.ToArray(), mainForm.UserName);
                }
            }
            else
                MessageBox.Show("No row selected!", "Abort Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /** Commits delete action respectively to selected option */
        private void delBT_Click(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;

            if (oneRowRB.Checked)           // Delete one row 
                delOneRow(clientService);
            else                            // Delete multi rows
                delMultiRows(clientService);
        }

        // Method delete one row from the table
        private void delOneRow(GameClient clientService)
        {
            try // check if any row selected
            {
                if (obj is AdviserObject[])
                {
                    AdviserObject adviser = ((AdviserObject)obj[queryDataGrid.getSelectedRow()]);
                    clientService.deleteOneRowFromAdvisers(adviser, mainForm.UserName);
                }
                else if (obj is PlayerObject[])
                {
                    PlayerObject player = ((PlayerObject)obj[queryDataGrid.getSelectedRow()]);
                    if (player.UserName.Equals("Computer"))
                        MessageBox.Show("You can't delete computer!", "Abort Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        clientService.deleteOneRowFromPlayers(player, mainForm.UserName);
                }
                else if (obj is ChampsObject[])
                {
                    ChampsObject champ = ((ChampsObject)obj[queryDataGrid.getSelectedRow()]);
                    clientService.deleteOneRowFromChamps(champ, mainForm.UserName);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("No row selected!", "Abort Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Method delete multi rows from the table
        private void delMultiRows(GameClient clientService)
        {
            string value, colHeader;
            int rowIndex;

            queryDataGrid.getSelectedCell(out rowIndex, out colHeader, out value);
            if (colHeader != null && value != null) // check if any value selected
            {
                if (obj is PlayerObject[]) // works for player and advisers as one
                {
                    PlayerObject player = (PlayerObject)obj[rowIndex];
                    clientService.deleteMultiRowFromPlayers(colHeader, value, mainForm.UserName);
                }
                else if (obj is ChampsObject[])
                {
                    ChampsObject champ = (ChampsObject)obj[rowIndex];
                    clientService.deleteMultiRowFromChamps(champ, colHeader, value, mainForm.UserName);
                }
            }
            else
                MessageBox.Show("No row selected!", "Abort Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Method gets edit and filter booleans, according to these booleans the method enable/disable the filter comboBox and the control buttons.
        private void showButtons(bool edit, bool filter = true)
        {
            filterCB.Enabled = filter;

            delBT.Enabled = edit;
            delOpGroup.Enabled = edit;
            saveBT.Enabled = edit;
        }

        // Method change the selection mode of the table to full row selection
        private void oneRowRB_CheckedChanged(object sender, EventArgs e)
        {
            queryDataGrid.setSelectionUnitCell(false);
        }

        // Method change the selection mode of the table to cell selection
        private void multiRowRB_CheckedChanged(object sender, EventArgs e)
        {
            queryDataGrid.setSelectionUnitCell(true);
        }

        // Method refresh the table data
        public void refreshTable()
        {
            int queryIndexTmp = queryCB.SelectedIndex;
            queryCB.SelectedIndex = -1;
            queryCB.SelectedIndex = queryIndexTmp;
        }

        // Method hides the form
        private void QueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
}
