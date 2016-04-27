// Sagi Shoffer & Oren Yulzary

using Client.TicTacToeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    class MyCallBack : IGameCallback
    {
        private MainForm mainForm;
        private registerForm regForm;

        // MyCallBack constructor
        public MyCallBack(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        //////////////////////// Main Form callbacks ////////////////////////

        #region Main Form callbacks

        // Method display the rooms menu
        public void displayRooms()
        {
            mainForm.GameControl.outGame();
            mainForm.EnterOrLeaveGame(false);
        }

        // Method sets status on the status bar in the main form
        public void WriteStatus(string str)
        {
            mainForm.setStatusBar(str);
        }

        #endregion

        //////////////////////// LogIn Form callbacks ////////////////////////

        #region LogIn Form callbacks

        // Method validates the user name in the login form. if the validation pass, the client log in and the login form close 
        public void validateLogInUserName(bool exist, string userName, string message)
        {
            LogInForm loginForm = mainForm.LoginForm;

            if (exist)
            {
                mainForm.logIn(userName);
                loginForm.Close();
            }
            else
            {
                loginForm.setError(message);
            }
        }

        #endregion

        //////////////////////// Register Form callbacks ////////////////////////

        #region Register Form callbacks

        // Method recieves the register form data and show this form
        public void sendRegisterInfo(PlayerObject[] advisers, ChampsObject[] champs)
        {
            regForm = new registerForm(mainForm);
            regForm.setAdvisersList(advisers);
            regForm.setChampsList(champs);
            regForm.Show();
        }

        // Method validates the user name in the register form
        public void validateRegisterUserName(string message)
        {
            regForm.setUserNameValidate(message);
        }

        // Method recieves refresh advisers list, sets them in the register form and showing the validation error
        public void refreshAdvisersList(PlayerObject[] advisers)
        {
            regForm.setAdvisersList(advisers);
            regForm.setAdvisersValidate("One of the advisers is not avilable, list has refreshed");
        }

        // Method recieves refresh champs list and sets them in the register form
        public void refreshChampsList(ChampsObject[] champs)
        {
            regForm.setChampsList(champs);
        }

        // Method login the client and close the register form
        public void ClientLogInFromRergister(string userName)
        {
            mainForm.logIn(userName);
            regForm.Close();
        }

        #endregion

        //////////////////////// Board Control callbacks ////////////////////////

        #region Board Control callbacks

        // Method switch the turn of the player
        public void SwitchTurn()
        {
            BoardControl boardControl = mainForm.GameControl.boardControl;
            boardControl.SwitchTurn();
        }

        // Method commit the step and fill the correct cell at the specific place
        public void UpdateBoard(int i, int j)
        {
            BoardControl boardControl = mainForm.GameControl.boardControl;
            boardControl.setStepOnBoard(i, j);
        }

        // Method set token to player
        public void setToken(char myToken)
        {
            BoardControl boardControl = mainForm.GameControl.boardControl;
            boardControl.myToken = myToken;
        }

        // Method determines if the player start first
        public void setStartTurn(bool start)
        {
            BoardControl boardControl = mainForm.GameControl.boardControl;
            boardControl.myTurn = start;
        }

        #endregion

        //////////////////////// Game Control callbacks ////////////////////////

        #region Game Control callbacks

        // Method display / hide the leave & rematch buttons in the end of the game
        public void FinishGame(char winnerToken ,int[] winIndexs)
        {
            if (winIndexs != null)
            {
                mainForm.GameControl.showWinMove(winnerToken, winIndexs);
                mainForm.GameControl.setTitle("Player " + winnerToken + " Won!");
            }
            else
                mainForm.GameControl.setTitle("Draw!");
            mainForm.GameControl.showOrHideFinishBT(true);
        }

        #endregion

        //////////////////////// Rooms Control callbacks ////////////////////////

        #region Rooms Control callbacks

        // Method sets the boards list on the Rooms Control
        public void sendListOfBoards(BoardsObject[] boards)
        {
            mainForm.RoomsControl.setBoardsListBox(boards);
        }

        // Method display the board details on the Rooms Control
        public void sendBoardDetails(BoardsObject[] boards, int numberOfPlayers)
        {
           mainForm.RoomsControl.setBoardDetails(boards[0], numberOfPlayers);
        }

        #endregion

        //////////////////////// History Form callbacks ////////////////////////

        #region History Form callbacks

        // Method set the games history on the comboBox and show the history form
        public void showGameHistory(GameObject[] arr)
        {
            HistoryForm historyForm = mainForm.HistoryForm;
            historyForm.setGameHistoryCB(arr);
            historyForm.ShowDialog();
        }

        #endregion

        //////////////////////// Thread Hold Queries Methods ////////////////////////

        #region Thread Hold Queries Methods

        // Method sets the player query data into the table
        public void showPlayersQuery(PlayerObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setData(arr);
        }

        // Method sets the adviser query data into the table
        public void showAdvisersQuery(AdviserObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setData(arr);
        }

        // Method sets the game query data into the table
        public void showGamesQuery(GameObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setData(arr);
        }

        // Method sets the champion query data into the table
        public void showChampsQuery(ChampsObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setData(arr);
        }

        // Method sets the players data into the comboBox filter
        public void showAllPlayersInQueryToCB(PlayerObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setValuesCB(arr);
        }

        // Method sets the games data into the comboBox filter
        public void showAllGamesInQueryToCB(GameObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setValuesCB(arr);
        }

        // Method sets the champions data into the comboBox filter
        public void showAllChampsInQueryToCB(ChampsObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setValuesCB(arr);
        }

        // Method sets the players and their num of games query data into the table
        public void showPlayersNumOfGamesQuery(PlayersGamesObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setData(arr);
        }

        // Method sets the city and their num of champions query data into the table
        public void showCityNumOfChampsQuery(CityObject[] arr)
        {
            QueryForm queryForm = mainForm.QueryForm;
            queryForm.setData(arr);
        }

        #endregion

        //////////////////////// Refresh DataGrid Callbacks ////////////////////////

        #region Refresh DataGrid Callback

        // Method display suitable messegeBox when edit/delete data from table and refresh the table
        public void messageAndRefreshTable(string message)
        {
            QueryForm queryForm = mainForm.QueryForm;

            if (message != null)
                MessageBox.Show(message, "Missing Data", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("All changes were made", "Data changed", MessageBoxButton.OK, MessageBoxImage.Information);

            queryForm.refreshTable();
        }

        #endregion
    }
}
