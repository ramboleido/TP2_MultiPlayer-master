using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class Carregamento : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text texto;
    // Start is called before the first frame update
    void Start()
    {
        // conecta no servidor Photon com as configurações predefinidas
        Debug.Log("Conectando....");
        texto.text = "Conectando....";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        texto.text = "Conectado ao servidor photon...";
        Debug.Log("Conectado ao servidor photon...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        texto.text = "Entrei no lobby do servidor photon";
        Debug.Log("Entrei no lobby do servidor photon");
        SceneManager.LoadScene("CreateGame");
    }
}
