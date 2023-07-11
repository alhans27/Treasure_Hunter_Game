using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public Text CoinOutput;

    private void Start()
    {
        CoinOutput.text = GameManager.Instance.CoinCollected.ToString();
    }
    private void OnTriggerEnter2D(Collider2D TheThingThatWalkedIntoMe)
    {

        if (TheThingThatWalkedIntoMe.CompareTag("Player") && gameObject.CompareTag("Coin"))
        {
            GameManager.Instance.CoinCollection();
            CoinOutput.text = GameManager.Instance.CoinCollected.ToString();
            Checkpoint.goName.Add(gameObject.name);
            gameObject.SetActive(false);
        }
        // else if (TheThingThatWalkedIntoMe.CompareTag("Player") && gameObject.CompareTag("Diamond"))
        // {
        //     Checkpoint.goName.Add(gameObject.name);
        //     gameObject.SetActive(false);
        // }
    }


}
