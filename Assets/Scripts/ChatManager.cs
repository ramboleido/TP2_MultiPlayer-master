using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{

    #region Private Var
    
    [SerializeField] private TMP_InputField _inputMessage;
    [SerializeField] private GameObject _content;
    [SerializeField] private GameObject _message;
    private PhotonView _photonView;

    #endregion

    #region Unity Methods
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }
    
    
    #endregion

    #region Public Methods

    public void SendMessage()
    {
        _photonView.RPC("ReceiveMessage", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName + ": " + _inputMessage.text);

        _inputMessage.text = "";

    }

    [PunRPC]
    public void ReceiveMessage(string messageReceived)
    {
        GameObject message = Instantiate(_message, _content.transform);
        
        message.GetComponent<TMP_Text>().text = messageReceived;
        
        message.GetComponent<RectTransform>().SetAsLastSibling();
        
    }
    
    #endregion
    
}
