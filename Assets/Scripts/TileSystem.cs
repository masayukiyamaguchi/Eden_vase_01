using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Ludiq;
using Bolt;

public class TileSystem : MonoBehaviourPunCallbacks
{

    public void TileSystemLineIn(string FncName,string TileName) 
    {


        //É\ÉçÉvÉåÉCÇ©Ç«Ç§Ç©ÇÃîªíf
        bool IsSoloPlay = GameObject.Find("PhotonSystem").gameObject.GetComponent<PhotonSystem>().SoloPlay;

        if (!IsSoloPlay)
        {
            photonView.RPC(FncName, RpcTarget.AllViaServer, FncName , TileName);
        }
        else
        {
            CustomEvent.Trigger(GameObject.Find(TileName), FncName);
        }

        
    }

    [PunRPC]
    public void RPCTileFall(string FncName, string TileName) 
    {
        CustomEvent.Trigger(GameObject.Find(TileName), FncName);
    }



}
