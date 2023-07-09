using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public GameManager GM;
    private void OnTriggerEnter2D(Collider2D TheThingThatWalkedIntoMe)
    {

        if (TheThingThatWalkedIntoMe.CompareTag("Player") && gameObject.CompareTag("Coin"))
        {

            GM.CoinCollection();
            Destroy(gameObject);

        }
        else if (TheThingThatWalkedIntoMe.CompareTag("Player") && gameObject.CompareTag("Diamond"))
        {
            GM.DiamondCollection();
            Destroy(gameObject);
        }
    }


}
