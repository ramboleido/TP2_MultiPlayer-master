using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class Carregamento : MonoBehaviourPunCallbacks
{
    #region Variables
    [SerializeField] private TMP_Text _texto;
    #endregion

    #region Unity Methods
    void Start()
    {
        // conecta no servidor Photon com as configura��es predefinidas
        Debug.Log("Conectando....");
        _texto.text = "Conectando....";
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        _texto.text = "Conectado ao servidor photon...";
        Debug.Log("Conectado ao servidor photon...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _texto.text = "Entrei no lobby do servidor photon";
        Debug.Log("Entrei no lobby do servidor photon");
        SceneManager.LoadScene("CreateGame");
    }
    #endregion
}
