using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IStorage
{
    [SerializeField]
    private int maxSize = 10;

    [SerializeField]
    private List<ItemInstance> items = new();

    public bool AddItem(ItemInstance item)
    {
        //need to check for an available slot
        for (var i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return true;
            }
        }

        //adds an item if there is space
        if (items.Count < maxSize)
        {
            items.Add(item);
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

    void CreatePhysicalItem(ItemInstance item)
    {
        GameObject droppedItem = Instantiate(item.GetItemPrefab(), transform.position, Quaternion.identity);
        droppedItem.GetComponent<ItemBehaviour>().SetItemInstance(item);
    }

}
