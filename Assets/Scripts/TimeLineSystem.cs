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
        //ソロプレイかどうかの判断
        bool IsSoloPlay = GameObject.Find("PhotonSystem").gameObject.GetComponent<PhotonSystem>().SoloPlay;

        if (!IsSoloPlay)
        {
            //マルチならrpc操作
            photonView.RPC(nameof(RPCAllStart), RpcTarget.AllViaServer);
        }
        else
        {
            CustomEvent.Trigger(this.gameObject, "AllStart");
        }        
    }

    [PunRPC]
    public void RPCAllStart() 
    {
        CustomEvent.Trigger(this.gameObject, "AllStart");
    }


    public void TimeLineSet(string RPCName) 
    {

        //ソロプレイかどうかの判断
        bool IsSoloPlay = GameObject.Find("PhotonSystem").gameObject.GetComponent<PhotonSystem>().SoloPlay;

        if (!IsSoloPlay)
        {
            //マルチならrpc操作
            photonView.RPC("RPCTimeLineSet", RpcTarget.AllViaServer, RPCName);
        }
        else
        {
            CustomEvent.Trigger(this.gameObject, RPCName);
        }        
    }

    [PunRPC]
    public void RPCTimeLineSet(string RPCName)
    {
        CustomEvent.Trigger(this.gameObject,RPCName);
    }


}
