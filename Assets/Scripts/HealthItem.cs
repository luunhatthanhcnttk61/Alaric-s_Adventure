using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthValue;
    public GameObject collectEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController2 player = other.GetComponent<PlayerController2>();
            if (player != null)
            {
                player.Heal(healthValue);
                Instantiate(collectEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
