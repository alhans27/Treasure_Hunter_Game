using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int CoinCollected = 0;
    public Text CoinOutput;
    private void Update()
    {
        CoinOutput.text = CoinCollected.ToString();
    }

    public void CoinCollection()
    {
        CoinCollected++;
    }
}

