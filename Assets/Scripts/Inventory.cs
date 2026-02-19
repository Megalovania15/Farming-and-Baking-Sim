using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour, IStorage
{
    [SerializeField]
    private int maxSize = 10;

    [SerializeField]
    private ItemInstance[] items;

    [SerializeField]
    private GameObject inventorySlots;
    
    private InventorySlot[] itemSlots;

    private ItemInstance currentlySelectedItem = null;

    void OnDisable()
    {
        Debug.Log($"Inventory has been disabled");
        foreach (var itemSlot in itemSlots)
        {
            itemSlot.onItemClicked.RemoveListener(HandleItemSelection);
        }
    }

    void Awake()
    {
        itemSlots = new InventorySlot[inventorySlots.transform.childCount];
        items = new ItemInstance[inventorySlots.transform.childCount];

        for (int i = 0; i < inventorySlots.transform.childCount; i++)
        {
            //need to store each of them in the array
            itemSlots[i] = inventorySlots.transform.GetChild(i).
                gameObject.GetComponent<InventorySlot>();
            itemSlots[i].UpdateItemIcon();
            itemSlots[i].UpdateText();
            itemSlots[i].onItemClicked.AddListener(HandleItemSelection);
        }

        /*foreach (var itemSlot in itemSlots)
        {
            itemSlot.ItemIcon.enabled = false;
        }*/

        //screenPos.performed += context => { currentScreenPos = context.ReadValue<Vector2>(); };
    }

    public bool CanAddItem()
    {
        for (var i = 0; i < items.Length; i++)
        {
            if (items[i] is null)
            {
                return true;
            }
        }
        return false;
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
        ItemInstance removedItem = null;

        for (int i = 0; i < items.Length; i++)
        {
            if (ReferenceEquals(items[i], itemToDrop))
            {
                removedItem = itemToDrop;
                items[i] = null;
                break;
            }
        }

        if (removedItem is not null)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (ReferenceEquals(itemSlots[i].StoredItem, removedItem))
                {
                    if (itemSlots[i].Quantity > 1)
                    {
                        itemSlots[i].RemoveFromQuantity(1);
                    }
                    else 
                    {
                        itemSlots[i].RemoveFromQuantity(1);
                        itemSlots[i].RemoveStoredItem();
                        itemSlots[i].UpdateItemIcon();
                        itemSlots[i].UpdateText();
                    }
                    break;
                }
            }
            CreatePhysicalItem(removedItem);
        }
        
        /*if (items.Contains(itemToDrop))
        {
            CreatePhysicalItem(itemToDrop);
            items.Remove(itemToDrop);
            return true;
        }*/
        Debug.Log("The item is no longer in the inventory");
    }

    //need to have a look at this method again when refactoring, because it's doing some of
    //what the slot should do, and isn't just updating the item display. And if it's supposed
    //to be doing that, it's not very versatile.
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

    //to be updated with functionality for selecting and choosing what to do with the item
    void HandleItemSelection(InventorySlot selectedSlot)
    {
        if (selectedSlot.StoredItem is not null)
        {
            Debug.Log($"This slot has {selectedSlot.StoredItem.Name} stored in it");
            DropItem(selectedSlot.StoredItem);
        }
    }
}
