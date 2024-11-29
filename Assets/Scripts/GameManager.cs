using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParrelSync;
using Unity.VisualScripting;


public class GameManager : MonoBehaviourPunCallbacks
{
    #region Variables

    public static GameManager Instance;
    [SerializeField] GameObject[] playerPrefabs;
    [SerializeField] Transform[] playerSpawnerPositions;

    #endregion
    

    #region Unity Methods
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
            // Recupera o índice do personagem selecionado
            int characterIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["CharacterIndex"];
            Debug.Log("CharacterIndex: " + characterIndex);
        
            // Determina o ponto de spawn baseado no ID do jogador
            int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
            Vector3 spawnPosition = playerSpawnerPositions[spawnIndex].position;
        
            // Instancia o personagem correto
            PhotonNetwork.Instantiate("Prefabs/" + playerPrefabs[characterIndex].name, spawnPosition, Quaternion.identity);
        }
    }
    #endregion
    
}
