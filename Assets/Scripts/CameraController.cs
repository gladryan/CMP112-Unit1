using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position; // Takes camera pos and compares it to player pos to find offset
    }

    void LateUpdate() // Ensure camera adjusts after all other game updates take place
    {
        transform.position = player.transform.position + offset; // Updates camera pos to track the player
    }
}
