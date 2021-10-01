using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Ludiq;
using Bolt;

public class ButtonClickGameStart : MonoBehaviourPunCallbacks
{


    public void ButtonClickGameStartFunc()
    {
        photonView.RPC(nameof(LoadScene), RpcTarget.AllViaServer);
    }


    [PunRPC]
    public void LoadScene() 
    {
        CustomEvent.Trigger(this.gameObject, "ButtonClick");
    }

}
