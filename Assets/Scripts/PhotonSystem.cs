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

    //�V�[���؂�ւ��Ŕj�󂳂�Ȃ��悤�ɂ���
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {

    }


    // ���[�������w�肵�ĎQ��
    public void CreateRoom()
    {
        //����������
        randomRoomName = Guid.NewGuid().ToString("N").Substring(0, 6);

        // �쐬���郋�[���̃��[���ݒ���s��
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;

        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom(randomRoomName, roomOptions, TypedLobby.Default);
    }


    public string randomRoomNameFunc() 
    { 
        return randomRoomName;
    }


    // ���[�������w�肵�ĎQ��
    public void JointIdentyRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }  



    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
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
