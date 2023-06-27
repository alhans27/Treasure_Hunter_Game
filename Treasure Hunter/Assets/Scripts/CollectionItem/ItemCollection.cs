using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public GameManager GM;
    private void OnTriggerEnter2D(Collider2D TheThingThatWalkedIntoMe)
    {
        if(TheThingThatWalkedIntoMe.name == "Player")
        {
            Debug.Log ("You got the diamond");
            GM.ItemCollection();
            Destroy(gameObject);
        }
    }
}
