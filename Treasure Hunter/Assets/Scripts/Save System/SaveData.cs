using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int DiamondCollected;
    public int CoinCollected;

    public SaveData(GameManager items)
    {
        DiamondCollected = items.DiamondCollected;
        CoinCollected = items.CoinCollected;
    }
}
