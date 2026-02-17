using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IStorage
{
    [SerializeField]
    private int maxSize = 10;

    [SerializeField]
    private ItemInstance[] items;

    [SerializeField] private GameObject inventorySlots;

    private InventorySlot[] itemSlots;

    void Awake()
    {
        itemSlots = new InventorySlot[inventorySlots.transform.childCount];
        items = new ItemInstance[inventorySlots.transform.childCount];

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
        //need to check for an available slot, could also use this to
        //check whether the inventory slot already has this item.
        for (var i = 0; i < items.Length; i++)
        {
            if (items[i] is null)
            {
                items[i] = item;
                UpdateItemDisplay(item);
                Debug.Log($"Added {item.Name}");
                return true;
            }
        }

        //adds an item if there is space
        /*if (items.Length < maxSize)
        {
            items.Add(item);
            
            return true;
        }*/
        //what happens if there isn't an available spot
        Debug.Log("There is no space in the inventory.");
        return false;
    }

    public void DropItem(ItemInstance itemToDrop)
    {
        ItemInstance newItem = null;

        for (int i = 0; i < items.Length; i++)
        {
            if (ReferenceEquals(items[i], itemToDrop))
            {
                newItem = itemToDrop;
                items[i] = null;
                break;
            }
        }

        if (newItem is not null)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (ReferenceEquals(itemSlots[i], newItem))
                {
                    itemSlots[i].RemoveStoredItem();
                    itemSlots[i].UpdateItemIcon();
                    break;
                }
            }
            CreatePhysicalItem(newItem);
        }
        
        /*if (items.Contains(itemToDrop))
        {
            CreatePhysicalItem(itemToDrop);
            items.Remove(itemToDrop);
            return true;
        }*/
        Debug.Log("The item is no longer in the inventory");
    }

    void UpdateItemDisplay(ItemInstance itemToUpdate)
    {
        for(int i = 0; i < itemSlots.Length; i++)
        {
            //we need to run through all the slots, and update them with an item
            //otherwise disable the remainder images and set the sprite on them to null.
            if (itemSlots[i].StoredItem == null)
            {
                itemSlots[i].StoreItemInSlot(itemToUpdate);
                itemSlots[i].UpdateItemIcon(itemToUpdate.ItemIcon);
                itemSlots[i].AddToQuantity(1);
                itemSlots[i].UpdateText();
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
