// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CollectItem : MonoBehaviour
// {
//     public int healthValue;
//     public int coinValue;
//     public GameObject collectEffect;
//     public Item item; // Tham chiếu đến item để thêm vào inventory

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         GameManager gameManager = FindObjectOfType<GameManager>();
            
//         // Kiểm tra nếu gameManager không tồn tại thì không thực hiện gì
//         if (gameManager == null) return;

//         // Thêm coins và health items vào player
//         if (other.tag == "Player")
//         {
//             gameManager.AddCoins(coinValue);
//             Instantiate(collectEffect, transform.position, transform.rotation);
//             gameManager.AddHealthItem(healthValue);
//             Instantiate(collectEffect, transform.position, transform.rotation);

//             // Thêm item vào inventory
//             gameManager.AddItemToInventory(item);

//             // Hủy gameobject này
//             Destroy(gameObject);
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int healthValue;
    public int coinValue;
    public GameObject collectEffect;
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                // gameManager.AddCoins(coinValue);
                // Instantiate(collectEffect, transform.position, transform.rotation);
                gameManager.AddHealthItem(healthValue);
                Instantiate(collectEffect, transform.position, transform.rotation);

                if (item != null)
                {
                    bool itemAdded = gameManager.TryAddItemToInventory(item);
                    if (itemAdded)
                    {
                        // Chỉ hủy gameObject nếu item được thêm vào kho đồ thành công
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Inventory is full. Cannot add item.");
                    }
                }
                else
                {
                    // Hủy gameObject nếu không có item để thêm vào kho đồ
                    Destroy(gameObject);
                }
            }
        }
    }
}
