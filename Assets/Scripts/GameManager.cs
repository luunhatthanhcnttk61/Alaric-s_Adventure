using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int healthItems = 0;
    public int coins = 0;
    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealthItem(int healthToAdd)
    {
        healthItems += healthToAdd;
    }

    public void AddCoins(int value)
    {
        coins += value;
        coinsText.text = "Coins: " + coins;
    }
}
