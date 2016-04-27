// Oren Yulzary - 200887008 & Sagi Shoffer - 300989241

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.TicTacToeService;
using System.ServiceModel.Description;
using System.Net.NetworkInformation;
namespace Client
{
    public partial class MainForm : Form
    {   
        #region Properties
        public GameClient ClientService { get; private set; }
        public RoomsControl RoomsControl { get; private set; }
        public GameControl GameControl { get; private set; }
        public LogInForm LoginForm { get; private set; }
        public HistoryForm HistoryForm { get; private set; }
        public QueryForm QueryForm { get; private set; }
        public string UserName { get; set; }
        public string BoardName { get; set; }
        
        #endregion

        // Main form constructor
        public MainForm()
        {
            InitializeComponent();

            RoomsControl = new RoomsControl(this);
            GameControl = new GameControl(this);
            panel.Controls.Add(GameControl);
            panel.Controls.Add(RoomsControl);
            GameControl.Visible = false;
            RoomsControl.Visible = false;

            // Create an instance of the service
            InstanceContext context = new InstanceContext(new MyCallBack(this));
            ClientService = new GameClient(context);
        }

        // *********** Form Methods ***********
        #region Form Methods
        private void GameForm_Load(object sender, EventArgs e)
        {
            // Load form in log out mode
            logIn(null);
        }

        // Method open the about box
        private void About_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        // Method close the connection on the server
        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ClientService.closeClient(UserName, BoardName);
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException ex)
            {

            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                // No Server
            }
        }
        #endregion

        // *********** Gui Methods ***********
        #region Gui Methods

        // The method takes care of display connected or disconnected mode
        public void logIn(string userName)
        {
            bool login;
            if (userName != null)
                login = true;
            else
                login = false;

            UserName = userName;
            logInToolStripMenuItem.Enabled = !login;
            registerToolStripMenuItem.Enabled = !login;
            logOutToolStripMenuItem.Enabled = login;
            queriesToolStripMenuItem.Visible = login;
            historyToolStripMenuItem.Visible = login;
            statusLabel.Text = "";

            if (login)
            {
                GameControl.outGame();
                RoomsControl.showRooms(true);
            }
            else
            {
                GameControl.outGame();
                RoomsControl.showRooms(false);
            }
        }

        // The method takes care of display the game or the rooms view
        public void EnterOrLeaveGame(bool enter)
        {
            GameControl.Visible = enter;
            RoomsControl.showRooms(!enter);
        }

        // The method takes care of display the game or the rooms view, if entered then the methos init the board
        public void EnterOrLeaveGame(bool enter, string boardName, int boardSize, char playMode)
        {
            EnterOrLeaveGame(enter);

            if (enter)
                GameControl.initBoard(boardName, boardSize, playMode);
        }

        // Method write a status description on the status bar
        public void setStatusBar(string str)
        {
            this.statusLabel.Text = str;
        }

        #endregion

        // *********** ToolStripMenu ***********
        #region Tool Strip Menu Functions

        // Method open the log in from
        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm = new LogInForm(this);
            LoginForm.ShowDialog();
        }

        // Method open the register from
        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClientService.getRegisterInfo();
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException ex)
            {

            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                // No Server
            }
        }

        // Method exit from the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Method closes the connection on the server and logs out from the game
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClientService.closeClient(UserName, BoardName);
                logIn(null);
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException ex)
            {
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                // No Server
            }
        }

        // Method call the server to open the history from with all the details
        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HistoryForm = new HistoryForm(this);
                ClientService.getAllGamesHistory(UserName);
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException ex)
            {
            }
            catch (System.ServiceModel.EndpointNotFoundException ex)
            {
                // No Server
            }
        }

        // Method opens the query from
        private void queriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(QueryForm == null)
                QueryForm = new QueryForm(this);

            QueryForm.ShowDialog();
        }

        #endregion
    }   
}
