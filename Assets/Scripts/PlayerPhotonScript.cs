using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerPhotonScript : MonoBehaviourPunCallbacks
{
    //IsMind”»’è
    public bool Photon_IsMine()
    {
        if (photonView.IsMine)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
