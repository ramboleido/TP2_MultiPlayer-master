using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonHolder : MonoBehaviourPunCallbacks
{
    private Button currentButton = null;

    private void Update()
    {
        if (currentButton != null && Input.GetKeyDown(KeyCode.E))
        {
            // Quando o jogador pressiona a tecla 'E', ativa o botão
            Debug.Log("Botão pressionado: " + currentButton.GetButtonType());

            // Encontra todas as portas na cena e notifica qual botão foi pressionado
            ButtonDoor[] doors = FindObjectsOfType<ButtonDoor>();
            foreach (var door in doors)
            {
                door.TryOpenDoor(currentButton.GetButtonType());
            }

            // Desativa o botão depois de ser pressionado (simula que ele foi "usado")
            currentButton.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Button button = collision.GetComponent<Button>();

        if (button != null)
        {
            // Marca que o jogador está perto de um botão
            currentButton = button;
            Debug.Log("Pressione 'E' para ativar o botão: " + button.GetButtonType());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Button button = collision.GetComponent<Button>();
        if (button != null)
        {
            // Quando o jogador sai da área do botão, ele deixa de interagir com ele
            currentButton = null;
        }
    }
}