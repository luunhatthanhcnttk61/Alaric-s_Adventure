using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item"; // Sử dụng itemName thay vì name để tránh xung đột
    public Sprite icon = null;
    public bool isDefaultItem = false;

    // Phương thức Use để sử dụng item
    public virtual void Use()
    {
        Debug.Log("Using " + itemName);
    }

    // Phương thức để loại bỏ item
    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}
