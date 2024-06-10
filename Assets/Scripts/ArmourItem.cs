// using UnityEngine;

// [CreateAssetMenu(fileName = "New Armour Item", menuName = "Inventory/ArmourItem")]
// public class ArmourItem : Item
// {
//     public int armourValue;

//     public override void Use()
//     {
//         base.Use();
//         PlayerController2 player = FindObjectOfType<PlayerController2>();
//         if (player != null)
//         {
//             player.AddArmour(armourValue);
//         }
//         RemoveFromInventory();
//     }
// }
using UnityEngine;

[CreateAssetMenu(fileName = "New Armour Item", menuName = "Inventory/ArmourItem")]
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
