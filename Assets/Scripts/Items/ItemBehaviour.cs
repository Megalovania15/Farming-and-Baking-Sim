using System.Collections;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour, ICollectible
{
    public ItemData itemData;
    public bool ItemDropped { get; set; }

    private ItemInstance thisItem;

    void Awake()
    {
        thisItem = new ItemInstance(itemData);
        ItemDropped = true;
        StartCoroutine(DroppedTimer());
    }

    public Vector2 GetPosition() => transform.position;

    public void SetPosition(Vector2 newPos) => transform.position = newPos;

    public void DestroyObject() => Destroy(gameObject);

    public ItemInstance GetItemInstance() => thisItem;

    public ItemInstance SetItemInstance(ItemInstance item) => thisItem = item;

    IEnumerator DroppedTimer()
    {
        if (ItemDropped)
        {
            yield return new WaitForSeconds(3f);
            ItemDropped = false;
        }
    }
}
