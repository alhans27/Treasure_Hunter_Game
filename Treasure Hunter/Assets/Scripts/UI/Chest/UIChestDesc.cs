using UnityEngine;
using UnityEngine.UI;

namespace ChestInventory.UI
{
    public class UIChestDesc : MonoBehaviour
    {
        [SerializeField]
        private Text currentValue;

        [SerializeField]
        private Text minValue;

        [SerializeField]
        private Text currentWeight;

        [SerializeField]
        private Text maxWeight;

        public void Awake()
        {
            ResetDesc();

        }

        public void ResetDesc()
        {
            this.currentValue.text = "";
            this.minValue.text = "";
            this.currentWeight.text = "";
            this.maxWeight.text = "";
        }

        public void SetDesc(string minValue, string currentValue, string maxWeight, string currentWeight)
        {
            this.minValue.text = minValue;
            this.currentValue.text = currentValue;
            this.maxWeight.text = maxWeight;
            this.currentWeight.text = currentWeight;
        }
    }
}
