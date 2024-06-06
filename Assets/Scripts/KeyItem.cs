using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New KeyItem", menuName = "Inventory/KeyItem")]
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
        RemoveFromInventory();
    }

    public void SetSceneToLoad(string sceneName)
    {
        sceneToLoad = sceneName;
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene to load is not set!");
        }
    }
}
