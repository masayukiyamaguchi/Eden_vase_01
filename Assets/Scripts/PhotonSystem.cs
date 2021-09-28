using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;


public class PhotonSystem : MonoBehaviourPunCallbacks
{
    //������
    public string randomRoomName;

    public bool IsRoomIn;
    public bool SoloPlay;

    public int inRoomPlayerCount;
    public int RoomMaxPlayers;

    //�V�[���؂�ւ��Ŕj�󂳂�Ȃ��悤�ɂ���
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
        PhotonNetwork.JoinOrCreateRoom(randomRoomName, roomOptions, TypedLobby.Default);

    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {

    }


    //�����_���̕����ԍ���Ԃ�
    public string randomRoomNameFunc() 
    { 
        return randomRoomName;
    }

    //���[���̍ő�Q���l����Ԃ�
    public string RoomMaxPlayersFunc()
    {
        //Debug.Log(RoomInfo.MaxPlayers());�@���̂悤�ɏ����Ă݂������߂������B
        return null;
    }

    //���[���̌��݂̎Q���l����Ԃ�
    public string RoomPlayerCountFunc()
    {
        return null;
    }

    //��������ޏo
    public void LeaveRoomFunc() 
    {
        PhotonNetwork.LeaveRoom();

    }


    // ���[�������w�肵�ĎQ��
    public void JointIdentyRoom(string roomName)
    {

        PhotonNetwork.JoinRoom(roomName);
    }



    //�R�[���o�b�N�F�X


    // ���[���ւ̎Q���������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���֎Q�����܂���");
        Debug.Log(randomRoomName);
        IsRoomIn = true;
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
