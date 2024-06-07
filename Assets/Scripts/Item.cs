// using UnityEngine;

// [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
// public class Item : ScriptableObject
// {
//     public string itemName = "New Item";
//     public Sprite icon = null;
//     public bool isDefaultItem = false;

//     public virtual void Use()
//     {
//         Debug.Log("Using " + itemName);
//     }

//     public void RemoveFromInventory()
//     {
//         Inventory.Instance.Remove(this);
//     }
// }
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
        // Add your custom use logic here
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}
