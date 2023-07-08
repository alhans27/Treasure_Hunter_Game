using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChestInventory.UI
{
    public class UIChestItem : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private Image borderImage;

        [SerializeField]
        private Text quantityTxt;

        public event Action<UIChestItem> onItemDroppedOn;

        public void Awake()
        {
            ResetItemUI();
            Deselect();
        }

        public void OnDrop(PointerEventData eventData)
        {
            // Menjalankan sebuah Action
            onItemDroppedOn?.Invoke(this);
        }

        internal void ResetItemUI()
        {
            this.itemImage.gameObject.SetActive(false);
        }
        internal void Deselect()
        {
            borderImage.enabled = false;
        }


        internal void SetItemUI(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityTxt.text = quantity + "";
        }
    }
}
