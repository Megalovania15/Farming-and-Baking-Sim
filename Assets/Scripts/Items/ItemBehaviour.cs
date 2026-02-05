using UnityEngine;

public class ItemBehaviour : MonoBehaviour, ICollectible
{
    public ItemData itemData;

    private ItemInstance thisItem;

    void Awake()
    {
        thisItem = new ItemInstance(itemData);
    }

    public Vector2 GetPosition() => transform.position;

    public void SetPosition(Vector2 newPos) => transform.position = newPos;

    public void DestroyObject() => Destroy(gameObject);

    public ItemInstance GetItemInstance() => thisItem;

    public ItemInstance SetItemInstance(ItemInstance item) => thisItem = item;
}
