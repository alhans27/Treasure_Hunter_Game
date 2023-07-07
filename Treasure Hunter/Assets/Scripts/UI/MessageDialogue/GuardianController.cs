using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianController : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private PopUpMessage message;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
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
