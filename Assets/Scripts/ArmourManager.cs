using UnityEngine;
using UnityEngine.UI;

public class ArmourManager : MonoBehaviour
{
    public Image armourIcon;
    public Text armourText;

    public void UpdateArmour(int currentArmour)
    {
        armourText.text = currentArmour.ToString();
    }
}
