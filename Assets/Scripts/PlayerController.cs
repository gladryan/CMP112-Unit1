using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    // Animation controller
    private Animator mAnimator;

    // Speed at which the player moves and jumps
    public float playerSpeed = 0, jumpForce = 1;

    // Rigidbody of the player.
    private Rigidbody rb;
    
    // Player input
    private PlayerInput input;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX, movementY;
    // Timer value
    private float currentTime;
    private bool jumpEnabled, timerActive = true;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;

    // UI object to display timer
    public TextMeshProUGUI timerText;
    
    // Coroutine to put power ups on a timer
    IEnumerator PowerDelay()
    {
        // Wait 2 seconds before resetting power boosts
        yield return new WaitForSeconds(1f);
        playerSpeed -= 5;
        jumpForce -= 5;
    }

    // Start is called before the first frame update.
    void Start()
    {
        // animations controller
        mAnimator = GetComponent<Animator>();

        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Get and store player input;
        input = GetComponent<PlayerInput>();

        // Initialize count and time to zero.
        count = 0; currentTime = 0;

        // Update the count display.
        SetCountText();

        // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Controls what happens when player dies
    void OnDeath()
    {
        SceneManager.LoadScene("Level1");
    }

    // jumping function
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Allows player to jump when touching ground
            jumpEnabled = true;
        }
        
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Disables jumping after leaving ground
            jumpEnabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            mAnimator.SetTrigger("run");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            mAnimator.SetTrigger("run");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            mAnimator.SetTrigger("run");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            mAnimator.SetTrigger("slide");
        }

        // Checks if timer is activated
        if (timerActive)
        {
            // Sets timer value
            currentTime = currentTime + Time.deltaTime;
        }
        // Store currentTime as variable that can be used as seconds, minutes, hours, ...
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        // Displays timer value neatly
        timerText.text = "Time =  " + time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();


        // Checks if player can jump
        if (jumpEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Adds an instant force rather than gradual with Impulse
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }
        }

        // Check if player has fallen off
        if (gameObject.transform.position.y < -10)
        {
            OnDeath();
        }

    }

    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Deliciousness: " + count.ToString();
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate() 
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * playerSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "Collectible" tag.
        if (other.gameObject.CompareTag("Collectible"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "Collectible" objects collected.
            count = count + 1;

            // Update the count display.
            SetCountText();
        }

        // Check if player collides with finish line
        if (other.gameObject.CompareTag("Finish"))
        {
            // Display win text
            winTextObject.SetActive(true);
            // Stop timer
            timerActive = false;
            // Gets elvis to boogie
            mAnimator.SetBool("dance", true);
            // Prevents further input
            input.DeactivateInput();
        }

        // Check if player collides with a star/power-up
        if (other.gameObject.CompareTag("PowerUp"))
        {
            // Apply power up
            playerSpeed += 5;
            jumpForce += 5;
            //Begin power up timer using coroutine
            StartCoroutine(PowerDelay());
        }
    }
}
