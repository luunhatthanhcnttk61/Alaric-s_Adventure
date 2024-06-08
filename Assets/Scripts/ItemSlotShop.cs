using UnityEngine;
using UnityEngine.UI;

public class ItemSlotShop : MonoBehaviour
{
    public Image itemImage;
    public Text itemNameText;
    public Text itemPriceText;
    public Button buyButton;

    private string itemName;
    private int itemPrice;
    private ShopManager shopManager;

    public void SetUpItemSlot(Sprite itemSprite, string name, int price, ShopManager manager)
    {
        itemImage.sprite = itemSprite;
        itemNameText.text = name;
        itemPriceText.text = price + " Coins";
        itemName = name;
        itemPrice = price;
        shopManager = manager;

        buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnBuyButtonClicked()
    {
        shopManager.OnItemButtonClicked(itemName, itemPrice);
    }
}
