using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarManager : MonoBehaviour
{
    public Image manaBarFill;
    public PlayerController2 player;
    public Text manaValue;
    // Start is called before the first frame update
    void Start()
    {
        if (manaBarFill == null)
        {
            Debug.LogError("ManaBar fill image is not assigned in the Inspector.");
        }
        if (player == null)
        {
            player = FindObjectOfType<PlayerController2>();
            if (player == null)
            {
                Debug.LogError("PlayerController2 script is not found in the scene.");
            }
        }
        
    }
    public void Update()
    {
        manaValue.text = player.currentMana + "/" + player.maxMana;
    }

    public void UpdateMana()
    {
        manaBarFill.fillAmount = (float)player.currentMana / player.maxMana;
        manaValue.text = player.currentMana + "/" + player.maxMana;
    }

    public void SetColor(Color color)
    {
        manaBarFill.color = color;
    }
}
