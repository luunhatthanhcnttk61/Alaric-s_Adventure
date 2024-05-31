using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button useButton; // Nút sử dụng item
    public Button removeButton; // Nút xóa item
    public Text itemNameText; 

    Item item;

    private void Start()
    {
        useButton.onClick.AddListener(UseItem);
        removeButton.onClick.AddListener(RemoveItem);
    }

    public void SetItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        
        itemNameText.text = item.name; 
        itemNameText.enabled = true; 
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        
        itemNameText.text = "";
        itemNameText.enabled = false; 
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void RemoveItem()
    {
        if (item != null)
        {
            InventoryUIManager.Instance.RemoveItem(item);
        }
    }

    public Item GetItem()
    {
        return item;
    }
}

