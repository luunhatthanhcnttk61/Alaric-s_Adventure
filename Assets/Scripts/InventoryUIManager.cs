using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance { get; private set; } // Singleton instance

    public GameObject inventoryUI; // Tham chiếu đến UI kho đồ
    public Transform inventorySlotParent; // Vị trí của các slot trong kho
    public GameObject inventorySlotPrefab; // Prefab của slot trong kho

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public Text inventoryFullMessage;
    private bool canCollect = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
            return;
        }
        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
        inventoryUI.SetActive(false);
        inventoryFullMessage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if(inventorySlots.Count >= 25)
        {
            inventoryFullMessage.gameObject.SetActive(true);
        }
        else 
        {
            inventoryFullMessage.gameObject.SetActive(false);
        }
    }

    public void ToggleInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        Cursor.visible = inventoryUI.activeSelf; 
        Cursor.lockState = inventoryUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddItem(Item item)
    {
        if(inventorySlots.Count < 25 && canCollect == true)
        { 
            Debug.Log("So luong do trong kho: " + inventorySlots.Count);
            GameObject slotObject = Instantiate(inventorySlotPrefab, inventorySlotParent);
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.SetItem(item);
                inventorySlots.Add(slot); 
            }
        }
        else
        {   
            canCollect = false;
            inventoryFullMessage.gameObject.SetActive(true);
        }
        
    }

    public void RemoveItem(Item item)
    { 
        InventorySlot slotToRemove = inventorySlots.Find(slot => slot.GetItem() == item);
        if (slotToRemove != null)
        {
            inventorySlots.Remove(slotToRemove);
            Destroy(slotToRemove.gameObject);
        }
    }
}
