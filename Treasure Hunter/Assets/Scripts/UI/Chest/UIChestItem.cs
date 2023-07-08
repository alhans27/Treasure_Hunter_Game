using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

// namespace Inventory.UI
// {
public class UIChestItem : MonoBehaviour, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Text quantityTxt;

    public event Action<UIChestItem> onItemDroppedOn, onItemEndDrag;

    private bool empty = true;


    public void Awake()
    {
        ResetData();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;

    }
    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        onItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        onItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
// }
