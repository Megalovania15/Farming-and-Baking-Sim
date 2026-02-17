using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [field: SerializeField] public Image ItemIcon { get; set; }

    private ItemInstance storedItem;

    public void UpdateItemIcon(Sprite newSprite, ItemInstance itemToStore)
    { 
        ItemIcon.sprite = newSprite;
        storedItem = itemToStore;
    }
}
