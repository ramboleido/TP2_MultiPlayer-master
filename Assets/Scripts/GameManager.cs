using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParrelSync;
using Unity.VisualScripting;


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerSpawnerPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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
