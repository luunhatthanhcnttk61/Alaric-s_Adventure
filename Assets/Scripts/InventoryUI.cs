using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            Cursor.visible = inventoryUI.activeSelf; // Hiển thị hoặc ẩn con trỏ chuột
            Cursor.lockState = inventoryUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].SetItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
