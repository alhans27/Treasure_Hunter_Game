using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    [SerializeField]
    private bool EnableSticky = true;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (EnableSticky)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.transform.SetParent(transform);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (EnableSticky)
        {
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.transform.SetParent(null);
            }
        }
    }
}
