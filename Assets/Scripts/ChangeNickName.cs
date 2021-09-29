using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ChangeNickName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //ニックネームを変更
    public void PlayerNickNameChange()
    {
        string name = GameObject.Find("PlayerNameText").GetComponent<Text>().text;
        PhotonNetwork.LocalPlayer.NickName = name;
    }

}
