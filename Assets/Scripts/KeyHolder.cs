using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;
    private bool isNearKey = false;
    private Key currentKey = null;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    private void Update()
    {
        bool getE = Input.GetKeyUp(KeyCode.E);

        if (isNearKey && getE)
        {
            if (currentKey != null)
            {
                AddKey(currentKey.GetKeyType());
                Destroy(currentKey.gameObject);
                currentKey = null;
            }
        }
    }

    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keyList.Add(keyType);
    }

    public void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Key key = collision.GetComponent<Key>();

        //if (key != null)
        //{
        //    AddKey(key.GetKeyType());
        //    Destroy(key.gameObject);
        //}

        Key key = collision.GetComponent<Key>();

        if (key != null)
        {
            isNearKey = true;  // Marca que está perto de uma chave
            currentKey = key;  // Armazena a referência da chave
            Debug.Log("Clique 'E' para pegar a chave");
        }

        KeyDoor keyDoor = collision.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                RemoveKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Key key = collision.GetComponent<Key>();
        if(key != null)
        {
            isNearKey = false;
            currentKey = null;
        }
    }

}
