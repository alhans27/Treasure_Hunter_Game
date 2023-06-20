using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    int diamonds;

    [SerializeField] private Text diamondsText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            diamonds++;
            diamondsText.text = "Diamonds: " + diamonds;
            Destroy(gameObject);
        }
    }
}
