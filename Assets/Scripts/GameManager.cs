using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParrelSync;


public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.LocalPlayerInstance == null) 
        {
            PhotonNetwork.Instantiate("Prefabs/" + playerPrefab.name, playerSpawnerPosition.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
