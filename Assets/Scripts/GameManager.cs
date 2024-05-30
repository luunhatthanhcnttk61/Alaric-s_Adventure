// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public int healthItems = 0;
//     public int coins = 0;
//     public Text coinsText;
//     public PlayerController2 player;
//     public InventoryUIManager inventoryUIManager;
    

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     public void AddHealthItem(int healthToAdd)
//     {
//         healthItems += healthToAdd;
//         if (player.currentHealth < player.maxHealth && player.currentHealth > 0)
//         {
//             int potentialHealth = player.currentHealth + healthItems;
//             if (potentialHealth > player.maxHealth)
//             {
//                 player.currentHealth = player.maxHealth;
//             }
//             else
//             {
//                 player.currentHealth = potentialHealth;
//             }
//         }
//     }


//     public void AddCoins(int value)
//     {
//         coins += value;
//         coinsText.text = "Coins: " + coins;
//     }

//     public void AddItemToInventory(Item item)
//     {
//         inventoryUIManager.AddItem(item); // Thêm item vào inventory
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int healthItems = 0;
    public int coins = 0;
    public Text coinsText;
    public PlayerController2 player;
    public Text inventoryFullText;

    private void Start()
    {
        inventoryFullText.gameObject.SetActive(false);
    }

    private void Update()
    {
    }

    public void AddHealthItem(int healthToAdd)
    {
        healthItems += healthToAdd;
        if (player.currentHealth < player.maxHealth && player.currentHealth > 0)
        {
            player.currentHealth += healthItems;
        }
    }

    public void AddCoins(int value)
    {
        coins += value;
        coinsText.text = "Coins: " + coins;
    }

    public bool TryAddItemToInventory(Item item)
    {
        if (InventoryUIManager.Instance.GetCurrentSlotCount() < InventoryUIManager.MaxInventorySlots)
        {
            InventoryUIManager.Instance.AddItem(item);
            return true;
        }
        else
        {
            StartCoroutine(ShowInventoryFullMessage());
            return false;
        }
    }

    private IEnumerator ShowInventoryFullMessage()
    {
        inventoryFullText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        inventoryFullText.gameObject.SetActive(false);
    }
}
