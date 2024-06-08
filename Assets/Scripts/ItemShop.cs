using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemShop : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
}
