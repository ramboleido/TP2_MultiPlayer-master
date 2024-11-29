using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Button : MonoBehaviourPunCallbacks
{
    public enum ButtonType { Button1, Button2 }
    [SerializeField] private ButtonType buttonType;

    public ButtonType GetButtonType()
    {
        return buttonType;
    }
}
