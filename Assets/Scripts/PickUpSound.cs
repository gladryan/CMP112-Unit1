using UnityEngine;

public class PickUpSound : MonoBehaviour
{
    public AudioSource audioSource;
    [Tooltip("Optional: Multiple audio clips to randomise between on pickup")]
    public AudioClip[] pickupSounds;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayPickupSound();

            // Add more item-specific logic here (like score, health, etc.)
            Destroy(gameObject);
        }
    }

    private void PlayPickupSound()
    {
        if (audioSource == null || !audioSource.enabled) return;

        if (pickupSounds != null && pickupSounds.Length > 0)
        {
            // Pick a random sound
            AudioClip randomClip = pickupSounds[Random.Range(0, pickupSounds.Length)];
            audioSource.PlayOneShot(randomClip);
        }
        else
        {
            // Fallback to whatever is already assigned in the AudioSource
            audioSource.Play();
        }
    }
}
