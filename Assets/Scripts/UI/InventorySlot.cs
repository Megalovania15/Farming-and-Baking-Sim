using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventorySlot : MonoBehaviour
{
    [field: SerializeField] public Image ItemIcon { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ItemText { get; private set; }

    public int Quantity { get; private set; } = 0;

    public ItemInstance StoredItem { get; private set; }

    public UnityEvent<InventorySlot> onItemClicked, onRightMouseClicked = new();

    

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
        Debug.Log("Item icon updated");
        ItemIcon.enabled = false;
        ItemIcon.sprite = null;
    }

    public void UpdateText()
    {
        if (StoredItem is not null)
        {
            if (StoredItem.MaxStack <= 2)
            {
                ItemText.enabled = false;
            }
            else
            {
                ItemText.enabled = true;
                ItemText.text = Quantity.ToString();
            }
        }
        else 
        {
            ItemText.enabled = false;
        }
    }

    public void RemoveStoredItem()
    {
        StoredItem = null;
    }

    public void AddToQuantity(int addedItems)
    {
        Quantity += addedItems;
    }

    public void RemoveFromQuantity(int itemsToRemove)
    {
        Quantity -= itemsToRemove;
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            onRightMouseClicked.Invoke(this);
        }
        else 
        {
            onItemClicked.Invoke(this);
        }
    }

}
