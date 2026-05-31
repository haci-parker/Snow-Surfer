using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private bool isCollected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Prevent multiple trigger registrations on the same coin
        if (isCollected) return;

        // Check if the triggering object is the Player (by tag or by having a PlayerController component)
        if (collision.CompareTag("Player") || collision.GetComponent<PlayerController>() != null)
        {
            isCollected = true;

            // Disable the collider immediately to prevent duplicate trigger calls in the same frame
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }

            // Increment the money value on the MoneyManager singleton
            if (MoneyManager.Instance != null)
            {
                MoneyManager.Instance.AddMoney(1);
            }
            else
            {
                Debug.LogWarning("MoneyManager Instance is not found in the scene! Please make sure a MoneyManager exists.");
            }

            // Destroy the coin immediately
            Destroy(gameObject);
        }
    }
}
