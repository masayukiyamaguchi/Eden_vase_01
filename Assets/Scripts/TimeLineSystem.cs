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
        //�\���v���C���ǂ����̔��f
        bool IsSoloPlay = GameObject.Find("PhotonSystem").gameObject.GetComponent<PhotonSystem>().SoloPlay;

        if (!IsSoloPlay)
        {
            //�}���`�Ȃ�rpc����
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
