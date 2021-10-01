using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Ludiq;
using Bolt;

public class TimeLineSystem : MonoBehaviourPunCallbacks
{    

    // Start is called before the first frame update
    void Start()
    {
        //ソロプレイかどうかの判断
        bool IsSoloPlay = GameObject.Find("PhotonSystem").gameObject.GetComponent<PhotonSystem>().SoloPlay;

        if (!IsSoloPlay)
        {
            //マルチならrpc操作
            Invoke("AllStart", 2.0f);
        }
        else 
        {
            CustomEvent.Trigger(this.gameObject, "AllStart");
        }       

    }

    public void AllStart()
    {
        photonView.RPC(nameof(RPCAllStart), RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void RPCAllStart() 
    {
        CustomEvent.Trigger(this.gameObject, "AllStart");
    }

}
