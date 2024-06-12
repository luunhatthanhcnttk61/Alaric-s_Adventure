using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int coins;
    public List<object> inventory; // Dùng object để chứa cả SwordItem và ArmourItem
    public float[] position; // Lưu vị trí của người chơi
    public int currentHealth; // Lưu máu của người chơi
    public int maxHealth;
    public float currentMana; // Lưu mana của người chơi
    public float maxMana;

    public PlayerData(GameManager gameManager)
    {
        coins = gameManager.GetPlayerCoins();
        currentHealth = gameManager.player.currentHealth;
        maxHealth = gameManager.player.maxHealth;
        currentMana = gameManager.player.currentMana;
        maxMana = gameManager.player.maxMana;

        // Lưu vị trí của người chơi
        Vector3 playerPosition = gameManager.player.transform.position;
        position = new float[3];
        position[0] = playerPosition.x;
        position[1] = playerPosition.y;
        position[2] = playerPosition.z;

        // Chuyển đổi inventory
        inventory = new List<object>();
        foreach (var item in Inventory.Instance.items)
        {
            if (item is SwordItem swordItem)
            {
                SerializableSwordItem serializableSwordItem = new SerializableSwordItem
                {
                    itemName = swordItem.itemName,
                    attackPower = swordItem.attackPower,
                    iconPath = swordItem.icon.name // Assuming icons are saved as resources
                };
                inventory.Add(serializableSwordItem);
            }
            else if (item is ArmourItem armourItem)
            {
                SerializableArmourItem serializableArmourItem = new SerializableArmourItem
                {
                    itemName = armourItem.itemName,
                    armourValue = armourItem.armourValue,
                    iconPath = armourItem.icon.name // Assuming icons are saved as resources
                };
                inventory.Add(serializableArmourItem);
            }
        }
    }
}
