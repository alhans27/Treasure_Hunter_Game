using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private Image borderImage;

        [SerializeField]
        private Text quantityTxt;

        public event Action<UIInventoryItem> onItemClicked, onItemRightMouseClicked, onItemDroppedOn, onItemBeginDrag, onItemEndDrag;

        private bool empty = true;


        public void Awake()
        {
            ResetItemUI();
            Deselect();
        }

        // Menonaktifkan UI Slot Item Bergambar dan Hanya Menampilkan UI Slot Item Kosong
        public void ResetItemUI()
        {
            if (empty != true)
            {
                this.itemImage.gameObject.SetActive(false);
                empty = true;
            }
        }

        // Menghilangkan Border Item yang sebelumnya diklik oleh User melalui mouse
        public void Deselect()
        {
            borderImage.enabled = false;
        }

        // Menampilkan UI Slot Item Bergambar
        public void SetItemUI(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityTxt.text = quantity + "";
            empty = false;
        }

        // Menampilkan Border UI Item Bergambar yang di pilih
        public void Select()
        {
            borderImage.enabled = true;
        }

        // Event Handling ketika User Mengklik Slot Item
        public void OnPointerClick(PointerEventData pointerData)
        {
            // Jika User menekan Right Click pada Mouse
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                // Menjalankan sebuah Action
                onItemRightMouseClicked?.Invoke(this);
            }
            // Jika User menekan Left Click pada Mouse
            else
            {
                // Menjalankan sebuah Action
                onItemClicked?.Invoke(this);
            }
        }

        // Event Handling ketika User Mulai Men-Drag Sebuah Item
        public void OnBeginDrag(PointerEventData eventData)
        {
            // Jika Slot Item Kosong, maka tidak akan terjadi apa-apa
            if (empty)
                return;
            // Jika Slot Item Bergambar, maka akan menjalankan sebuah Action
            onItemBeginDrag?.Invoke(this);
        }

        // Event Handling ketika User Hampir Selesai Men-Drag Sebuah Item
        public void OnEndDrag(PointerEventData eventData)
        {
            // Menjalankan sebuah Action
            onItemEndDrag?.Invoke(this);
        }

        // Event Handling ketika User Men-Drop Sebuah Item
        public void OnDrop(PointerEventData eventData)
        {
            // Menjalankan sebuah Action
            onItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Kosong karena ini hanya syarat untuk menjalankan Event Handling BeginDrag and EndDrag
        }
    }
}
