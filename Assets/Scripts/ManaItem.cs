using UnityEngine;

public class ManaItem : MonoBehaviour
{
    public int manaValue;
    public GameObject collectEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController2 player = other.GetComponent<PlayerController2>();
            if (player != null)
            {
                player.RegenerateMana(manaValue);
                Instantiate(collectEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
