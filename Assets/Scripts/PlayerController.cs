using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 0;

    private Rigidbody rb;
    private float movementX, movementY;

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
    
    private void FixedUpdate() // Will call once per fixed frame rate frame
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // Take OnMove() info and make a Vector3 variable for it
        rb.AddForce(movement*playerSpeed);
    }

}
