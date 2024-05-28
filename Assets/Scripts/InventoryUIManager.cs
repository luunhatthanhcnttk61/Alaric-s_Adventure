using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance { get; private set; } // Singleton instance

    public GameObject inventoryUI; // Tham chiếu đến UI kho đồ
    public Transform inventorySlotParent; // Vị trí của các slot trong kho
    public GameObject inventorySlotPrefab; // Prefab của slot trong kho

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Đảm bảo chỉ có một instance của InventoryUIManager
            return;
        }
        DontDestroyOnLoad(gameObject); // Đảm bảo InventoryUIManager không bị phá hủy khi load scene khác
    }

    void Start()
    {
        // Ẩn kho đồ khi bắt đầu
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím "I" để bật/tắt Inventory UI
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        Cursor.visible = inventoryUI.activeSelf; // Hiển thị hoặc ẩn con trỏ chuột
        Cursor.lockState = inventoryUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddItem(Item item)
    {
        GameObject slotObject = Instantiate(inventorySlotPrefab, inventorySlotParent);
        InventorySlot slot = slotObject.GetComponent<InventorySlot>();
        if (slot != null)
        {
            slot.SetItem(item);
            inventorySlots.Add(slot); 
        }
    }

    public void RemoveItem(Item item)
    {
        // Tìm slot chứa item và xóa item
        InventorySlot slotToRemove = inventorySlots.Find(slot => slot.GetItem() == item);
        if (slotToRemove != null)
        {
            inventorySlots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);
        }
    }
}
