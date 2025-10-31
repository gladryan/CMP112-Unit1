using UnityEngine;

public class StarterSceneStar : MonoBehaviour
{
    [Header("Effects")]
    // Assign particle system prefab in Inspector
    public GameObject particleEffectPrefab;

    [Header("Motion Settings")]
    // Rotation speed in degrees per second
    public float rotationSpeed = 0.01f;
    // Amplitude of bobbing motion
    public float bobbingAmount = 0.01f;
    // Speed of bobbing motion
    public float bobbingSpeed = 10f;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        // Remember the original position of the GameObject
        startPosition = transform.position;

    }

    void Update()
    {
        // Rotate the object around its up axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Create a bobbing motion up and down
        timer += Time.deltaTime * bobbingSpeed;
        float newY = startPosition.y + Mathf.Sin(timer) * bobbingAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Instantiate the particle effect
            if (particleEffectPrefab != null)
            {
                Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            }

            // Destroy the star
            Destroy(gameObject);

        }
    }
}
