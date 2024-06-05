using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public SceneLoadManager sceneLoadManager;
    public string nextSceneName;

    void Start()
    {
        sceneLoadManager.SetKeyItemScene(nextSceneName);
    }
}
