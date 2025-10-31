using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        // Enable the AudioSource if it wasn't already enabled
        if (audioSource != null)
        {
            // Ensure it is enabled
            audioSource.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // only the player can trigger this event
        if (other.CompareTag("Player"))
        {
            PlayPickupSound();
            // Add more code here to handle item pick-up
            Destroy(gameObject);
        }
    }

    private void PlayPickupSound()
    {
        if (audioSource != null && audioSource.enabled)
        {
            audioSource.Play();
        }
    }
}
