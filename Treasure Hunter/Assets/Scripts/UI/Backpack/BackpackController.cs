using UnityEngine;

public class BackpackController : MonoBehaviour
{
    [SerializeField]
    private BackpackInventory inventoryUI;

    [SerializeField]
    private int inventorySize = 5;

    public void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }
    public void Update()
    {
        if (Input.GetButtonDown("Backpack"))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
