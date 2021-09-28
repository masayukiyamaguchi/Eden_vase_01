using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;


public class PhotonSystem : MonoBehaviourPunCallbacks
{
    //部屋名
    public string randomRoomName;

    public bool IsRoomIn;
    public bool SoloPlay;

    public int inRoomPlayerCount;
    public int RoomMaxPlayers;

    //シーン切り替えで破壊されないようにする
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        IsRoomIn = false;
    }


    public void ConnectMasterServer() 
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }


    public void ReaveMasterServer()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.Disconnect();
    }




    // ルームを生成
    public void CreateRoom(){

        //部屋名決定
        randomRoomName = Guid.NewGuid().ToString("N").Substring(0, 6);
        Debug.Log("RandomCreated");

        // 作成するルームのルーム設定を行う
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;

        // ルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom(randomRoomName, roomOptions, TypedLobby.Default);

    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {

    }


    //ランダムの部屋番号を返す
    public string randomRoomNameFunc() 
    { 
        return randomRoomName;
    }

    //ルームの最大参加人数を返す
    public string RoomMaxPlayersFunc()
    {
        //Debug.Log(RoomInfo.MaxPlayers());　このように書いてみたがだめだった。
        return null;
    }

    //ルームの現在の参加人数を返す
    public string RoomPlayerCountFunc()
    {
        return null;
    }

    //部屋から退出
    public void LeaveRoomFunc() 
    {
        PhotonNetwork.LeaveRoom();

    }


    // ルーム名を指定して参加
    public void JointIdentyRoom(string roomName)
    {

        PhotonNetwork.JoinRoom(roomName);
    }



    //コールバック色々


    // ルームへの参加が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームへ参加しました");
        Debug.Log(randomRoomName);
        IsRoomIn = true;
    }

    // ルーム名を指定したルームへの参加が失敗した時に呼ばれるコールバック
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームへの参加に失敗しました: {message}");
        IsRoomIn = false;
    }

    // ルームから退出した時に呼ばれるコールバック
    public override void OnLeftRoom()
    {
        Debug.Log("ルームから退出しました");
        IsRoomIn = false;
    }




    //プレイヤースポーン
    public GameObject ActivePlayerSporn(string ActivePlayerName) 
    {
        var position = new Vector3(0.0f,3.7f, -3.24f);
        GameObject playerGameobject = PhotonNetwork.Instantiate(ActivePlayerName, position, Quaternion.identity);
        return playerGameobject;

    }



}
