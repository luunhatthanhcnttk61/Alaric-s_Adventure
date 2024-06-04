using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Inventory/Key Item")]
public class KeyItem : Item
{
    public string sceneToLoad;

    public override void Use()
    {
        base.Use();
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
