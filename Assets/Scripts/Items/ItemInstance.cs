using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    private string _name;
    private string _description;
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
        _name = item.Name;
        _description = item.Description;
        _itemObject = item.itemObject;
        itemQuality = Quality.None;
    }

    public GameObject GetItemPrefab()
    { 
        return _itemObject;
    }
}
