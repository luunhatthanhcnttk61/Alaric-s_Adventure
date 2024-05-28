    // using System.Collections;
    // using System.Collections.Generic;
    // using UnityEngine;

    // public class CollectItem : MonoBehaviour
    // {
    //     public int healthValue;
    //     public int coinValue;
    //     public GameObject collectEffect;


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
                
    //             // Kiểm tra nếu gameManager không tồn tại thì không thực hiện gì
    //             if (gameManager == null) return;

    //             // Thêm coins và health items vào player
    //             if(other.tag == "Player"){
    //                 gameManager.AddCoins(coinValue);
    //                 Instantiate(collectEffect, transform.position, transform.rotation);
    //                 gameManager.AddHealthItem(healthValue);
    //                 Instantiate(collectEffect, transform.position, transform.rotation);
                    
    //                 // Hủy gameobject này
    //                 Destroy(gameObject); 
    //             }
                
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
    public Item item; // Tham chiếu đến item để thêm vào inventory

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
            
        // Kiểm tra nếu gameManager không tồn tại thì không thực hiện gì
        if (gameManager == null) return;

        // Thêm coins và health items vào player
        if (other.tag == "Player")
        {
            gameManager.AddCoins(coinValue);
            Instantiate(collectEffect, transform.position, transform.rotation);
            gameManager.AddHealthItem(healthValue);
            Instantiate(collectEffect, transform.position, transform.rotation);

            // Thêm item vào inventory
            gameManager.AddItemToInventory(item);

            // Hủy gameobject này
            Destroy(gameObject);
        }
    }
}
