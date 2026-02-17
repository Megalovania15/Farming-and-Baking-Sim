using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [field: SerializeField] public Image ItemIcon { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemText { get; private set; }

    public ItemInstance StoredItem { get; private set; }

    private int quantity = 0;

    public void StoreItemInSlot(ItemInstance itemToStore)
    {
        StoredItem = itemToStore;
    }

    public void UpdateItemIcon(Sprite newSprite)
    {
        ItemIcon.enabled = true;
        ItemIcon.sprite = newSprite;
    }

    public void UpdateItemIcon()
    {
        ItemIcon.enabled = false;
        ItemIcon.sprite = null;
    }

    public void UpdateText()
    {
        if (StoredItem != null)
        {
            if (StoredItem.MaxStack <= 1)
            {
                ItemText.enabled = false;
            }
            else
            {
                ItemText.enabled = true;
                ItemText.text = quantity.ToString();
            }
        }
    }

    public void RemoveStoredItem()
    {
        StoredItem = null;
    }

    public void AddToQuantity(int addedItems)
    {
        quantity += addedItems;
    }
}
