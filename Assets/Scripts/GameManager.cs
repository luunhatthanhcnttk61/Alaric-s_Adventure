// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public int healthItems = 0;
//     public PlayerController2 player;
//     public Text inventoryFullText;
//     public Text coinsText;
//     private int totalCoins = 0;

//     private void Start()
//     {
//         inventoryFullText.gameObject.SetActive(false);
//         UpdateCoinsText();
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
//             healthItems = 0;
//         }
//     }

//     public bool TryAddItemToInventory(Item item)
//     {
//         if (Inventory.Instance.items.Count < Inventory.Instance.space)
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

//     public void AddCoins(int value)
//     {
//         totalCoins += value;
//         UpdateCoinsText();
//     }

//     private void UpdateCoinsText()
//     {
//         if (coinsText != null)
//         {
//             coinsText.text = "Coins: " + totalCoins;
//         }
//     }

//     public int GetPlayerCoins()
//     {
//         return totalCoins;
//     }

//     public void SpendCoins(int amount)
//     {
//         totalCoins -= amount;
//         UpdateCoinsText();
//     }

//     public void AddItemToInventory(ItemShop itemShop)
//     {
//         Item newItem;
//         switch (itemShop.itemName)
//         {
//             case "Sword":
//                 newItem = ScriptableObject.CreateInstance<SwordItem>();
//                 break;
//             case "Armour":
//                 newItem = ScriptableObject.CreateInstance<ArmourItem>();
//                 break;
//             default:
//                 newItem = ScriptableObject.CreateInstance<Item>();
//                 break;
//         }

//         newItem.itemName = itemShop.itemName;
//         newItem.icon = itemShop.icon;

//         if (TryAddItemToInventory(newItem))
//         {
//             Debug.Log("Item added to inventory: " + newItem.itemName);
//         }
//         else
//         {
//             Debug.Log("Failed to add item to inventory: " + newItem.itemName);
//         }
//     }
// }
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController2 player;
    public Text inventoryFullText;
    public Text coinsText;
    private int totalCoins = 0;
    public int healthItems = 0;

    private void Start()
    {
        inventoryFullText.gameObject.SetActive(false);
        UpdateCoinsText();
    }

    private void Update()
    {
    }

    public bool TryAddItemToInventory(Item item)
    {
        if (Inventory.Instance.items.Count < Inventory.Instance.space)
        {
            Inventory.Instance.Add(item);
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
    public void AddHealthItem(int healthToAdd)
    {
        healthItems += healthToAdd;
        if (player.currentHealth < player.maxHealth && player.currentHealth > 0)
        {
            player.currentHealth += healthItems;
            healthItems = 0;
        }
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
        Item newItem = CreateItemFromShopItem(itemShop);

        if (newItem != null)
        {
            if (TryAddItemToInventory(newItem))
            {
                newItem.itemName = itemShop.itemName;
                Debug.Log("Item added to inventory: " + newItem.itemName);
            }
            else
            {
                Debug.Log("Failed to add item to inventory: " + newItem.itemName);
            }
        }
    }

    private Item CreateItemFromShopItem(ItemShop itemShop)
    {
        Item newItem = null;

        switch (itemShop.itemType)
        {
            case ItemType.Sword:
                SwordItem swordItem = ScriptableObject.CreateInstance<SwordItem>();
                swordItem.attackPower = itemShop.attackPower;
                swordItem.itemName = itemShop.itemName;
                swordItem.icon = itemShop.icon;
                newItem = swordItem;
                break;

            case ItemType.Armour:
                ArmourItem armourItem = ScriptableObject.CreateInstance<ArmourItem>();
                armourItem.armourValue = itemShop.armorValue;
                armourItem.itemName = itemShop.itemName;
                armourItem.icon = itemShop.icon;
                newItem = armourItem;
                break;

            default:
                Debug.LogWarning("Unsupported item type");
                break;
        }

        return newItem;
    }
}
