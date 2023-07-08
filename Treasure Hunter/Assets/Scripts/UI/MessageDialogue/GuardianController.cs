using System.Collections;
using System.Collections.Generic;
using ChestInventory;
using UnityEngine;

public class GuardianController : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private PopUpMessage message;

    [SerializeField]
    private ChestController chest;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            chest.Show();
            message.ShowMessage();
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            message.HideMessage();
        }
    }
}
