using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KeyDoor : MonoBehaviourPunCallbacks
{
    [SerializeField] private Key.KeyType keyType;
    private bool isOpen = false;

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    // Verifica se a porta está aberta
    public bool IsOpen()
    {
        return isOpen;
    }

    public void OpenDoor()
    {
        if (isOpen) return; 

        if (photonView.IsMine)  
        {
            photonView.RPC("OpenDoorRPC", RpcTarget.All); 
        }
    }

    [PunRPC]  // Esse método será chamado por todos os clientes
    public void OpenDoorRPC()
    {
        isOpen = true;
        gameObject.SetActive(false);  
        Debug.Log("Door opened for all players.");
    }

}
