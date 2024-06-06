using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance { get; private set; }

    public GameObject inventoryUI;
    public Transform inventorySlotParent;
    public GameObject inventorySlotPrefab;
    public Text inventoryFullMessage;

    public const int MaxInventorySlots = 20;

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        inventoryUI.SetActive(false);
        inventoryFullMessage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
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
        if (inventorySlots.Count < MaxInventorySlots)
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, inventorySlotParent);
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if (slot != null)
            {
                slot.SetItem(item);
                inventorySlots.Add(slot);
            }
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

    public int GetCurrentSlotCount()
    {
        return inventorySlots.Count;
    }
}
