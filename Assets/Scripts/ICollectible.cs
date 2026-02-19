using UnityEngine;

public interface ICollectible
{
    public bool ItemDropped { get; set; }
    public Vector2 GetPosition();
    public void SetPosition(Vector2 newPos);
    public void DestroyObject();
    public ItemInstance GetItemInstance();
}
