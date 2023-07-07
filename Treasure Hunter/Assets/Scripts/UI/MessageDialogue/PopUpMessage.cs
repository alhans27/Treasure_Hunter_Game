using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowMessage()
    {
        gameObject.SetActive(true);
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
    }
}
