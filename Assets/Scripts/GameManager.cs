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
//     public Text inventoryFullText;

//     private void Start()
//     {
//         inventoryFullText.gameObject.SetActive(false);
//     }

//     private void Update()
//     {
//     }

//     public void AddHealthItem(int healthToAdd)
//     {
//         healthItems += healthToAdd;
//         if (player.currentHealth < player.maxHealth && player.currentHealth > 0)
//         {
//             player.currentHealth += healthItems;
//         }
//     }

//     public void AddCoins(int value)
//     {
//         coins += value;
//         coinsText.text = "Coins: " + coins;
//     }

//     public bool TryAddItemToInventory(Item item)
//     {
//         if (InventoryUIManager.Instance.GetCurrentSlotCount() < InventoryUIManager.MaxInventorySlots)
//         {
//             InventoryUIManager.Instance.AddItem(item);
//             return true;
//         }
//         else
//         {
//             StartCoroutine(ShowInventoryFullMessage());
//             return false;
//         }
//     }

//     private IEnumerator ShowInventoryFullMessage()
//     {
//         inventoryFullText.gameObject.SetActive(true);
//         yield return new WaitForSeconds(2f);
//         inventoryFullText.gameObject.SetActive(false);
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int healthItems = 0;
    public PlayerController2 player;
    public Text inventoryFullText;
    public Text coinsText; // Thêm biến tham chiếu đến Text UI để hiển thị số lượng coin

    private int totalCoins = 0; // Số lượng coin hiện tại

    private void Start()
    {
        inventoryFullText.gameObject.SetActive(false);
        UpdateCoinsText(); // Cập nhật UI khi bắt đầu
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

    // Thêm hàm để thêm coin
    public void AddCoins(int value)
    {
        totalCoins += value;
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + totalCoins;
        }
    }
}
