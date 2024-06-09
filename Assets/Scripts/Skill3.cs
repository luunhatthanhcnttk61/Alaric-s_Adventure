using UnityEngine;

public class Skill3 : MonoBehaviour
{
    public Sword sword; 
    public int manaCost = 40;

    public void UseSkill3()
    {
        if (sword != null)
        {
            sword.ReduceMana(manaCost);
            sword.Magic(); 
        }
    }
}
