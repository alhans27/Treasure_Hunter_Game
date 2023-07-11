using System;
using System.Collections.Generic;
using ChestInventory;
using ChestInventory.Model;
using Inventory.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuardianController : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private PopUpMessage taskMessage;

    [SerializeField]
    private PopUpMessage questionMessage;

    [SerializeField]
    private TextMeshProUGUI btnResetTxt;
    [SerializeField]
    private TextMeshProUGUI btnSubmitTxt;

    [SerializeField]
    private ChestController chest;

    [SerializeField]
    private ChestInventorySO chestData;

    private Animator anim;

    // private bool IsAttack = false;

    [SerializeField]
    private float damageForce = 1;

    [SerializeField]
    private Health playerHealth;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && IsChestInventoryFilled())
        {
            btnResetTxt.text = "No, I Don't";
            btnSubmitTxt.text = "Yes, I'm Sure!";
            questionMessage.ShowMessage();
        }
        else
        {
            chest.Show();
            taskMessage.ShowMessage();
        }
    }

    private bool IsChestInventoryFilled()
    {
        if (chestData.GetDataLength() > 0 && chestData.GetDataLength() <= chestData.Size)
            return true;
        return false;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            questionMessage.HideMessage();
            taskMessage.HideMessage();
        }
    }

    public void CheckTheAnswer()
    {
        if (chestData.totalValue >= chestData.minValue && chestData.totalWeight <= chestData.maxWeight)
        {
            if (KnapsackAlgoritm.Instance.resultmaxValue == chestData.totalValue)             //&& IsCorrectItem(chestData.GetCurrentInventoryState())
            {
                // taskMessage.UpdateMessage("Congratulation !");
                anim.SetTrigger("Gone");
                GameManager.Instance.ResetCoin();
            }
        }
        else
        {
            chest.ResetData();
            anim.SetTrigger("Attack");
            playerHealth.TakeDamage(damageForce);
            questionMessage.HideMessage();
            if (playerHealth.currentHealth <= 0)
                SceneManager.LoadScene("GameOver");
        }
    }

    // private bool IsCorrectItem(Dictionary<int, ItemInventory> listItem)
    // {
    //     bool result = false;
    //     Debug.Log(gameObject.GetComponent<KnapsackAlgoritm>().GetResultItem());
    //     // foreach (var item in listItem)
    //     // {
    //     //     for (int i = 0; i < items.Count; i++)
    //     //     {
    //     //         if (item.Value.item.ID == items[i])
    //     //         {
    //     //             result = true;
    //     //         }
    //     //         result = false;
    //     //     }
    //     // }
    //     return result;
    // }

    public void ResetInventory()
    {
        chest.ResetData();
        questionMessage.HideMessage();
    }

    private void DestroyObj()
    {
        chest.DisableColl();
        questionMessage.HideMessage();
        this.gameObject.SetActive(false);
    }
}
