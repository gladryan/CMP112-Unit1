using UnityEngine;

public class CloudBob : MonoBehaviour
{
    [Header("Bob Settings")]
    // Max distance to move from original position
    public float bobDistance = 0.5f;
    // Speed of bobbing
    public float bobSpeed = 1f;
    // True = up/down, False = left/right
    public bool vertical = true;      

    private Vector3 startPos;
    private float bobOffset;

    void Start()
    {
        // Store starting position
        startPos = transform.position;

        // Randomize starting phase so clouds don't move in sync
        bobOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        // Calculate bob amount using sine wave for smooth movement
        float bob = Mathf.Sin(Time.time * bobSpeed + bobOffset) * bobDistance;

        if (vertical)
        {
            transform.position = startPos + new Vector3(0, bob, 0);
        }
        else
        {
            transform.position = startPos + new Vector3(bob, 0, 0);
        }
    }
}
