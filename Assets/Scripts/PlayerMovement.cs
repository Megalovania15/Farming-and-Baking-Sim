using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private Vector2 moveDir = Vector2.zero;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    //this has to know which item we want to drop
    public void DropItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var inventory = GetComponent<Inventory>();
            //inventory.DropItem();
        }
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(true);
            }
            else
            {
                inventoryUI.SetActive(false);
            }
        }
    }

    void Move()
    {
        rb.linearVelocity = moveDir * speed;
    }
}
