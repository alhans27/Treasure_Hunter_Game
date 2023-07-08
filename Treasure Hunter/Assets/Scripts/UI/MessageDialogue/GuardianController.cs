using ChestInventory;
using ChestInventory.Model;
using TMPro;
using UnityEngine;
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

    public void IsTrueAnswer()
    {
        if (chestData.totalValue >= chestData.minValue && chestData.totalWeight <= chestData.maxWeight)
        {
            Debug.Log("Jawaban Benar");
        }
        else
        {
            Debug.Log("Jawaban Salah");
            chest.ResetData();
        }
    }

    public void ResetInventory()
    {
        chest.ResetData();
        questionMessage.HideMessage();
    }
}
