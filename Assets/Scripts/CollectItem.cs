using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public int healthValue;
    public int coinValue;


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
        // if(other.tag == "Player")
        // {
        //     FindObjectOfType<GameManager>().AddHealthItem(healthValue);

        //     Destroy(gameObject); 
        //}
        GameManager gameManager = FindObjectOfType<GameManager>();
            
            // Kiểm tra nếu gameManager không tồn tại thì không thực hiện gì
            if (gameManager == null) return;

            // Thêm coins và health items vào player
            gameManager.AddCoins(coinValue);
            gameManager.AddHealthItem(healthValue);
            
            // Hủy gameobject này
            Destroy(gameObject);    
    }
}
