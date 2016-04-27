// Sagi Shoffer & Oren Yulzary

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract(CallbackContract= typeof(ICallBack), SessionMode=SessionMode.Required)]
public interface IGame
{
    [OperationContract(IsOneWay = true)]
    void Register(PlayerObject player, int[] lstAdvisers, int[] champs);

    [OperationContract(IsOneWay = true)]
    void LogIn(string userName, ICallBack channel);

    [OperationContract(IsOneWay = true)]
    void RegisterBoard(string boardName, char mode, string userName, int boardSize);

    [OperationContract(IsOneWay = true)]
    void PlayAgain(string boardName, char playMode, string userName, char token, int boardSize);

    [OperationContract(IsOneWay = true)]
    void LeaveGame(string boardName, string userName, bool logOut);

    [OperationContract(IsOneWay = true)]
    void CommitTurn(string boardName, char playMode, string userName, char tokenSender, int i, int j, int boardSize);

    [OperationContract(IsOneWay = true)]
    void closeClient(string userName, string boardName);

    [OperationContract(IsOneWay = true)]
    void getBoardsByMode(string userName, char mode);

    [OperationContract(IsOneWay = true)]
    void getBoardDetails(string userName, string boardName);

    [OperationContract]
    int getPlayersPerBoard(string boardName);

    [OperationContract(IsOneWay = true)]
    void getRegisterInfo();

    [OperationContract(IsOneWay = true)]
    void updateInsertChamptionQuery(ChampsObject champ, bool update);

    [OperationContract(IsOneWay = true)]
    void getAllGamesHistory(string userName);

    [OperationContract(IsOneWay = true)]
    void getAllPlayersDetails(string userName, bool delay);

    [OperationContract(IsOneWay = true)]
    void getAllGamesDetails(string userName, bool delay);

    [OperationContract(IsOneWay = true)]
    void getAllGamesDetailsToCB(string userName);

    [OperationContract(IsOneWay = true)]
    void getAllChampsDetails(string userName, bool delay);

    [OperationContract(IsOneWay = true)]
    void getAllPlayersDetailsToCB(string userName);

    [OperationContract(IsOneWay = true)]
    void getGamesByPlayerDetails(string clientUserName, string selectedUserName, bool delay);

    [OperationContract(IsOneWay = true)]
    void getChampsByPlayerDetails(string clientUserName, string selectedUserName, bool delay);

    [OperationContract(IsOneWay = true)]
    void getPlayersByGameDetails(string userName, GameObject game, bool delay);

    [OperationContract(IsOneWay = true)]
    void getAdvisersByGameDetails(string userName, GameObject game, bool delay);

    [OperationContract(IsOneWay = true)]
    void getAllChampsDetailsToCB(string userName);

    [OperationContract(IsOneWay = true)]
    void getPlayersByChampDetails(string userName, ChampsObject champ, bool delay);

    [OperationContract(IsOneWay = true)]
    void getPlayersNumOfGames(string userName, bool delay);

    [OperationContract(IsOneWay = true)]
    void getCityNumOfChamps(string userName, bool delay);

    [OperationContract(IsOneWay = true)]
    void updatePlayersDB(PlayerObject[] players, string userName);

    [OperationContract(IsOneWay = true)]
    void updateAdviserDB(AdviserObject[] advisers, string userName);

    [OperationContract(IsOneWay = true)]
    void updateChampsDB(ChampsObject[] champs, string userName);

    [OperationContract(IsOneWay = true)]
    void deleteOneRowFromPlayers(PlayerObject player, string userName);

    [OperationContract(IsOneWay = true)]
    void deleteOneRowFromAdvisers(AdviserObject adviser, string userName);

    [OperationContract(IsOneWay = true)]
    void deleteOneRowFromChamps(ChampsObject champ, string userName);

    [OperationContract(IsOneWay = true)]
    void deleteMultiRowFromPlayers(string colHeader, string value, string userName);

    [OperationContract(IsOneWay = true)]
    void deleteMultiRowFromChamps(ChampsObject champ, string colHeader, string value, string userName);
        
}

public interface ICallBack
{
    [OperationContract(IsOneWay = true)]
    void UpdateBoard(int i, int j);

    [OperationContract(IsOneWay = true)] 
    void SwitchTurn();

    [OperationContract(IsOneWay = true)]
    void WriteStatus(string str);

    [OperationContract(IsOneWay = true)]
    void setToken(char token);

    [OperationContract(IsOneWay = true)]
    void setStartTurn(bool start);

    [OperationContract(IsOneWay = true)]
    void FinishGame(char token, int[] winIndexs);

    [OperationContract(IsOneWay = true)]
    void displayRooms();

    [OperationContract(IsOneWay = true)]
    void validateRegisterUserName(string messaage);

    [OperationContract(IsOneWay = true)]
    void ClientLogInFromRergister(string userName);

    [OperationContract(IsOneWay = true)]
    void validateLogInUserName(bool exist, string userName, string message = "");

    [OperationContract(IsOneWay = true)]
    void sendListOfBoards(BoardsObject[] boards);

    [OperationContract(IsOneWay = true)]
    void sendBoardDetails(BoardsObject[] boards, int numberOfPlayers);
    
    [OperationContract(IsOneWay = true)]
    void sendRegisterInfo(PlayerObject[] advisers, ChampsObject[] champs);

    [OperationContract(IsOneWay = true)]
    void refreshChampsList(ChampsObject[] champs);

    [OperationContract(IsOneWay = true)]
    void refreshAdvisersList(PlayerObject[] advisers);

    [OperationContract(IsOneWay = true)]
    void showGameHistory(GameObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showPlayersQuery(PlayerObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showAdvisersQuery(AdviserObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showGamesQuery(GameObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showChampsQuery(ChampsObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showAllPlayersInQueryToCB(PlayerObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showAllGamesInQueryToCB(GameObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showAllChampsInQueryToCB(ChampsObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showPlayersNumOfGamesQuery(PlayersGamesObject[] arr);

    [OperationContract(IsOneWay = true)]
    void showCityNumOfChampsQuery(CityObject[] arr);

    [OperationContract(IsOneWay = true)]
    void messageAndRefreshTable(string message = null);
}
