using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.UIElements;
using System;
using Random = UnityEngine.Random;

public class CriarEConectar : MonoBehaviourPunCallbacks
{
    #region Variables

    [SerializeField] private TMP_InputField _nickname;
    [SerializeField] private TMP_InputField _roomID;
    private RoomOptions _options = new RoomOptions();

    #endregion

    #region Unity Methods

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _options.MaxPlayers = 2;
        _options.IsVisible = true;
        _options.IsOpen = true;
    }

    private void Update()
    {
        
    }

    #endregion

    #region Public Methods

    public string GeraCodigo()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string code = "";
        int digitCount = 6;
        for (int i = 0; i < digitCount; i++)
        {
            code += chars[Random.Range(0, chars.Length)];
        }
        Debug.Log(code);
        return code;
    }


    public void CriaSala() // cria a sala de jogo
    {
        string roomName = GeraCodigo();
        Debug.Log("Sala criada");
        PhotonNetwork.CreateRoom(roomName, _options);
        
    }

    public void JoinRoom()
    {
        if (_roomID.text == null)
        {
            return;
        }

        PhotonNetwork.JoinRoom(_roomID.text);
    }
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void MudaNome()
    {
        PhotonNetwork.LocalPlayer.NickName = _nickname.text;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);  
    }

    #endregion

    #region Photon Callbacks

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("LobbyGame");
    }

    #endregion
}
