using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    public KeyItem keyItem;

    public void SetKeyItemScene(string newScene)
    {
        if (keyItem != null)
        {
            keyItem.SetSceneToLoad(newScene);
        }
        else
        {
            Debug.LogError("KeyItem is not assigned in the SceneLoadManager.");
        }
    }

    public void UseKeyItem()
    {
        if (keyItem != null)
        {
            keyItem.Use();
        }
        else
        {
            Debug.LogError("KeyItem is not assigned in the SceneLoadManager.");
        }
    }
}
