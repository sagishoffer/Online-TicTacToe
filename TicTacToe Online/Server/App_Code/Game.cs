// Sagi Shoffer & Oren Yulzary

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

[ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
public class Game : IGame
{
    private TicTacToeGameDBDataContext db = new TicTacToeGameDBDataContext();
    private static char[] token = { 'X', 'O' };

    static Dictionary<string, ICallBack> clients = new Dictionary<string, ICallBack>();
    static Dictionary<string, Dictionary<string, ICallBack>> boardsComm = new Dictionary<string, Dictionary<string, ICallBack>>();
    static Dictionary<string, int> playAgainBoards = new Dictionary<string, int>();
    static Dictionary<string, char[ , ]> boardsCell = new Dictionary<string, char[ , ]>();
    static Dictionary<string, List<int>> emptyCells = new Dictionary<string, List<int>>();
    static Dictionary<string, List<int>> fillCells = new Dictionary<string, List<int>>();

    static object registerLocker = new object();
    const string COMPUTER = "Computer";

    //-------------------------------- Initiate Methods -------------------------------------//

    #region Initiate methods

    /* Initiates a new board and cells */
    public void InitBoard(string boardName, int boardSize)
    {
        if (boardsCell.ContainsKey(boardName))  // check if this board has already been in use
            boardsCell.Remove(boardName);

        char[,] cellMat = new char[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
            for (int j = 0; j < boardSize; j++)
                cellMat[i,j] = ' ';

        boardsCell.Add(boardName, cellMat);     // add the board to boards dictionary
    }

    /* Initiate a list that represents the empty cells of board*/
    public void InitEmptyCells(string boardName, int boardSize)
    {
        if (emptyCells.ContainsKey(boardName))  // check if this list of board has already been in use
            emptyCells.Remove(boardName);
        
        List<int> lst = new List<int>();
        for (int i = 0; i < boardSize * boardSize; i++)
            lst.Add(i);

        emptyCells.Add(boardName, lst);         // add the list to empty cells dictionary
    }

    /* Initiate a list that represents the filled cells of board*/
    public void InitFillCells(string boardName, int boardSize)
    {
        if (fillCells.ContainsKey(boardName))       // check if this list of board has already been in use
            fillCells.Remove(boardName);

        fillCells.Add(boardName, new List<int>());  // add the list to fill cells dictionary
    }

    #endregion

    //------------------------------- Managment Methods -------------------------------------//

    #region Managment methods

    /* User registration to the system */
    public void Register(PlayerObject player, int[] lstAdvisers, int[] champs)
    {
        lock (registerLocker)    
        {
            try
            {
                ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

                if (checkAvilableData(player, lstAdvisers, channel))                // checking information available for registration
                {
                    if (insertPlayerQuery(player, channel))                         // insert player to Players table
                    {
                        if (insertAdviserPerPlayerQuery(player, lstAdvisers))       // insert advisers for player
                        {
                            if (insertChampPerPlayerQuery(player, champs, channel)) // insert record of participation
                            {
                                channel.ClientLogInFromRergister(player.UserName);  // log in the new user
                                clients.Add(player.UserName, channel);
                                channel.WriteStatus("Hello " + player.UserName + ", please choose board to play");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Client Off
                closeClient(player.UserName, null);
            }
        }
    }

    /* Log in user */
    public void LogIn(string userName, ICallBack channel)
    {
        try
        {
            if (channel == null)
                channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

            // check for avilable log in user 
            if (userName.Equals(COMPUTER))
                channel.validateLogInUserName(false, userName, "You can't use 'Computer' as User Name");
            else if (checkAvilableUserName(userName))
                channel.validateLogInUserName(false, userName, "User Name does not exist");
            else if (clients.Keys.Contains(userName))
                channel.validateLogInUserName(false, userName, "User Name already in use");
            else
            {
                clients.Add(userName, channel);
                channel.validateLogInUserName(true, userName);
                channel.WriteStatus("Hello " + userName + ", please choose board to play");
                getBoardsByMode(userName, 's');
            }
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Game player registration by play mode */
    public void RegisterBoard(string boardName, char playMode, string userName, int boardSize)
    {
        if (playMode == 's')
            RegisterSinglePlayer(userName, boardSize);
        else
            RegisterMultiPlayer(boardName, userName, boardSize);
    }

    /* Register player to multiplayer board */
    private void RegisterMultiPlayer(string boardName, string userName, int boardSize)
    {
        try
        {
            Dictionary<string, ICallBack> players;
            if (boardsComm.Keys.Contains(boardName))    // get board's dictionary
                players = boardsComm[boardName];
            else
                players = new Dictionary<string, ICallBack>();

            char tok = token[players.Count];            // set token to player

            if (players.Count < 2)                      // board still not full - insert player
            {
                players.Add(userName, clients[userName]);
                players[userName].setToken(tok);

                boardsComm[boardName] = players;
            }

            if (players.Count == 1)                     // first player connection
            {
                players[userName].WriteStatus("Waiting for another player");
                players[userName].setStartTurn(true);
            }
            else if (players.Count == 2)                // second player connection
            {
                // initiate all data for multiplayer game
                InitBoard(boardName, boardSize);
                InitFillCells(boardName, boardSize);
                changeTurnStatus(players, userName, 'O');
            }
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, boardName);
        }
    }

    /* Register player to single player board */
    private void RegisterSinglePlayer(string userName, int boardSize)
    {
        try
        {
            Dictionary<string, ICallBack> players;
            if (boardsComm.Keys.Contains(userName)) // get board's dictionary
                players = boardsComm[userName];
            else
                players = new Dictionary<string, ICallBack>();

            char tok = 'X';                         // set token to player

            if (!players.ContainsKey(userName))     // insert player to game
            {
                players.Add(userName, clients[userName]);
                players.Add(COMPUTER, null);
                boardsComm[userName] = players;
            }

            // initiate all data for single player game
            players[userName].setToken(tok);
            InitBoard(userName, boardSize);
            InitEmptyCells(userName, boardSize);
            InitFillCells(userName, boardSize);
            players[userName].WriteStatus("My turn");
            players[userName].setStartTurn(true);
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, userName);
        }
    }

    /* Make another game by play mode */
    public void PlayAgain(string boardName, char playMode, string userName, char token, int boardSize)
    {
        if (playMode == 's')
            RegisterSinglePlayer(userName, boardSize);
        else
            PlayAgainMultiPlayer(boardName, userName, token, boardSize);
    }

    /* Arrange data for another game multiplayer */
    private void PlayAgainMultiPlayer(string boardName, string userName, char token, int boardSize)
    {
        try
        {
            Dictionary<string, ICallBack> players = boardsComm[boardName];

            if (players.ContainsKey(userName))
            {
                // set number of players
                if (playAgainBoards.ContainsKey(boardName))
                    playAgainBoards[boardName]++;
                else
                    playAgainBoards.Add(boardName, 1);
            }

            if (playAgainBoards[boardName] == 1)        // first player connection
            {
                players[userName].WriteStatus("Waiting for another player");
                players[userName].setStartTurn(true);
            }
            else if (playAgainBoards[boardName] == 2)   // second player connection
            {
                // initiate all data for multiplayer game
                InitBoard(boardName, boardSize);
                InitFillCells(boardName, boardSize);
                changeTurnStatus(players, userName, token);
                playAgainBoards[boardName] = 0;
            }
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, boardName);
        }
    }

    /* Handles leave game event */
    public void LeaveGame(string boardName, string userName, bool logOut)
    {
        try
        {
            Dictionary<string, ICallBack> players = boardsComm[boardName];

            foreach (var player in players)
            {
                if (player.Value != null)
                {
                    if (!player.Key.Equals(userName))   // actions for the player who didnt left
                    {
                        player.Value.WriteStatus("Other player has left");
                        System.Threading.Thread.Sleep(1000);
                        player.Value.displayRooms();
                        player.Value.WriteStatus("Choose board to play");
                    }
                    else                                // actions for the leaving player
                    {
                        if (!logOut)
                        {
                            player.Value.displayRooms();
                            player.Value.WriteStatus("Choose board to play");
                        }
                    }
                    player.Value.setStartTurn(false);   // stop game nobodys turn
                }
            }

            players.Clear();                            // delete game board at dictionary
            boardsComm[boardName] = players;
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, boardName);
        }
    }

    /* Close data of user when exit */
    public void closeClient(string userName, string boardName)
    {
        if (userName != null)
        {
            clients.Remove(userName);               
            if (boardsComm.ContainsKey(userName))   
            {
                boardsComm.Remove(userName);        
                clearBoard(userName);
            }
        }

        if (boardName != null)
        {
            if (boardsComm.ContainsKey(boardName))
            {
                LeaveGame(boardName, userName, true);
                clearBoard(boardName);
            }
        }
    }

    #endregion

    //------------------------------ Game Actions Methods -----------------------------------//

    #region Game actions methods

    /* Clear board content */
    private void clearBoard(string boardName)
    {
        if (boardsCell.ContainsKey(boardName))
            boardsCell.Remove(boardName);

        if (emptyCells.ContainsKey(boardName))
            emptyCells.Remove(boardName);
    }

    /* Commit turn by play mode */
    public void CommitTurn(string boardName, char playMode, string userName, char tokenSender, int i, int j, int boardSize)
    {
        if (playMode == 's')
            commitSinglePlayer(boardName, userName, i, j, boardSize);
        else
            commitMultiPlayer(boardName, userName, tokenSender, i, j, boardSize);
    }

    /* Commit turn of single player mode */
    private void commitSinglePlayer(string boardName, string userName, int i, int j, int boardSize)
    {
        try
        {
            if (boardsCell.Keys.Contains(boardName))
            {
                List<int> winIndexs;

                // update all structures with selected move
                char[,] cell = boardsCell[boardName];
                cell[i, j] = 'X';
                emptyCells[boardName].Remove(i * boardSize + j);
                fillCells[boardName].Add(i * boardSize + j);
                boardsCell[boardName] = cell;
                ICallBack ch = boardsComm[boardName][userName];

                ch.UpdateBoard(i, j);

                if (isWon(boardName, 'X', boardSize, out winIndexs))   // check X Win
                {
                    ch.WriteStatus("Player X Won!");
                    ch.FinishGame('X', winIndexs.ToArray());
                    ch.setStartTurn(false);

                    saveGameRound(boardName, userName, 'X');
                    clearBoard(boardName);
                }
                else if (isFull(boardName, boardSize))  // check for draw
                {
                    ch.WriteStatus("Draw!");
                    ch.FinishGame(' ', null);
                    ch.setStartTurn(false);

                    saveGameRound(boardName, null, 'T');
                    clearBoard(boardName);
                }
                else    // commit computer's turn
                {
                    ch.SwitchTurn();
                    ch.WriteStatus("Computer turn - wait for your turn");
                    System.Threading.Thread.Sleep(2000);

                    // update all structures with selected move
                    Random rand = new Random();
                    int cellIndex = emptyCells[boardName][rand.Next(emptyCells[boardName].Count)];
                    fillCells[boardName].Add(cellIndex);

                    int row = cellIndex / boardSize;
                    int col = cellIndex % boardSize;

                    cell[row, col] = 'O';
                    emptyCells[boardName].Remove(cellIndex);
                    boardsCell[boardName] = cell;
                    ch.UpdateBoard(row, col);

                    if (isWon(boardName, 'O', boardSize, out winIndexs))   // check O Win
                    {
                        ch.WriteStatus("Player O Won!");
                        ch.FinishGame('O', winIndexs.ToArray());
                        ch.setStartTurn(false);

                        saveGameRound(boardName, COMPUTER, 'O');
                        clearBoard(boardName);
                    }
                    else if (isFull(boardName, boardSize))  // check for draw
                    {
                        ch.WriteStatus("Draw!");
                        ch.FinishGame(' ', null);
                        ch.setStartTurn(false);

                        saveGameRound(boardName, null, 'T');
                        clearBoard(boardName);
                    }
                    else        // game not over switch turn
                    {
                        ch.SwitchTurn();
                        ch.WriteStatus("My turn");
                    }
                }
            }
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, boardName);
        }
    }

    /* Commit turn of multiplayer mode */
    private void commitMultiPlayer(string boardName, string userName, char tokenSender, int i, int j, int boardSize)
    {
        try
        {
            if (boardsCell.Keys.Contains(boardName))
            {
                List<int> winIndexs;
                // update all structures with selected move
                char[,] cell = boardsCell[boardName];
                cell[i, j] = tokenSender;
                fillCells[boardName].Add(i * boardSize + j);
                boardsCell[boardName] = cell;

                Dictionary<string, ICallBack> players = boardsComm[boardName];

                foreach (var player in players)
                {
                    player.Value.UpdateBoard(i, j);
                    player.Value.SwitchTurn();
                }

                changeTurnStatus(players, userName, tokenSender);

                if (isWon(boardName, tokenSender, boardSize, out winIndexs))   // check for win
                {
                    foreach (var player in players)
                    {
                        player.Value.WriteStatus("Player " + tokenSender + " Won!");
                        player.Value.FinishGame(tokenSender, winIndexs.ToArray());
                        player.Value.setStartTurn(false);
                    }
                    saveGameRound(boardName, userName, tokenSender);
                    clearBoard(boardName);
                }
                else if (isFull(boardName, boardSize))          // check for draw
                {
                    foreach (var player in players)
                    {
                        player.Value.WriteStatus("Draw!");
                        player.Value.FinishGame(' ', null);
                        player.Value.setStartTurn(false);
                    }
                    saveGameRound(boardName, null, 'T');
                    clearBoard(boardName);
                }
            }
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, boardName);
        }
    }

    /* Save finished game to system history */
    private void saveGameRound(string boardName, string winnerUserName, char winner)
    {
        // get all game's parameters
        string jsonString = "";
        int winnerPlayerID = -1;
        int cellIndex = fillCells[boardName][0];
        int boardSize = boardsCell[boardName].GetLength(0);
        int row = cellIndex / boardSize;
        int col = cellIndex % boardSize;
        int boardID = 0;

        char starter = boardsCell[boardName][row, col];

        // json of game's data including starter steps and winner
        jsonString += "{\"starter\":\"" + starter;
        jsonString += "\", \"steps\":" + fillCells[boardName].ToJSON();
        jsonString += ", \"winner\":\"" + winner + "\"}";

        bool single = // check game play mode
            (from p in db.Players
             where p.userName.Equals(boardName)
             select p.userName).Any();

        if (single) // single player
        {
            string boardNameSingle = boardSize + "X" + boardSize;
            boardID =
                (from b in db.Boards
                 where b.name.Equals(boardNameSingle)
                 select b.Id).First();
        }
        else        // multiplayer
        {
            boardID =
            (from board in db.Boards
             where board.name.Equals(boardName)
             select board.Id).First();
        }

        string[] users = boardsComm[boardName].Keys.ToArray();

        // get players id
        var players =
             (from player in db.Players
              where users.Contains(player.userName)
              select player).ToArray();

        int winnerID = players[1].id;
        int loserID = players[0].id;

        if (winnerUserName != null)
        {
            winnerPlayerID =
                (from player in players
                 where player.userName.Equals(winnerUserName)
                 select player.id).First();

            if (winnerUserName.Equals(players[0].userName))
            {
                winnerID = players[0].id;
                loserID = players[1].id;
            }
        }

        // insert game to history table
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            string sql = string.Format("Insert into GamesHistory(boardID, losser, winner, steps, date) values({0}, {1}, {2}, '{3}', '{4}')", boardID, loserID, winnerID, jsonString, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) { }
        }
    }

    /* Change turn */
    private void changeTurnStatus(Dictionary<string, ICallBack> players, string userName, char tokenSender)
    {
        try
        {
            string otherUserName = "";
            foreach (string name in players.Keys)   // get second userName for callback
            {
                if (!name.Equals(userName))
                {
                    otherUserName = name;
                    break;
                }
            }

            if (tokenSender == 'X')
                players[userName].WriteStatus("O turn - wait for your turn");
            else
                players[userName].WriteStatus("X turn - wait for your turn");

            players[otherUserName].WriteStatus("My turn");
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Determine if the player with the specified token wins */
    private bool isWon(string boardName, char token, int boardSize, out List<int> winIndexs)
    {
        winIndexs = new List<int>(); 
        if (boardsCell.Keys.Contains(boardName))
        {
            bool flag = true;
            char[,] cell = boardsCell[boardName];

            // Check all rows
            for (int i = 0; i < boardSize; i++)
            {
                winIndexs.Clear();
                flag = true;
                for (int j = 0; j < boardSize; j++)
                {
                    if (cell[i, j] != token)
                    {
                        flag = false;
                        winIndexs.Clear();
                    }
                    else
                        winIndexs.Add(i * boardSize + j);
                }

                if (flag)
                    return true;
            }

            // Check all columns 
            for (int j = 0; j < boardSize; j++)
            {
                winIndexs.Clear();
                flag = true;
                for (int i = 0; i < boardSize; i++)
                {
                    if (cell[i, j] != token)
                    {
                        flag = false;
                        winIndexs.Clear();
                    }
                    else
                        winIndexs.Add(i * boardSize + j);
                }

                if (flag)
                    return true;
            }

            // Check major diagonal 
            winIndexs.Clear();
            flag = true;
            for (int i = 0; i < boardSize; i++)
            {
                if (cell[i, i] != token)
                {
                    flag = false;
                    winIndexs.Clear();
                }
                else
                    winIndexs.Add(i * boardSize + i);    
            }
            if (flag)
                return true;

            // Check sub diagonal 
            winIndexs.Clear();
            flag = true;
            for (int i = 0; i < boardSize; i++)
            {
                if (cell[i, boardSize - 1 - i] != token)
                {
                    flag = false;
                    winIndexs.Clear();
                }
                else
                    winIndexs.Add(i * boardSize + (boardSize - 1 - i));
            }
            if (flag)
                return true;

            return false;
        }
        else
            return false;
    }

    /* Determine if the cells are all occupied */
    private bool isFull(string boardName, int boardSize)
    {
        if (boardsCell.Keys.Contains(boardName))
        {
            char[,] cell = boardsCell[boardName];

            for (int i = 0; i < boardSize; i++)
                for (int j = 0; j < boardSize; j++)
                    if (cell[i, j] == ' ')
                        return false; // At least one cell is not filled
            // All cells are filled
            return true;
        }
        else
            return false;
    }

    #endregion

    //------------------------------- Validation Methods ------------------------------------//

    #region Validation methods

    /* Check if all data avilable */
    private bool checkAvilableData(PlayerObject player, int[] lstAdvisers, ICallBack channel)
    {
        try
        {
            if (!checkAvilableUserName(player.UserName))    // checks user name
            {
                channel.validateRegisterUserName("UserName is used");
                return false;
            }

            if (!checkAvilableAdvisers(lstAdvisers))        // checks advisers
            {
                var advisers = getAllAvilableAdvisers<PlayerObject>();
                channel.refreshAdvisersList(advisers.ToArray());
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            // Client Off
            return false;
        }
    }

    /* Check for avilable user name */
    private bool checkAvilableUserName(string userName)
    {
        var userNames =
            (from player in db.Players
             where player.userName.Equals(userName)
             select player.userName).ToList();

        return !userNames.Contains(userName);
    }

    /* Check for avilable advisers */
    private bool checkAvilableAdvisers(int[] lstAdvisers)
    {
        foreach (int adviserId in lstAdvisers)
        {
            var freeAdvisers =
                from adviser in db.PlayersAdvisers
                where adviser.adviserID == adviserId
                select adviser.adviserID;

            if (freeAdvisers.Count() != 0)
                return false;
        }

        return true;
    }

    /* Check at data base for avilable data */
    private bool checkIfDataExists(object obj)
    {
        bool exist = false;

        if (obj is PlayerObject)        // check on players and advisers
        {
            exist =
               (from p in db.Players
                where p.id == ((PlayerObject)obj).Id
                select p).Any();

            return exist;
        }

        else if (obj is ChampsObject)   // check on championships
        {
            exist =
                (from c in db.Champs
                 where c.Id == ((ChampsObject)obj).Id
                 select c).Any();

            return exist;
        }

        return exist;
    }

    #endregion

    //--------------------------------- Get Info Methods ------------------------------------//

    #region Information methods

    /* Get all boards by mode */
    public void getBoardsByMode(string userName, char mode)
    {

        var x =
            from board in db.Boards
            where board.mode == mode
            select new BoardsObject { Id = board.Id, Name = board.name, Size = board.size, Mode = board.mode, Description = board.description };
        
        try
        {
            clients[userName].sendListOfBoards(x.ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get boards details and description */
    public void getBoardDetails(string userName, string boardName)
    {
        var x =
             from board in db.Boards
             where board.name.Equals(boardName)
             select new BoardsObject { Id = board.Id, Name = board.name, Size = board.size, Mode = board.mode, Description = board.description };

        int onlinePlayers = 0;
        if (boardsComm.Keys.Contains(boardName))
            onlinePlayers = boardsComm[boardName].Count();

        try
        {
            clients[userName].sendBoardDetails(x.ToArray(), onlinePlayers);
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Retrives number of players for board */
    public int getPlayersPerBoard(string boardName)
    {
        int onlinePlayers = 0;
        if (boardsComm.Keys.Contains(boardName))
            onlinePlayers = boardsComm[boardName].Count();

        return onlinePlayers;
    }

    /* Retrives all avilable information to register form */
    public void getRegisterInfo()
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        var advisers = getAllAvilableAdvisers<PlayerObject>(); 
        var champs = getAllChamps<ChampsObject>();

        try
        {
            channel.sendRegisterInfo(advisers.ToArray(), champs.ToArray());
        }
        catch (Exception e)
        {
            // Client Off
        }
    }

    /* Get all players that matches to column and value selected in DataGrid */
    private void getPlayersMatchColumn(out IEnumerable<PlayerObject> players, string colHeader, string value)
    {
        var allPlayers =    // all players at db
            from player in db.Players
            where !player.userName.Equals("Computer")
            select player;

        IQueryable<Player> query = allPlayers.AsQueryable();

        // concat where to query by selected column
        switch (colHeader)
        {
            case "UserName":
                query = query.Where(p => p.userName.Equals(value));
                break;
            case "FirstName":
                query = query.Where(p => p.firstName.Equals(value));
                break;
            case "LastName":
                query = query.Where(p => p.lastName.Equals(value));
                break;
            default:
                query = query.Where(p => p.id == 0); // an empty query because column not allowed
                break;
        }

        players =   // convert result to PlayerObject
            from q in query
            select new PlayerObject
            {
                Id = q.id,
                UserName = q.userName,
                FirstName = q.firstName,
                LastName = q.lastName
            };
    }

    #endregion

    //----------------------------- Get IEnumerable Methods ---------------------------------//

    #region IEnumerable methods

    /* Get all players with thier details */
    private IEnumerable<T> getAllPlayers<T>()
    {
        var players =
            from player in db.Players
            where !player.userName.Equals(COMPUTER)
            select new PlayerObject { 
                Id = player.id, 
                UserName = player.userName, 
                FirstName = player.firstName, 
                LastName = player.lastName, 
                CreatedAt = player.created_at, 
                Rank = player.PlayerRank.type
            };

        return (IEnumerable<T>)players;
    }

    /* Get all avilable advisers */
    private IEnumerable<T> getAllAvilableAdvisers<T>()
    {
        var notAvlAdvisers =
            from adviser in db.PlayersAdvisers
            select adviser.adviserID;

        var advisers =
            db.Players.Where(adviser => adviser.PlayerRank.type.Equals("Professional") && !notAvlAdvisers.Contains(adviser.id))
            .Select(adviser => new PlayerObject { Id = adviser.id, FirstName = adviser.firstName, LastName = adviser.lastName });

        return (IEnumerable<T>)advisers;
    }

    /* Get all championships */
    private IEnumerable<T> getAllChamps<T>()
    {
        var champs =
            from champ in db.Champs
            select new ChampsObject { Id = champ.Id, Name = champ.name, City = champ.city, Date = champ.date, Image = champ.image };

        return (IEnumerable<T>)champs;
    }

    /* Get all games from history */
    private IEnumerable<T> getAllGames<T>()
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        var games =
            from game in db.GamesHistories
            select new GameObject { 
                Id = game.Id, 
                BoardName = game.Board.name, 
                BoardSize = game.Board.size, 
                Date = game.date,
                Player1 = game.Player.userName, 
                Player2 = game.Player1.userName, 
                Steps = serializer.Deserialize<History>(game.steps) };

        return (IEnumerable<T>)games;
    }

    /* Get games who played by specipic player */
    private IEnumerable<T> getGamesByPlayer<T>(string userName)
    {
        var allGames = getAllGames<GameObject>();
        var games =
            from game in allGames
            where game.Player2.Equals(userName) || game.Player1.Equals(userName)
            select game;

        return (IEnumerable<T>)games;
    }

    /* Get championships particepated by specific player */
    private IEnumerable<T> getChampsByPlayer<T>(string userName)
    {
        var champs =
            from c in db.PlayersChamps
            join player in db.Players on c.playerID equals player.id
            where player.userName.Equals(userName)
            select new ChampsObject { Id = c.champID, Name = c.Champ.name, City = c.Champ.city, Date = c.Champ.date, Image = c.Champ.image };

        return (IEnumerable<T>)champs;
    }

    /* Get the players who played in specific game */
    private IEnumerable<T> getPlayerByGame<T>(GameObject game)
    {
        //player lose
        var players1 =
            from g in db.GamesHistories
            join player1 in db.Players on g.losser equals player1.id
            where g.Id == game.Id && !player1.userName.Equals(COMPUTER)
            select new PlayerObject { 
                Id = g.Player.id ,
                FirstName = g.Player.firstName, 
                LastName = g.Player.lastName, 
                UserName = g.Player.userName, 
                Rank = g.Player.PlayerRank.type, 
                CreatedAt = g.Player.created_at, 
            };

        //player won
        var players2 =
            from g in db.GamesHistories
            join player2 in db.Players on g.winner equals player2.id
            where g.Id == game.Id && !player2.userName.Equals(COMPUTER)
            select new PlayerObject { 
                Id = g.Player1.id, 
                FirstName = g.Player1.firstName, 
                LastName = g.Player1.lastName, 
                UserName = g.Player1.userName, 
                Rank = g.Player1.PlayerRank.type, 
                CreatedAt = g.Player1.created_at,
            };

        return (IEnumerable<T>)players1.Union(players2);
    }

    /* Get the advisers who advised in specific game */
    private IEnumerable<T> getAdviserByGame<T>(GameObject game)
    {
        // players of selected game
        var playersPerGame = getPlayerByGame<PlayerObject>(game);

        // advisers matched to players
        var advisers =
            from adviser in db.Players
            from player in playersPerGame
            from pa in db.PlayersAdvisers
            where pa.playerID == player.Id && pa.adviserID == adviser.id
            select new AdviserObject
            {
                Id = adviser.id,
                FirstName = adviser.firstName,
                LastName = adviser.lastName,
                UserName = adviser.userName,
                Rank = adviser.PlayerRank.type,
                CreatedAt = adviser.created_at,
                AdviseId = player.Id,
                AdviseTo = player.UserName
            };

        return (IEnumerable<T>)advisers;
    }

    /* Get all players who participated in specific championship */
    private IEnumerable<T> getPlayersByChamp<T>(ChampsObject champ)
    {
        var players =
            from pc in db.PlayersChamps
            where pc.champID == champ.Id
            select new PlayerObject
                {
                    Id = pc.Player.id,
                    UserName = pc.Player.userName,
                    FirstName = pc.Player.firstName,
                    LastName = pc.Player.lastName,
                    CreatedAt = pc.Player.created_at,
                    Rank = pc.Player.PlayerRank.type,
                };

        return (IEnumerable<T>)players;
    }

    /* Get the number of games per player */
    private IEnumerable<T> getPlayersGames<T>()
    {
        // count the number of loses of each player
        var playersLose =
            from player in db.Players
            join game in db.GamesHistories on player.id equals game.Player.id into joined
            from j in joined.DefaultIfEmpty()
            group j by player.userName into grouping
            select new PlayersGamesObject { UserName = grouping.Key, NumOfGames = grouping.Count(g => g.Player.id != null) };

        // count the number of wins of each player
        var playersWin =
            from player in db.Players
            join game in db.GamesHistories on player.id equals game.Player1.id into joined
            from j in joined.DefaultIfEmpty()
            group j by player.userName into grouping
            select new PlayersGamesObject { UserName = grouping.Key, NumOfGames = grouping.Count(g => g.Player.id != null) };

        // sum the results of win and lose for total games played
        var playersGames =
            (from player in playersLose.Union(playersWin)
             where !player.UserName.Equals(COMPUTER)
             group player by player.UserName into grouping
             select new PlayersGamesObject { UserName = grouping.Key, NumOfGames = grouping.Sum(player => player.NumOfGames) }).OrderBy(p => p.UserName);

        return (IEnumerable<T>)playersGames;
    }

    /* Get the number of championships hosted by every city */
    private IEnumerable<T> getCityChamps<T>()
    {
        var cityChamps = 
            from champ in db.Champs
            group champ by champ.city into cityGroup
            select new CityObject { Name = cityGroup.Key, NumOfChamps = cityGroup.Count() };

        return (IEnumerable<T>)cityChamps;
    }

    #endregion

    //----------------------------- Service Queries Methods ---------------------------------//

    #region Methods of service queries

    /* Insert player to DataBase */
    private bool insertPlayerQuery(PlayerObject player, ICallBack channel)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            // convert rank type to rank number
            int rank =
                (from pr in db.PlayerRanks
                where pr.type.Equals(player.Rank)
                select pr.rank).FirstOrDefault();

            string sql = string.Format("Insert into Players(userName, firstName, lastName, created_at, rank) values('{0}', '{1}', '{2}', '{3}', {4})", player.UserName, player.FirstName, player.LastName, player.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"), rank);
            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        return true;
    }

    /* Insert advisers for player by their ID */
    private bool insertAdviserPerPlayerQuery(PlayerObject player, int[] lst)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            // get player given id from data base
            int playerId =
                (from p in db.Players
                 where p.userName.Equals(player.UserName)
                 select p.id).First();

            foreach (int adviserId in lst)
            {
                string sql = string.Format("Insert into PlayersAdvisers(adviserID, playerID) values({0}, {1})", adviserId, playerId);
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (InvalidOperationException ex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /* Insert participated championships for player by their ID */
    private bool insertChampPerPlayerQuery(PlayerObject player, int[] champs, ICallBack channel)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            // get player given id from data base
            int playerId =
                (from p in db.Players
                 where p.userName.Equals(player.UserName)
                 select p.id).First();

            foreach (int champId in champs)
            {
                string sql = string.Format("Insert into PlayersChamps(playerID, champID) values({0}, {1})", playerId, champId);
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /* Insert a new championship or update an existing one */
    public void updateInsertChamptionQuery(ChampsObject champ, bool update)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();

        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            string sql;

            if (update)
                sql = string.Format("Update Champs SET name='{0}', date='{1}', city='{2}', image='{3}' WHERE id={4}", champ.Name, champ.Date.ToString("yyyy-MM-dd hh:mm:ss"), champ.City, champ.Image, champ.Id);
            else
                sql = string.Format("Insert into Champs(name, date, city, image) values('{0}', '{1}', '{2}', '{3}')", champ.Name, champ.Date.ToString("yyyy-MM-dd hh:mm:ss"), champ.City, champ.Image);


            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (update)
                    db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.Champs);

                var champs =
                    from c in db.Champs
                    select new ChampsObject { Id = c.Id, Name = c.name, City = c.city, Date = c.date, Image = c.image };

                channel.refreshChampsList(champs.ToArray());
            }
            catch (InvalidOperationException ex1) { }
            catch (Exception ex2) { }
        }
    }

    /* Get all games and send to client */
    public void getAllGamesHistory(string userName)
    {
        try
        {
            clients[userName].showGameHistory(getAllGames<GameObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    #endregion

    //--------------------------- Thread Hold Queries Methods -------------------------------//

    #region Queries methods

    /* Get all players details and send to client */
    public void getAllPlayersDetails(string userName, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showPlayersQuery(getAllPlayers<PlayerObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get players details to send this information to client's filter combobox */
    public void getAllPlayersDetailsToCB(string userName)
    {
        try
        {
            clients[userName].showAllPlayersInQueryToCB(getAllPlayers<PlayerObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get all games details and send to client */
    public void getAllGamesDetails(string userName, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showGamesQuery(getAllGames<GameObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get games details to send this information to client's filter combobox */
    public void getAllGamesDetailsToCB(string userName)
    {
        try
        {
            clients[userName].showAllGamesInQueryToCB(getAllGames<GameObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get all champs details and send to client */
    public void getAllChampsDetails(string userName, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showChampsQuery(getAllChamps<ChampsObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get champs details to send this information to client's filter combobox */
    public void getAllChampsDetailsToCB(string userName)
    {
        try
        {
            clients[userName].showAllChampsInQueryToCB(getAllChamps<ChampsObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get games of player and send to client */
    public void getGamesByPlayerDetails(string clientUserName, string selectedUserName, bool delay)
    {
        sleep(delay);

        try
        {
            clients[clientUserName].showGamesQuery(getGamesByPlayer<GameObject>(selectedUserName).ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(clientUserName, null);
        }
    }

    /* Get particepated champs by player and send to client */
    public void getChampsByPlayerDetails(string clientUserName, string selectedUserName, bool delay)
    {
        sleep(delay);

        try
        {
            clients[clientUserName].showChampsQuery(getChampsByPlayer<ChampsObject>(selectedUserName).ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(clientUserName, null);
        }
    }

    /* Get player of game and send to client */
    public void getPlayersByGameDetails(string userName, GameObject game, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showPlayersQuery(getPlayerByGame<PlayerObject>(game).ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get advisers of game and send to client */
    public void getAdvisersByGameDetails(string userName, GameObject game, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showAdvisersQuery(getAdviserByGame<AdviserObject>(game).ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get games of champ and send to client */
    public void getPlayersByChampDetails(string userName, ChampsObject champ, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showPlayersQuery(getPlayersByChamp<PlayerObject>(champ).ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get number of games by player and send to client */
    public void getPlayersNumOfGames(string userName, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showPlayersNumOfGamesQuery(getPlayersGames<PlayersGamesObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Get number of champs hosted by city and send to client */
    public void getCityNumOfChamps(string userName, bool delay)
    {
        sleep(delay);

        try
        {
            clients[userName].showCityNumOfChampsQuery(getCityChamps<CityObject>().ToArray());
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Decides whether delay the sending of the results */
    private void sleep(bool setDelay)
    {
        if (setDelay)
            System.Threading.Thread.Sleep(3000);
    }

    #endregion

    //------------------------------------ Update DB ----------------------------------------//

    #region Update DataBase methods

    /* Update player information in DataBase */
    public void updatePlayersDB(PlayerObject[] players, string userName)
    {
        string msg = null;
        bool containComp = false;

        List<PlayerObject> lst = new List<PlayerObject>();
        
        foreach (var player in players)
        {
            if (!containComp && player.UserName.Equals(COMPUTER))
                containComp = true;
            else if (checkIfDataExists(player))
            {
                // convert rank type to rank number
                int rank =
                    (from pr in db.PlayerRanks
                     where pr.type.Equals(player.Rank)
                     select pr.rank).FirstOrDefault();

                if (rank != 3) // Player is not adviser anymore check if he advised to someone and delete this bind
                    deleteAdvisingBind(player);
                
                // Update player fields
                using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
                {
                    string sql = string.Format("Update Players SET firstName='{0}', lastName='{1}', created_at='{2}', rank={3} WHERE id={4}",
                                 player.FirstName, player.LastName, player.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"), rank == 0 ? 2 : rank, player.Id);

                    SqlCommand cmd = new SqlCommand(sql, con);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.Players);
                    }
                    catch (Exception ex) { }
                }
            }
            else
                lst.Add(player);               
        }

        if (containComp)
            msg += "You can't edit computer!\n\n";

        if (lst.Any())
            msg += getMsgAfterPlayerDataError(lst.ToArray()) + "\n";

        try
        {
            clients[userName].messageAndRefreshTable(msg);
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }

    }

    /* Update adviser information in DataBase */
    public void updateAdviserDB(AdviserObject[] advisers, string userName)
    {
        List<AdviserObject> lst = new List<AdviserObject>();

        foreach (var adviser in advisers)
        {
            if (checkIfDataExists(adviser))
            {
                // convert rank type to rank number
                int rank =
                    (from pr in db.PlayerRanks
                     where pr.type.Equals(adviser.Rank)
                     select pr.rank).FirstOrDefault();

                using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
                {
                    string sql = string.Format("Update Players SET firstName='{0}', lastName='{1}', created_at='{2}', rank={3} WHERE id={4}",
                            adviser.FirstName, adviser.LastName, adviser.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"), rank == 0 ? 2 : rank, adviser.Id);

                    SqlCommand cmd = new SqlCommand(sql, con);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.Players);
                    }
                    catch (Exception ex) { }
                }

                if (rank != 3)  // check rank for advising
                    deleteAdvisingBind(adviser);
                else
                    updateAdvisingBind(adviser);
            }
            else
                lst.Add(adviser);
        }

        try
        {
            if (lst.Any())
            {
                String msg = getMsgAfterPlayerDataError(lst.ToArray());
                clients[userName].messageAndRefreshTable(msg);
            }
            else
                clients[userName].messageAndRefreshTable();
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Update championship information in DataBase */
    public void updateChampsDB(ChampsObject[] champs, string userName)
    {
        List<ChampsObject> lst = new List<ChampsObject>();

        foreach (var champ in champs)
        {
            if (checkIfDataExists(champ))
            {
                using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
                {
                    string sql = string.Format("Update Champs SET name='{0}', date='{1}', city='{2}', image='{3}' WHERE id={4}", champ.Name, champ.Date.ToString("yyyy-MM-dd hh:mm:ss"), champ.City, champ.Image, champ.Id);

                    SqlCommand cmd = new SqlCommand(sql, con);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.Champs);
                    }

                    catch (Exception ex) { }
                }
            }
            else 
                lst.Add(champ);               
        }

        try
        {
            if (lst.Any())
            {
                String msg = getMsgAfterChampDataError(lst.ToArray());
                clients[userName].messageAndRefreshTable(msg);
            }
            else
                clients[userName].messageAndRefreshTable();
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Update player-adviser relationship */
    private void updateAdvisingBind(AdviserObject adviser)
    {
        // convert advised name to advised Id
        int adviseId =
            (from p in db.Players
             where p.userName.Equals(adviser.AdviseTo)
             select p.id).FirstOrDefault();

        if (adviseId != 0) // update only if player id is legal
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                string sql = string.Format("Update PlayersAdvisers SET playerID={0} WHERE adviserID={1}", adviseId, adviser.Id);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.Players);
                }
                catch (Exception ex) { }
            }
        }
    }

    #endregion

    //------------------------------------ Delete DB ----------------------------------------//

    #region Delete from DataBase methods

    /* Delete player from DataBase */
    public void deleteOneRowFromPlayers(PlayerObject player, string userName)
    {
        string msg = null;

        if (!clients.Keys.Contains(player.UserName)) // prevent from deleting online user
        {
            if (checkIfDataExists(player))           // delete only if user exists
                delPlayer(player);
            else
                msg = getMsgAfterPlayerDataError(player);
        }
        else
            msg = getMsgAfterLogInError(player);

        try
        {
            clients[userName].messageAndRefreshTable(msg);
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Delete adviser from DataBase */
    public void deleteOneRowFromAdvisers(AdviserObject adviser, string userName)
    {
        PlayerObject player = (PlayerObject)adviser;
        deleteOneRowFromPlayers(player, userName); 
    }

    /* Delete championship from DataBase */
    public void deleteOneRowFromChamps(ChampsObject champ, string userName)
    {
        string msg = null;

        if (checkIfDataExists(champ))   // delete only if champ exists
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                string sql = string.Format("DELETE FROM Champs WHERE id = {0}", champ.Id);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { }
            }
        }
        else
            msg = getMsgAfterChampDataError(champ);

        try
        {
            clients[userName].messageAndRefreshTable(msg);
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Delete multiple players or advisers from DataBase */
    public void deleteMultiRowFromPlayers(string colHeader, string value, string userName)
    {
        string msg = null;

        List<PlayerObject> lst = new List<PlayerObject>();
        IEnumerable<PlayerObject> players;
        getPlayersMatchColumn(out players, colHeader, value);

        if (players.Any()) // check if there are any players who matches to column's value
        {
            foreach (PlayerObject p in players)
            {
                // prevent from deleting online user
                if (!clients.Keys.Contains(p.UserName))
                    delPlayer(p);
                else
                    lst.Add(p);
            }

            if (lst.Any())
                msg = getMsgAfterLogInError(lst.ToArray());
        }
        else
            msg = getMsgAfterPlayerDataError(lst.ToArray());

        try
        {
            clients[userName].messageAndRefreshTable(msg);
        }
        catch (Exception e)
        {
            // Client Off
            closeClient(userName, null);
        }
    }

    /* Delete multiple championships from DataBase */
    public void deleteMultiRowFromChamps(ChampsObject champ, string colHeader, string value, string userName)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            string sql = string.Format("DELETE FROM Champs WHERE {0} = '{1}'", colHeader, value);

            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) { }
        }
    }

    /* Delete player and all foriegn keys relationships related */
    private void delPlayer(PlayerObject player)
    {
        // delete foriegn key bindes first
        deleteAdviserBind(player);
        deleteGameHistoryBind(player);
        deleteChampionshipBind(player);
        deletePlayer(player);
    }

    /* Delete player-adviser relationship query */ 
    private void deleteAdviserBind(PlayerObject player)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            // check if this player advising and delete this bind
            bool isAdvising =
                (from pa in db.PlayersAdvisers
                 where pa.playerID == player.Id || pa.adviserID == player.Id
                 select pa).Any();

            if (isAdvising)
            {
                string sql = string.Format("DELETE FROM PlayersAdvisers WHERE playerID = {0} OR adviserID={0}", player.Id);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { }
            }
        }
    }

    /* Delete player-adviser relationship after update rank to not professional player */
    private void deleteAdvisingBind(PlayerObject player)
    {
        // convert advised name to advised Id
        int adviseId =
            (from pa in db.PlayersAdvisers
             where pa.adviserID == player.Id
             select pa.playerID).FirstOrDefault();

        if (adviseId != 0)
        {
            using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
            {
                string sql = string.Format("DELETE FROM PlayersAdvisers WHERE adviserID={0}", player.Id);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.Players);
                }
                catch (Exception ex) { }
            }
        }
    }

    /* Delete player-gameHistory relationship query */ 
    private void deleteGameHistoryBind(PlayerObject player)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            // check if this player has games in history and delete this bind                    
            bool hasHistory =
                (from gh in db.GamesHistories
                 where gh.winner == player.Id || gh.losser == player.Id
                 select gh).Any();

            if (hasHistory)
            {
                string sql = string.Format("DELETE FROM GamesHistory WHERE winner = {0} OR losser = {0}", player.Id);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { }
            }
        }
    }

    /* Delete player-champ relationship query */
    private void deleteChampionshipBind(PlayerObject player)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            // check if this player participating in champ and delete this bind
            bool isParticipate =
                (from pc in db.PlayersChamps
                 where pc.playerID == player.Id
                 select pc).Any();

            if (isParticipate)
            {
                string sql = string.Format("DELETE FROM PlayersChamps WHERE playerID = {0}", player.Id);

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { }
            }
        }
    }

    /* Delete player query */
    private void deletePlayer(PlayerObject player)
    {
        // delete the choosen player
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            string sql = string.Format("DELETE FROM Players WHERE id = {0}", player.Id);

            SqlCommand cmd = new SqlCommand(sql, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) { }
        }
    }

    #endregion

    //---------------------------------- Messages alert -------------------------------------//

    #region Messages alert

    /* Error on missing championship data */
    private string getMsgAfterChampDataError(ChampsObject camp)
    {
        List<ChampsObject> lst = new List<ChampsObject> { camp };
        return getMsgAfterChampDataError(lst.ToArray());
    }

    /* Error on multiple missing championship data */
    private string getMsgAfterChampDataError(ChampsObject[] arr)
    {
        string message = "";

        if (arr.Count() > 1)
        {
            message = "Some of the championships you chose have already deleted:\n";

            for (int i = 0; i < arr.Length; i++)
                message += (i + 1) + ". " + arr[i].Name + "\n";
            message += "\nTable will now update";
        }
        else
            message = arr[0].Name + " has already deleted\nTable will now update";

        return message;
    }

    /* Error on deleting logged in user */
    private string getMsgAfterLogInError(PlayerObject player)
    {
        List<PlayerObject> lst = new List<PlayerObject> { player };
        return getMsgAfterLogInError(lst.ToArray());
    }

    /* Error on deleting multiple logged in users */
    private string getMsgAfterLogInError(PlayerObject[] arr)
    {
        string message = "Cannot delete an online ";

        if (arr.Any())
        {
            message += "users:\n";

            for (int i = 0; i < arr.Length; i++)
                message += (i + 1) + ". " + arr[i].UserName + "\n";
            message += "\nTable will now update";
        }
        else
            message += "user";

        return message;
    }

    /* Error on missing player data */
    private string getMsgAfterPlayerDataError(PlayerObject player)
    {
        List<PlayerObject> lst = new List<PlayerObject> { player };
        return getMsgAfterPlayerDataError(lst.ToArray());
    }

    /* Error on multiple missing player data */
    private string getMsgAfterPlayerDataError(PlayerObject[] arr)
    {
        string message = "";

        if (arr.Count() > 1)
        {
            message = "Some of the players you chose have already deleted:\n";

            for (int i = 0; i < arr.Length; i++)
                message += (i + 1) + ". " + arr[i].UserName + "\n";
            message += "\nTable will now update";
        }
        else if (arr.Count() == 1)
            message = arr[0].UserName + " has already deleted\nTable will now update";
        else
            message = "No matching players for selected value";

        return message;
    }
    
    #endregion
}


//------------------------------------- JSON Class ------------------------------------------//

#region JSON Class
public static class JsonHelper
{
    public static string ToJSON(this object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }

    public static string ToJSON(this object obj, int recursionDepth)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
        return serializer.Serialize(obj);
    }
}

#endregion

