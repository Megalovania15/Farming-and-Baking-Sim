using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectObjects: MonoBehaviour
{
    private IStorage inventory;

    void Awake()
    {
        inventory = GetComponent<IStorage>();
    }    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.
            TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            if (collectible != null)
            {
                var collectableInstance = collectible.GetItemInstance();

                if (!collectible.ItemDropped && inventory.AddItem(collectableInstance))
                {
                    collectible.DestroyObject();
                }
                else
                {
                    return;
                }
            }
        }
    }
}
