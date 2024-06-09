using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int healthItems = 0;
    public PlayerController2 player;
    public Text inventoryFullText;
    public Text coinsText;
    private int totalCoins = 0;

    private void Start()
    {
        inventoryFullText.gameObject.SetActive(false);
        UpdateCoinsText();
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
            healthItems = 0;
        }
    }

    public bool TryAddItemToInventory(Item item)
    {
        if (Inventory.Instance.items.Count < Inventory.Instance.space)
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

    public int GetPlayerCoins()
    {
        return totalCoins;
    }

    public void SpendCoins(int amount)
    {
        totalCoins -= amount;
        UpdateCoinsText();
    }

    public void AddItemToInventory(ItemShop itemShop)
    {
        Item newItem = new Item { itemName = itemShop.itemName, icon = itemShop.icon, /*price = itemShop.price*/ };
        if (TryAddItemToInventory(newItem))
        {
            Debug.Log("Item added to inventory: " + newItem.itemName);
        }
        else
        {
            Debug.Log("Failed to add item to inventory: " + newItem.itemName);
        }
    }

}
