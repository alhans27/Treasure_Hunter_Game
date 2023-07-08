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

        [SerializeField]
        private Text valueTxt;

        [SerializeField]
        private Text weightTxt;

        public void Awake()
        {
            ResetDesc();

        }

        public void ResetDesc()
        {
            this.itemImage.gameObject.SetActive(false);
            this.titleTxt.text = "";
            this.descTxt.text = "";
            this.valueTxt.text = "";
            this.weightTxt.text = "";
        }

        public void SetDesc(Sprite sprite, string itemName, string itemDesc, int valueItem, int weightItem)
        {
            string valueTxt = "$ " + valueItem.ToString();
            string weightTxt = weightItem.ToString() + " gr";

            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.titleTxt.text = itemName;
            this.descTxt.text = itemDesc;
            this.valueTxt.text = valueTxt;
            this.weightTxt.text = weightTxt;
        }
    }
}
