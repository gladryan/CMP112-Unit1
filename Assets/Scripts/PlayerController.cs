using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 0, jumpForce = 1;

    private Rigidbody rb;
    private float movementX, movementY;
    private bool jumpEnabled;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); // Stores movement as a Vector2 variable
        movementX = movementVector.x;
        movementY = movementVector.y; // Assign both axes of movement as separate variables
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpEnabled = true; // Allows player to jump when touching ground
        }
        
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpEnabled = false; // Disables jumping after leaving ground
        }
    }

    private void Update()
    {
        if (jumpEnabled) // Checks if player can jump
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse); // Adds an instant force rather than gradual with Impulse
            }
        }

    }

    private void FixedUpdate() // Will call once per fixed frame rate frame
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Take OnMove() info and make a Vector3 variable for it
        rb.AddForce(movement * playerSpeed);
    }

}
