using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;

    private Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    { 
        if (item != null)
        {
            item.RemoveFromInventory();
        }
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
