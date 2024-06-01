using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int coinValue = 1;
    public GameObject collectEffect;
    public AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddCoins(coinValue);
            }

            Instantiate(collectEffect, transform.position, transform.rotation);

            // Phát âm thanh khi nhặt coin
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            Destroy(gameObject);
        }
    }
}
