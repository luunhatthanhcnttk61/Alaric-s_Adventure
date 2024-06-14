using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string currentScene;
    public int coins;
    public List<object> inventory;
    public float[] position;
    public int currentHealth;
    public int maxHealth;
    public float currentMana;
    public float maxMana;

    public PlayerData(GameManager gameManager)
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        coins = gameManager.GetPlayerCoins();
        currentHealth = gameManager.player.currentHealth;
        maxHealth = gameManager.player.maxHealth;
        currentMana = gameManager.player.currentMana;
        maxMana = gameManager.player.maxMana;

        Vector3 playerPosition = gameManager.player.transform.position;
        position = new float[3];
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        inventory = new List<object>();
        foreach (var item in Inventory.Instance.items)
        {
            if (item is SwordItem swordItem)
            {
                SerializableSwordItem serializableSwordItem = new SerializableSwordItem
                {
                    itemName = swordItem.itemName,
                    attackPower = swordItem.attackPower,
                    iconPath = swordItem.icon.name
                };
                inventory.Add(serializableSwordItem);
            }
            else if (item is ArmourItem armourItem)
            {
                SerializableArmourItem serializableArmourItem = new SerializableArmourItem
                {
                    itemName = armourItem.itemName,
                    armourValue = armourItem.armourValue,
                    iconPath = armourItem.icon.name
                };
                inventory.Add(serializableArmourItem);
            }
        }
    }
}
