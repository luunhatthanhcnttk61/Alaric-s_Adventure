using UnityEngine;

public class Skill1 : MonoBehaviour
{
    public Sword sword; 
    public int manaCost = 10; 
    public void UseSkill1()
    {
        if (sword != null)
        {
            sword.ReduceMana(manaCost);
            sword.BasicAttack(); 
        }
    }
}

