using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public Sword sword; 
    public int manaCost = 30;

    public void UseSkill2()
    {
        if (sword != null)
        {
            sword.ReduceMana(manaCost);
            sword.ComboAttack(); 
        }
    }
}
