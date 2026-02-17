using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IStorage
{
    [SerializeField]
    private int maxSize = 10;

    [SerializeField]
    private List<ItemInstance> items = new();

    [SerializeField] private GameObject inventorySlots;

    private InventorySlot[] itemSlots;

    void Awake()
    {
        itemSlots = new InventorySlot[inventorySlots.transform.childCount];

        for (int i = 0; i < inventorySlots.transform.childCount; i++)
        {
            //need to store each of them in the array
            itemSlots[i] = inventorySlots.transform.GetChild(i).
                gameObject.GetComponent<InventorySlot>();
        }

        foreach (var itemSlot in itemSlots)
        {
            itemSlot.ItemIcon.enabled = false;
        }
    }

    public bool AddItem(ItemInstance item)
    {
        //need to check for an available slot
        for (var i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                Debug.Log($"A slot is available");
                return true;
            }
        }

        //adds an item if there is space
        if (items.Count < maxSize)
        {
            items.Add(item);
            UpdateItemDisplay(item);
            Debug.Log($"Added {item.Name}");
            return true;
        }
        //what happens if there isn't an available spot
        Debug.Log("There is no space in the inventory.");
        return false;
    }

    public bool DropItem(ItemInstance item)
    {
        if (items.Contains(item))
        {
            CreatePhysicalItem(item);
            items.Remove(item);
            return true;
        }
        Debug.Log("The item is no longer in the inventory");
        return false;
    }

    void UpdateItemDisplay(ItemInstance itemToUpdate)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            //we need to run through all the slots, and update them with an item
            //otherwise disable the remainder images and set the sprite on them to null.
            if (itemSlots[i].ItemIcon.sprite == null)
            {
                itemSlots[i].ItemIcon.enabled = true;
                itemSlots[i].ItemIcon.sprite = itemToUpdate.ItemIcon;
                break;
            }
        }
    }

    void CreatePhysicalItem(ItemInstance item)
    {
        GameObject droppedItem = Instantiate(item.GetItemPrefab(), transform.position, Quaternion.identity);
        droppedItem.GetComponent<ItemBehaviour>().SetItemInstance(item);
    }

}
