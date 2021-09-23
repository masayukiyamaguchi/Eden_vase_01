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

    //シーン切り替えで破壊されないようにする
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {

    }


    // ルーム名を指定して参加
    public void CreateRoom()
    {
        //部屋名決定
        randomRoomName = Guid.NewGuid().ToString("N").Substring(0, 6);

        // 作成するルームのルーム設定を行う
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;

        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom(randomRoomName, roomOptions, TypedLobby.Default);
    }


    public string randomRoomNameFunc() 
    { 
        return randomRoomName;
    }


    // ルーム名を指定して参加
    public void JointIdentyRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }  



    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        //var position = new Vector3(Random.Range(-3f, 3f), Random.Range(3f, 4f),0.0f);
        //PhotonNetwork.Instantiate("Playersample2", position, Quaternion.identity);
    }


    public GameObject ActivePlayerSporn(string ActivePlayerName) 
    {
        var position = new Vector3(0.0f,3.7f, -3.24f);
        GameObject playerGameobject = PhotonNetwork.Instantiate(ActivePlayerName, position, Quaternion.identity);
        return playerGameobject;

    }



}
