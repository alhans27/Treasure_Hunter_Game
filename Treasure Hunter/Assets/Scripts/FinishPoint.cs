using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    [SerializeField]
    private InventorySO backpackData;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && this.gameObject.CompareTag("QuestionGate"))
        {
            Debug.Log(backpackData.GetAllItems());
            GameManager.Instance.SetBackpack(backpackData.GetAllItems());
            SceneManager.LoadScene("GuardianGate");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (coll.gameObject.CompareTag("Player") && this.gameObject.CompareTag("NextLevel"))
        {
            // SceneManager.LoadScene("GuardianGate");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
