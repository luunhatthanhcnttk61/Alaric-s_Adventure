using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject confirmationPanel;
    public Text confirmationText;
    public Button yesButton;
    public Button noButton;
    public GameObject itemSlotPrefab;
    public Transform itemSlotContainer;
    public GameManager gameManager; 

    public List<ItemShop> items;

    private string selectedItem;
    private int selectedItemPrice;

    private void Start()
    {
        shopPanel.SetActive(false);
        confirmationPanel.SetActive(false);

        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);

        PopulateShop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleShop();
        }
    }

    private void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        if (shopPanel.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void PopulateShop()
    {
        foreach (var item in items)
        {
            GameObject itemSlotObject = Instantiate(itemSlotPrefab, itemSlotContainer);
            ItemSlotShop itemSlot = itemSlotObject.GetComponent<ItemSlotShop>();
            itemSlot.SetUpItemSlot(item, this);
        }
    }

    public void OnItemButtonClicked(string itemName, int itemPrice)
    {
        selectedItem = itemName;
        selectedItemPrice = itemPrice;
        confirmationText.text = "Do you want to buy " + itemName + " for " + itemPrice + " Coins?";
        confirmationPanel.SetActive(true);
    }

    public void OnYesButtonClicked()
    {
        if (gameManager.GetPlayerCoins() >= selectedItemPrice)
        {
            gameManager.SpendCoins(selectedItemPrice);
            Debug.Log("Bought " + selectedItem);
            confirmationPanel.SetActive(false);
            ToggleShop(); 
        }
        else
        {
            Debug.Log("Not enough coins to buy " + selectedItem);
            confirmationPanel.SetActive(false);
        }
    }

    public void OnNoButtonClicked()
    {
        confirmationPanel.SetActive(false);
    }
}
