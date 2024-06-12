using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Sword Item", menuName = "Inventory/SwordItem")]
[Serializable]
public class SwordItem : Item
{
    public int attackPower;

    public override void Use()
    {
        base.Use();
        Sword sword = FindObjectOfType<Sword>();
        if (sword != null)
        {
            sword.UpdateSwordDamage(attackPower);
        }
        RemoveFromInventory();
    }
}
