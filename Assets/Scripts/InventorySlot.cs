using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Text itemNameText; 

    Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        
        itemNameText.text = item.name; 
        itemNameText.enabled = true; 
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        
        itemNameText.text = "";
        itemNameText.enabled = false; 
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public Item GetItem()
    {
        return item;
    }
}
