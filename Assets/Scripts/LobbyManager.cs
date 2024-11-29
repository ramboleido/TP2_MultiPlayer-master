using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region Variables
    private int currentCharacterIndex = 0;
    [SerializeField] private List<Sprite> characterSprites;
    [SerializeField] private List<GameObject> _playersPanels;
    [SerializeField] private TMP_Text _textPlayerCount;
    private int _playersCount;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        
        ChecaJogadores();
        AtualizarSpritesNosPaineis();
    }

    private void Update()
    {
        ChecaJogadores();
    }
    #endregion

    #region Private Methods
    private void ChecaJogadores()
    {
        _playersCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Player[] playersList = PhotonNetwork.PlayerList;

        if( _playersCount <= 0)
        {
            return;
        }

        _textPlayerCount.text = "Jogadores na sala: " + _playersCount.ToString();

        for(int i = 0; i < _playersCount; i++)
        {
            _playersPanels[i].SetActive(true);
            _playersPanels[i].GetComponentInChildren<TMP_Text>().text = playersList[i].NickName;
        }
        
    }

    private void AtualizarSpritesNosPaineis()
    {
        for (int i = 0; i < _playersPanels.Count; i++)
        {
            if (i < characterSprites.Count)
            {
                Image spriteImage = _playersPanels[i].transform.Find("CharacterSprite").GetComponent<Image>();
                spriteImage.sprite = characterSprites[i];
            }
        }
    }
    #endregion

    #region Public Methods
    
    public void RotateCharacter()
    {
        // Alterna entre 0 e 1
        currentCharacterIndex = (currentCharacterIndex + 1) % characterSprites.Count;
        Debug.Log("Personagem selecionado: " + currentCharacterIndex);
        SelectCharacter(currentCharacterIndex);
    }
    public void StartGame()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public void SelectCharacter(int characterIndex)
    {
        if (PhotonNetwork.LocalPlayer != null)
        {
            // Salva a escolha de personagem nos CustomProperties
            PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
            { 
                { "CharacterIndex", characterIndex } 
            });
            Debug.Log($"Personagem {characterIndex} selecionado.");
        }
    }
    
    #endregion
}
