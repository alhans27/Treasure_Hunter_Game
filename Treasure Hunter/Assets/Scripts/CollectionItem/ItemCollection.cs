using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public GameManager GM;

    private void Awake()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D TheThingThatWalkedIntoMe)
    {

        if (TheThingThatWalkedIntoMe.CompareTag("Player") && gameObject.CompareTag("Coin"))
        {
            GM.CoinCollection();
            Checkpoint.goName.Add(gameObject.name);
            gameObject.SetActive(false);
        }
        else if (TheThingThatWalkedIntoMe.CompareTag("Player") && gameObject.CompareTag("Diamond"))
        {
            GM.DiamondCollection();
            Checkpoint.goName.Add(gameObject.name);
            gameObject.SetActive(false);
        }
    }


}
