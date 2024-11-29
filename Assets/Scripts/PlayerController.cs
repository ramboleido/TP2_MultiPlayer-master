using System;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Tilemaps;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    
    #region Variables
    public static GameObject LocalPlayerInstance;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private TMP_Text _namePlayer;

    private bool isGrounded = false;

    private Vector2 _networkingPosition;
    private Rigidbody2D _rb;
    private string _nickName;
    private Vector2 _playerMovement;
    
    
    #endregion
    
    #region Lists
    public List<TypeButton> listButtons = new List<TypeButton>(); 
    #endregion

    #region Unity Methods
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (photonView.IsMine)
        {
            if (LocalPlayerInstance == null)
            {
                LocalPlayerInstance = this.gameObject;

            }
            _nickName = PhotonNetwork.NickName;
            _namePlayer.text = _nickName;
        }
        else
        {
            _namePlayer.text = _nickName;
        }

    }

    public void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // local
            _rb.velocity = _playerMovement;

        }
        else
        {
            //network
            transform.position = Vector2.Lerp(transform.position, _networkingPosition, Time.deltaTime * 10);
        }
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            float moveH = Input.GetAxisRaw("Horizontal");
            float moveV = Input.GetAxisRaw("Vertical");
            _playerMovement = new Vector2(moveH * _moveSpeed, _rb.velocity.y);

            bool jump = Input.GetButtonDown("Jump");

            if (jump && isGrounded)
            {
                Pular();
            }
        }

    }
    #endregion

    #region 2D Methods

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject go = collision.gameObject;
        
        if (go.CompareTag("Ground"))
        {
            isGrounded = true;
            //Debug.Log("chao");
        }

        if (go.CompareTag("Door"))
        {
            Debug.Log("Precisa apertar os botões para abrir a porta");
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;

        if (Input.GetKey(KeyCode.E) && go.CompareTag("Door"))
        {
            go.SetActive(false);
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        
        GameObject go = collision.gameObject;
        
        if (go.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        TypeButton buttonComponent = go.GetComponent<Buttons>().type;

        if (go.CompareTag("Button"))
        {
            Debug.Log($"Tipo do botão: {buttonComponent}");
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        TypeButton buttonComponent = go.GetComponent<Buttons>().type;

        if (Input.GetKey(KeyCode.E) && go.CompareTag("Button"))
        {
            listButtons.Add(buttonComponent);
            go.SetActive(false);
        }
    }

    private void Pular()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
    }

    #endregion

    #region Photon callbacks

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            // Enviar dados
            stream.SendNext((Vector2)transform.position);
            stream.SendNext(_nickName);
        }
        else
        {
            //Receber dados
            _networkingPosition = (Vector2)stream.ReceiveNext();
            _nickName = (string)stream.ReceiveNext();


            if (photonView.IsMine)
            {
                _nickName = PhotonNetwork.LocalPlayer.NickName;
                _namePlayer.text = _nickName;
            }
            else
            {
                _namePlayer.text = _nickName;
            }
        }


    }


    #endregion
    

}
