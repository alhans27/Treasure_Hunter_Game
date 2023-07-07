using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryDescItem : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private Text titleTxt;

        [SerializeField]
        private Text descTxt;

        public void Awake()
        {
            ResetDesc();

        }

        public void ResetDesc()
        {
            this.itemImage.gameObject.SetActive(false);
            this.titleTxt.text = "";
            this.descTxt.text = "";
        }

        public void SetDesc(Sprite sprite, string itemName, string itemDesc)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.titleTxt.text = itemName;
            this.descTxt.text = itemDesc;
        }
    }
}
