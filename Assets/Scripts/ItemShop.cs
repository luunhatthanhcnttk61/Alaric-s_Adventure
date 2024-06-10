using UnityEngine;

public enum ItemType
{
    General,
    Sword,
    Armour
}

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop/Item")]
public class ItemShop : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public ItemType itemType;

    // Các chỉ số riêng cho từng loại item
    public int attackPower; // Sử dụng cho Sword
    public int armorValue; // Sử dụng cho Armour
}
