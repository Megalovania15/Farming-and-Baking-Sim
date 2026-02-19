using System.Collections.Generic;
using UnityEngine;

public class FlyObjectToTarget : MonoBehaviour
{
    [SerializeField]
    private float collectionTime = 0.2f;

    private Vector3 objectVelocity = Vector3.zero;
    private ICollectible collectibleToMove;

    private List<ICollectible> objectsToCollect = new List<ICollectible>();

    private IStorage inventory;

    void Awake()
    {
        inventory = GetComponentInParent<IStorage>();
    }

    private void FixedUpdate()
    {
        if (objectsToCollect.Count > 0)
        {
            foreach (ICollectible collectible in objectsToCollect)
            {
                if (objectsToCollect.Contains(collectible))
                {
                    FlyToPlayer(collectible);
                }
            }
        }
    }

    //gets the position of the collectible and uses the SmoothDamp function to make it "fly"
    //to the player
    void FlyToPlayer(ICollectible collectible)
    {
        var initialPos = collectible.GetPosition();
        collectible.SetPosition(Vector3.SmoothDamp(initialPos, transform.position,
            ref objectVelocity, collectionTime));
    }

    //need to check the inventory to first see if there is space to add another item.
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            if (collectible is not null)
            {
                var collectibleInstance = collectible.GetItemInstance();

                if (inventory.CanAddItem() && !collectible.ItemDropped)
                {
                    objectsToCollect.Add(collectible);
                }
            }
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.
            TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            if (objectsToCollect.Contains(collectible))
            {
                objectsToCollect.Remove(collectible);
            }
        }
    }
}
