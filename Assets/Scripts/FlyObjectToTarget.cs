using System.Collections.Generic;
using UnityEngine;

public class FlyObjectToTarget : MonoBehaviour
{
    [SerializeField]
    private float collectionTime = 0.2f;

    private Vector3 objectVelocity = Vector3.zero;
    private ICollectible collectibleToMove;

    private List<ICollectible> objectsToCollect = new List<ICollectible>();
    
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

    void FlyToPlayer(ICollectible collectible)
    {
        var initialPos = collectible.GetPosition();
        collectible.SetPosition(Vector3.SmoothDamp(initialPos, transform.position,
            ref objectVelocity, collectionTime));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //can update to use interfaces instead of tags at a later stage
        if (collision.gameObject.
            TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            //collectibleToMove = collectible;
            objectsToCollect.Add(collectible);
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
