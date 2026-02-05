using System.Collections.Generic;
using UnityEngine;

public class CollectObjects: MonoBehaviour
{
    private IStorage inventory;

    void Awake()
    {
        inventory = GetComponent<IStorage>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.
            TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            if (collectible != null)
            {
                var collectableInstance = collectible.GetItemInstance();

                inventory.AddItem(collectableInstance);

                collectible.DestroyObject();
            }
        }
    }
}
