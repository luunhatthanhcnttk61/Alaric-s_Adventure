using System.Collections;
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

    public GameObject itemUsePopup;
    public Text popupText;
    public Button useButton;
    public Button cancelButton;

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private Item currentItem;
    private InventorySlot currentSlot;

    public const int MaxInventorySlots = 24;

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
        itemUsePopup.SetActive(false);

        InitializeInventorySlots();

        useButton.onClick.AddListener(OnUseButton);
        cancelButton.onClick.AddListener(OnCancelButton);

        Inventory.Instance.onItemChangedCallback += UpdateUI;
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

    private void InitializeInventorySlots()
    {
        for (int i = 0; i < MaxInventorySlots; i++)
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, inventorySlotParent);
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if (slot != null)
            {
                inventorySlots.Add(slot);
            }
        }
    }

    public void AddItem(Item item)
    {
        if (Inventory.Instance.Add(item))
        {
            UpdateUI();
        }
        else
        {
            StartCoroutine(ShowInventoryFullMessage());
        }
    }

    public void RemoveItem(Item item)
    {
        Inventory.Instance.Remove(item);
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (i < Inventory.Instance.items.Count)
            {
                inventorySlots[i].SetItem(Inventory.Instance.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }

    public void ShowItemUsePopup(Item item, InventorySlot slot)
    {
        currentItem = item;
        currentSlot = slot;
        popupText.text = "Sử dụng " + item.itemName + "?";
        itemUsePopup.SetActive(true);
    }

    void OnUseButton()
    {
        currentSlot.UseItem();
        itemUsePopup.SetActive(false);
        UpdateUI();
    }

    void OnCancelButton()
    {
        itemUsePopup.SetActive(false);
    }

    private IEnumerator ShowInventoryFullMessage()
    {
        inventoryFullMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        inventoryFullMessage.gameObject.SetActive(false);
    }

    public int GetCurrentSlotCount()
    {
        return Inventory.Instance.items.Count;
    }
}
