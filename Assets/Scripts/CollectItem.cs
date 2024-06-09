using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int healthValue;
    public int coinValue;
    public GameObject collectEffect;
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.AddHealthItem(healthValue);
                Instantiate(collectEffect, transform.position, transform.rotation);

                if (item != null)
                {
                    bool itemAdded = gameManager.TryAddItemToInventory(item);
                    if (itemAdded)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Inventory is full. Cannot add item.");
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
