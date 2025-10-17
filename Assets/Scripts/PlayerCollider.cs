using System.Threading;
using UnityEngine;
using TMPro;

public class PlayerCollider : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score;
    
    void Start()
    {
        score = 0;
        SetScoreText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible")) // If it hits a collectible update score and deactivate collectible
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Converts score variable to a displayable string
    }
}
