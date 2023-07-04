using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int DiamondCollected = 0;
    public int CoinCollected = 0;
    public Text DiamondOutput;
    public Text CoinOutput;
    void Update()
    {
        DiamondOutput.text = "Diamonds: " + DiamondCollected;
        CoinOutput.text = "Coins: " + CoinCollected;
    }

    public void CoinCollection()
    {
        CoinCollected++;
    }
    public void DiamondCollection()
    {
        DiamondCollected++;
    }
    public void UsePotion()
    {
        DiamondCollected--;
    }

}

