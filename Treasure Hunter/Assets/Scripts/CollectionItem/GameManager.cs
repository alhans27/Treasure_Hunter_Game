using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int DiamondCollected = 0;

    public Text DiamondOutput;
    void Update()
    {
        DiamondOutput.text = "Diamonds: " + DiamondCollected;
    }

    public void ItemCollection()
    {
        DiamondCollected++;
    }
    public void UsePotion()
    {
        DiamondCollected--;
    }
}
