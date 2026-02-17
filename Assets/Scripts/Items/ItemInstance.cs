using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemInstance
{
    public string Name { get; private set; }
    public Sprite ItemIcon { get; private set; }
    private string _description;
    private int _maxStack;
    private GameObject _itemObject;
    private Quality itemQuality;
    public enum Quality
    {
        None,
        Good,
        Great,
        Perfect
    }

    public ItemInstance(ItemData item)
    { 
        Name = item.Name;
        _description = item.Description;
        _itemObject = item.itemObject;
        ItemIcon = item.itemIcon;
        _maxStack = item.maxStack;
        itemQuality = Quality.None;
    }

    public GameObject GetItemPrefab()
    { 
        return _itemObject;
    }
}
