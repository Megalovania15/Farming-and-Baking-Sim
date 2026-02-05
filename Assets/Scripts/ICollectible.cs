using UnityEngine;

public interface ICollectible
{
    public Vector2 GetPosition();
    public void SetPosition(Vector2 newPos);
    public void DestroyObject();
    public ItemInstance GetItemInstance();
    //public void CreateInstance(ItemInstance newItem);

}
