using UnityEngine;
using UnityEngine.UI;

public class ItemSlotShop : MonoBehaviour
{
    public Image icon;
    public Text itemNameText;
    public Text priceText;
    public Button buyButton;  

    private ShopManager shopManager;
    private string itemName;
    private int itemPrice;

    public void SetUpItemSlot(ItemShop item, ShopManager manager)
    {
        icon.sprite = item.icon;
        itemNameText.text = item.itemName;
        priceText.text = item.price.ToString();
        itemName = item.itemName;
        itemPrice = item.price;
        shopManager = manager;

        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    public void OnBuyButtonClicked()
    {
        shopManager.OnItemButtonClicked(itemName, itemPrice);
    }
}
