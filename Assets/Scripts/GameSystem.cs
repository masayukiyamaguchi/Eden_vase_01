using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;
using Photon.Pun;
using Photon.Realtime;

public class GameSystem : MonoBehaviourPunCallbacks
{

    public void GameObjectSecActive(string name)
    {
        photonView.RPC(nameof(RPCGameObjectSecActive), RpcTarget.All, name);
    }


    [PunRPC]
    public void RPCGameObjectSecActive(string name) 
    {
        GameObject.Find(name).gameObject.SetActive(false);
    }

}
