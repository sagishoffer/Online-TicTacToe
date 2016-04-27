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
using System.Windows.Forms;

namespace Client
{
    public partial class HistoryForm : Form
    {
        private MainForm mainForm;
        private BoardControl boardControl;
        private GameObject[] games;
        private List<int> steps;
        private string winnerName;
        private char starter, winnerToken;
        private int stepIndex;

        // Register form constructor. Method gets mainForm instance.
        public HistoryForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.boardControl = new BoardControl();
        }

        // Method sets the window alignment to the parent center
        private void HistoryForm_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(mainForm.Location.X + (mainForm.Width - this.Width) / 2, mainForm.Location.Y + (mainForm.Height - this.Height) / 2);
            elementHost.Child = boardControl;
        }

        // Method sets games data into the comboBox
        public void setGameHistoryCB(GameObject[] games)
        {
            this.games = games;

            var x =
                from game in games
                select game.Date.ToString("dd/MM/yyyy hh:mm:ss") + " - " + game.BoardName + ", " + game.Player2 + " Vs " + game.Player1;

            gamesCB.DataSource = x.ToArray();
        }

        // Method show the game selected on the board
        private void gamesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            GameObject game = games[gamesCB.SelectedIndex];
            this.steps = game.Steps.steps.ToList();
            this.starter = game.Steps.starter;
            this.winnerName = game.Player2;
            this.winnerToken = game.Steps.winner;
            this.stepIndex = 0;
            prevBT.Enabled = false;
            nextBT.Enabled = true;
            statusLB.Text = "";

            boardControl.myToken = starter;
            boardControl.myTurn = true;
            boardControl.boardSize = game.BoardSize;
            boardControl.InitBoard();
        }

        // Method shows game's steps, forward one step 
        private void nextBT_Click(object sender, EventArgs e)
        {
            if (stepIndex >= 0 && stepIndex < steps.Count)
            {
                int row, col;
                getCoords(out row, out col);

                boardControl.setStepOnBoard(row, col);
                boardControl.SwitchTurn();

                stepIndex++;

                nextBT.Enabled = stepIndex < steps.Count;
                prevBT.Enabled = stepIndex > 0;

                if (stepIndex >= steps.Count)
                {
                    if (winnerToken != 'T')
                        statusLB.Text = "Player " + winnerToken + " (" + winnerName + ") Won!";
                    else
                        statusLB.Text = "Draw!";
                }
            }

        }

        // Method shows game's steps, previous one step
        private void prevBT_Click(object sender, EventArgs e)
        {
            if (stepIndex > 0 && stepIndex <= steps.Count)
            {
                int row, col;
                
                stepIndex--;
                statusLB.Text = "";
                getCoords(out row, out col);
                boardControl.drawEmptyCell(row, col);
                boardControl.SwitchTurn();

                nextBT.Enabled = stepIndex < steps.Count;
                prevBT.Enabled = stepIndex > 0;
            }
        }

        // Method calculates column and row indexes
        private void getCoords(out int row, out int col)
        {
            int cellIndex = steps[stepIndex];
            row = cellIndex / boardControl.boardSize;
            col = cellIndex % boardControl.boardSize;
        }
    }
}
