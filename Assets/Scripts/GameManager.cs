using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public PlayerController2 player;
//     public Text inventoryFullText;
//     public Text coinsText;
//     private int totalCoins = 0;
//     public int healthItems = 0;
//     public InventoryUIManager inventoryUIManager;

//     private void Start()
//     {
//         LoadPlayerData();
//         inventoryFullText.gameObject.SetActive(false);
//         UpdateCoinsText();
//     }

//     private void Update()
//     {
//     }

//     public void SavePlayerData()
//     {
//         SaveSystem.SavePlayer(this);
//     }

//     public void LoadPlayerData()
//     {
//         PlayerData data = SaveSystem.LoadPlayer();
//         if (data != null)
//         {
//             Vector3 position;
//             position.x = data.position[0];
//             position.y = data.position[1];
//             position.z = data.position[2];

//             player.transform.position = position;
//             totalCoins = data.coins;
//             player.currentHealth = data.currentHealth;
//             player.maxHealth = data.maxHealth;
//             player.currentMana = data.currentMana;
//             player.maxMana = data.maxMana;

//             Inventory.Instance.items.Clear();
//             foreach (var item in data.inventory)
//             {
//                 if (item is SerializableSwordItem serializableSwordItem)
//                 {
//                     SwordItem swordItem = ScriptableObject.CreateInstance<SwordItem>();
//                     swordItem.itemName = serializableSwordItem.itemName;
//                     swordItem.attackPower = serializableSwordItem.attackPower;
//                     swordItem.icon = Resources.Load<Sprite>(serializableSwordItem.iconPath);
//                     Inventory.Instance.Add(swordItem);
//                 }
//                 else if (item is SerializableArmourItem serializableArmourItem)
//                 {
//                     ArmourItem armourItem = ScriptableObject.CreateInstance<ArmourItem>();
//                     armourItem.itemName = serializableArmourItem.itemName;
//                     armourItem.armourValue = serializableArmourItem.armourValue;
//                     armourItem.icon = Resources.Load<Sprite>(serializableArmourItem.iconPath);
//                     Inventory.Instance.Add(armourItem);
//                 }
//             }

//             UpdateCoinsText();
//         }
//     }

//     public bool TryAddItemToInventory(Item item)
//     {
//         if (Inventory.Instance.items.Count < Inventory.Instance.space)
//         {
//             Inventory.Instance.Add(item);
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

//     public void AddHealthItem(int healthToAdd)
//     {
//         healthItems += healthToAdd;
//         if (player.currentHealth < player.maxHealth && player.currentHealth > 0)
//         {
//             player.currentHealth += healthItems;
//             healthItems = 0;
//         }
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
//         Item newItem = CreateItemFromShopItem(itemShop);

//         if (newItem != null)
//         {
//             if (TryAddItemToInventory(newItem))
//             {
//                 Debug.Log("Item added to inventory: " + newItem.itemName);
//             }
//             else
//             {
//                 Debug.Log("Failed to add item to inventory: " + newItem.itemName);
//             }
//         }
//     }

//     private Item CreateItemFromShopItem(ItemShop itemShop)
//     {
//         Item newItem = null;

//         switch (itemShop.itemType)
//         {
//             case ItemType.Sword:
//                 SwordItem swordItem = ScriptableObject.CreateInstance<SwordItem>();
//                 swordItem.attackPower = itemShop.attackPower;
//                 swordItem.itemName = itemShop.itemName ;
//                 swordItem.icon = itemShop.icon;
//                 newItem = swordItem;
//                 break;

//             case ItemType.Armour:
//                 ArmourItem armourItem = ScriptableObject.CreateInstance<ArmourItem>();
//                 armourItem.armourValue = itemShop.armorValue;
//                 armourItem.itemName = itemShop.itemName ;
//                 armourItem.icon = itemShop.icon;
//                 newItem = armourItem;
//                 break;

//             default:
//                 Debug.LogWarning("Unsupported item type");
//                 break;
//         }

//         return newItem;
//     }
// }
public class GameManager : MonoBehaviour
{
    public PlayerController2 player;
    public Text inventoryFullText;
    public Text coinsText;
    private int totalCoins = 0;
    public int healthItems = 0;
    public InventoryUIManager inventoryUIManager;

    private void Start()
    {
        LoadPlayerData();
        inventoryFullText.gameObject.SetActive(false);
        UpdateCoinsText();
        // Đảm bảo các UI không được kích hoạt
        UIManager.Instance.HidePauseMenu();
        UIManager.Instance.HideItemUsePopup();
    }

    private void Update()
    {
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            player.transform.position = position;
            totalCoins = data.coins;
            player.currentHealth = data.currentHealth;
            player.maxHealth = data.maxHealth;
            player.currentMana = data.currentMana;
            player.maxMana = data.maxMana;

            Inventory.Instance.items.Clear();
            foreach (var item in data.inventory)
            {
                if (item is SerializableSwordItem serializableSwordItem)
                {
                    SwordItem swordItem = ScriptableObject.CreateInstance<SwordItem>();
                    swordItem.itemName = serializableSwordItem.itemName;
                    swordItem.attackPower = serializableSwordItem.attackPower;
                    swordItem.icon = Resources.Load<Sprite>(serializableSwordItem.iconPath);
                    Inventory.Instance.Add(swordItem);
                }
                else if (item is SerializableArmourItem serializableArmourItem)
                {
                    ArmourItem armourItem = ScriptableObject.CreateInstance<ArmourItem>();
                    armourItem.itemName = serializableArmourItem.itemName;
                    armourItem.armourValue = serializableArmourItem.armourValue;
                    armourItem.icon = Resources.Load<Sprite>(serializableArmourItem.iconPath);
                    Inventory.Instance.Add(armourItem);
                }
            }

            UpdateCoinsText();
        }
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
                swordItem.itemName = itemShop.itemName ;
                swordItem.icon = itemShop.icon;
                newItem = swordItem;
                break;

            case ItemType.Armour:
                ArmourItem armourItem = ScriptableObject.CreateInstance<ArmourItem>();
                armourItem.armourValue = itemShop.armorValue;
                armourItem.itemName = itemShop.itemName ;
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
