using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonDoor : MonoBehaviourPunCallbacks
{
    public Button.ButtonType requiredButton1;  // O primeiro botão necessário
    public Button.ButtonType requiredButton2;  // O segundo botão necessário

    private bool hasButton1 = false;  // Marca se o botão 1 foi pressionado
    private bool hasButton2 = false;  // Marca se o botão 2 foi pressionado
    private bool isOpened = false;    // Marca se a porta já foi aberta

    // Tenta abrir a porta com base no tipo do botão pressionado
    public void TryOpenDoor(Button.ButtonType buttonType)
    {
        // Verifica se a porta já foi aberta
        if (isOpened) return;  // Se a porta já foi aberta, não faz nada

        if (buttonType == requiredButton1)
        {
            hasButton1 = true;
        }
        else if (buttonType == requiredButton2)
        {
            hasButton2 = true;
        }

        // Se ambos os botões foram pressionados, a porta é aberta
        if (hasButton1 && hasButton2)
        {
            OpenDoor();
        }
    }

    // Método para abrir a porta
    private void OpenDoor()
    {
        Debug.Log("A porta foi aberta!");
        isOpened = true;  // Marca a porta como aberta
        gameObject.SetActive(false);  // Desativa a porta (simulando a abertura)
    }
}
