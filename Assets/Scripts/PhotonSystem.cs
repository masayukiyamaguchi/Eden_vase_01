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
                    Debug.LogError(t + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                }
            }

            return instance;
        }
    }

    void Awake()
    {
        // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        // �A�^�b�`����Ă���ꍇ�͔j������B
        CheckInstance();
    }

    protected bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this as PhotonSystem;

            //�V�[�����؂�ւ���Ă��I�u�W�F�N�g���󂳂Ȃ�
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


    //������
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
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();


    }


    public void ReaveMasterServer()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.Disconnect();
    }


    // ���[���𐶐�
    public void CreateRoom(){

        //����������
        randomRoomName = Guid.NewGuid().ToString("N").Substring(0, 6);
        Debug.Log("RandomCreated");

        // �쐬���郋�[���̃��[���ݒ���s��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;

        // ���[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        //PhotonNetwork.JoinOrCreateRoom(randomRoomName, roomOptions, TypedLobby.Default);

        //�f�o�b�N�p��room�ɓ���悤�ɂ���
        PhotonNetwork.JoinOrCreateRoom("room", roomOptions, TypedLobby.Default);

    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        //�����l�[��������
        PhotonNetwork.LocalPlayer.NickName = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
    }


    //�����_���̕����ԍ���Ԃ�
    public string randomRoomNameFunc() 
    { 
        return randomRoomName;
    }


    //��������ޏo
    public void LeaveRoomFunc() 
    {
        PhotonNetwork.LeaveRoom();

    }


    // ���[�������w�肵�ĎQ��
    public void JointIdentyRoom(string roomName)
    {

        //�f�o�b�N�p��room�ɓ���悤�ɂ���
        //PhotonNetwork.JoinRoom(roomName);
        PhotonNetwork.JoinRoom("room");
    }



    //�R�[���o�b�N�F�X


    // ���[���ւ̎Q���������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + "�����[���֎Q�����܂���");
        Debug.Log(randomRoomName);

        IsRoomIn = true;
    }


    public void RoomStateIndicate()
    {
        //�����̐l����c��
        PlayerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        //�I�u�W�F�N�g�ɕ`��
        GameObject.Find("PlayerCount").GetComponent<Text>().text = PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;

        //�v���C���[���\��
        GameObject.Find("PlayerList").GetComponent<Text>().text = "";
        foreach (Player onePlayer in PhotonNetwork.PlayerList)
        {
            GameObject.Find("PlayerList").GetComponent<Text>().text += onePlayer.NickName + "\n";
        }

        //�v���C���[�̐��������f�B�[�\��������
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
        Debug.Log(newPlayer.NickName + "�������ɓ���܂���");
        RoomStateIndicate();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "����������o�܂���");
        RoomStateIndicate();
    }

    // ���[�������w�肵�����[���ւ̎Q�������s�������ɌĂ΂��R�[���o�b�N
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"���[���ւ̎Q���Ɏ��s���܂���: {message}");
        IsRoomIn = false;
    }

    // ���[������ޏo�������ɌĂ΂��R�[���o�b�N
    public override void OnLeftRoom()
    {
        Debug.Log("���[������ޏo���܂���");
        IsRoomIn = false;
    }




    //�v���C���[�X�|�[��
    public GameObject ActivePlayerSporn(string ActivePlayerName) 
    {
        var position = new Vector3(0.0f,3.7f, -3.24f);
        GameObject playerGameobject = PhotonNetwork.Instantiate(ActivePlayerName, position, Quaternion.identity);
        return playerGameobject;

    }





}
