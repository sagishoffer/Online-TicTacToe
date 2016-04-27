using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.TicTacToeService;

namespace Client
{
    public partial class RoomsControl : UserControl
    {
        private MainForm mainForm;

        // Rooms control constructor. Method gets mainForm instance.
        public RoomsControl(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            RoomsPanel.Panel2.Hide();
        }

        // Method inits the form to default values
        public void showRooms(bool show)
        {
            this.Visible = show;
            computerRB.Checked = true;
        }

        // Method gets the board list from the server by the mode
        private void getBoardsListBox(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;

            char mode;
            if (playerRB.Checked)
                mode = 'm';
            else
                mode = 's';

            clientService.getBoardsByMode(mainForm.UserName, mode);
        }

        // Method sets the boards list
        public void setBoardsListBox(BoardsObject[] boards)
        {
            var x =
                from b in boards
                select b.Name;

            this.boardsListBox.DataSource = x.ToArray();
        }

        // Method display the choosen board details
        public void setBoardDetails(BoardsObject board, int numberOfPlayers)
        {
            boardNameLB.Text = board.Name;
            boardSizeLB.Text = board.Size + "X" + board.Size;
            numberOfPlayersLB.Text = numberOfPlayers + "/" + 2;
            descLB.Text = board.Description;

            if (numberOfPlayers == 2 && board.Mode == 'm')
                playBT.Enabled = false;
            else
                playBT.Enabled = true;
        }

        // Method gets the board details from the server
        private void boardsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;

            RoomsPanel.Panel2.Show();
            string boardName = boardsListBox.SelectedItem.ToString();
            clientService.getBoardDetails(mainForm.UserName, boardName);
        }

        // Method enter to the selected board
        private void playBT_Click(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;

            string boardName = boardNameLB.Text;
            int numOfPlayers = clientService.getPlayersPerBoard(boardName);
            numberOfPlayersLB.Text = numOfPlayers + "/" + 2;

            if (numOfPlayers < 2)
            {
                playBT.Enabled = true;
                char playMode = computerRB.Checked ? 's' : 'm';
                boardName = computerRB.Checked ? mainForm.UserName : boardName;
                mainForm.EnterOrLeaveGame(true, boardName, int.Parse(boardSizeLB.Text.Split('X')[0]), playMode);
            }
            else
                playBT.Enabled = false;
        }


    }
}
