using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int DiamondCollected = 0;
    public int CoinCollected { get; set; }
    public Text DiamondOutput;
    public Text CoinOutput;
    private void Awake()
    {
        DiamondOutput.text = "Diamonds: " + DiamondCollected;
        CoinOutput.text = CoinCollected.ToString();
    }

    public void CoinCollection()
    {
        CoinCollected++;
        CoinOutput.text = CoinCollected.ToString();
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

