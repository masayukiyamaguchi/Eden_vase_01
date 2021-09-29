using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ReadyCheckButton : MonoBehaviourPunCallbacks
{
    public int ReadyCheckNum = 0;
    public bool IsReadyCheck = false;
        
    public void ReadyCheckButtonClickFunk() 
    {
        //���X�g�̉��Ԗڂ���c��
        string myNickName =  PhotonNetwork.LocalPlayer.NickName;
        int countNum = 0;

        foreach (Player onePlayer in PhotonNetwork.PlayerList) 
        {
            if (myNickName == onePlayer.NickName) 
            {
                break;
            }
            countNum++;
        }

        if (!IsReadyCheck)
        {
            photonView.RPC(nameof(RpcReadyCheck), RpcTarget.AllBuffered, countNum);
            IsReadyCheck = true;
        }
        else 
        {
            photonView.RPC(nameof(RpcReadyCheckCancel), RpcTarget.AllBuffered, countNum);
            IsReadyCheck = false;
        }
        

    }


    [PunRPC]
    private void RpcReadyCheck(int countNum) 
    {
        //���e�B�`�F�b�N�I�u�W�F�N�g�̕\���ƐF��ύX
        GameObject ReadyChekObject = GameObject.Find("PlayerList").transform.GetChild(countNum).gameObject;
        ReadyChekObject.GetComponent<Text>().text = "��������";
        ReadyChekObject.GetComponent<Text>().color = Color.green;

        ReadyCheckNum++;

        if (PhotonNetwork.LocalPlayer.IsMasterClient && ReadyCheckNum >= PhotonNetwork.CurrentRoom.PlayerCount) 
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
        }
        else
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        }

    }

    [PunRPC]
    private void RpcReadyCheckCancel(int countNum)
    {
        //���e�B�`�F�b�N�I�u�W�F�N�g�̕\���ƐF��ύX
        GameObject ReadyChekObject = GameObject.Find("PlayerList").transform.GetChild(countNum).gameObject;
        ReadyChekObject.GetComponent<Text>().text = "������";
        ReadyChekObject.GetComponent<Text>().color = Color.red;

        ReadyCheckNum--;

        if (PhotonNetwork.LocalPlayer.IsMasterClient && ReadyCheckNum >= PhotonNetwork.CurrentRoom.PlayerCount)
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
        }
        else 
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        }

    }


}
