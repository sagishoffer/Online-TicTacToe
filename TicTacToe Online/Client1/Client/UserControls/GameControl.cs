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
    public partial class GameControl : UserControl
    {
        public BoardControl boardControl { get; private set; }
        private MainForm mainForm;

        // Game control constructor. Method gets mainForm instance.
        public GameControl(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            boardControl = new BoardControl(mainForm);
            boardElementHost.Child = boardControl;
        }

        // Method init the game board
        public void initBoard(string boardName, int boardSize, char playMode)
        {
            GameClient clientService = mainForm.ClientService;
            
            mainForm.BoardName = boardName;
            boardControl.boardSize = boardSize;
            boardControl.playMode = playMode;
            boardControl.InitBoard();

            clientService.RegisterBoard(boardName, boardControl.playMode, mainForm.UserName, boardControl.boardSize);
        }

        // Method rematch the game
        private void rematchBT_Click(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;

            setTitle("");
            showOrHideFinishBT(false);
            boardControl.InitBoard();
            clientService.PlayAgain(mainForm.BoardName, boardControl.playMode, mainForm.UserName, boardControl.myToken, boardControl.boardSize);
        }

        // Method leaves the board game and return to the menu
        private void leaveBT_Click(object sender, EventArgs e)
        {
            GameClient clientService = mainForm.ClientService;

            setTitle("");
            showOrHideFinishBT(false);
            if (boardControl.playMode == 's')
                clientService.LeaveGame(mainForm.UserName, mainForm.UserName, false);
            else
                clientService.LeaveGame(mainForm.BoardName, mainForm.UserName, false);
            mainForm.BoardName = "";
        }

        // Method display / hide the leave & rematch buttons
        public void showOrHideFinishBT(bool show)
        {
            this.rematchBT.Visible = show;
            this.leaveBT.Visible = show;
        }

        // Method show the winner move path
        public void showWinMove(char winnerToken, int[] winIndexs)
        {
            boardControl.drawWinMove(winnerToken, winIndexs);
        }

        // Method sets game winner/draw title
        public void setTitle(string title) {
            infoLB.Text = title;
        }

        // Method sets defualt values on leaves the game
        public void outGame() {
            this.Visible = false;
            showOrHideFinishBT(false);
            setTitle("");
        }
    }
}
