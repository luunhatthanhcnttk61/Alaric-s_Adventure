using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Armour Item", menuName = "Inventory/ArmourItem")]
[Serializable]
public class ArmourItem : Item
{
    public int armourValue;

    public override void Use()
    {
        base.Use();
        PlayerController2 player = FindObjectOfType<PlayerController2>();
        if (player != null)
        {
            player.AddArmour(armourValue);
        }
        RemoveFromInventory();
    }
}
