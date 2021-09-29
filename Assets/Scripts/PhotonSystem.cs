using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;
using Ludiq;
using Bolt;
using UnityEngine.UI;


public class PhotonSystem : MonoBehaviourPunCallbacks
{
    private static PhotonSystem instance;


    public static PhotonSystem Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(PhotonSystem);

                instance = (PhotonSystem)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this as PhotonSystem;

            //シーンが切り替わってもオブジェクトを壊さない
            DontDestroyOnLoad(gameObject);

            Application.targetFrameRate = 60;


            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }


    //部屋名
    public string randomRoomName;

    public bool IsRoomIn;
    public bool SoloPlay;

    public int inRoomPlayerCount;
    public int RoomMaxPlayers;

    public int PlayerCount;


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
        //PhotonNetwork.JoinOrCreateRoom(randomRoomName, roomOptions, TypedLobby.Default);

        //デバック用にroomに入るようにする
        PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);

    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        //初期ネームが決定
        PhotonNetwork.LocalPlayer.NickName = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
    }


    //ランダムの部屋番号を返す
    public string randomRoomNameFunc() 
    { 
        return randomRoomName;
    }


    //部屋から退出
    public void LeaveRoomFunc() 
    {
        PhotonNetwork.LeaveRoom();

    }


    // ルーム名を指定して参加
    public void JointIdentyRoom(string roomName)
    {

        //デバック用にroomに入るようにする
        //PhotonNetwork.JoinRoom(roomName);
        PhotonNetwork.JoinRoom("room");
    }



    //コールバック色々


    // ルームへの参加が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "がルームへ参加しました");
        Debug.Log(randomRoomName);

        IsRoomIn = true;
    }


    public void RoomStateIndicate()
    {
        //部屋の人数を把握
        PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //オブジェクトに描写
        GameObject.Find("PlayerCount").GetComponent<Text>().text = PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;

        //プレイヤー名表示
        GameObject.Find("PlayerList").GetComponent<Text>().text = "";
        foreach (Player onePlayer in PhotonNetwork.PlayerList)
        {
            GameObject.Find("PlayerList").GetComponent<Text>().text += onePlayer.NickName + "\n";
        }

        //プレイヤーの数だけレディー表示をする
        for (int i = 0; i < PlayerCount; i++)
        {
            GameObject.Find("PlayerList").transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void InRoomPlayerCount() 
    {
        RoomStateIndicate();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + "が部屋に入りました");
        RoomStateIndicate();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "が部屋から出ました");
        RoomStateIndicate();
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
