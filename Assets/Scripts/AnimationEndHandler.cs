using UnityEngine;

public class AnimationEndHandler : MonoBehaviour
{
    private SkillManager skillManager;

    void Start()
    {
        skillManager = FindObjectOfType<SkillManager>();
        if (skillManager == null)
        {
            Debug.LogError("SkillManager not found!");
        }
    }

    public void OnAnimationEnd()
    {
        if (skillManager != null)
        {
            skillManager.OnAnimationEnd();
            Debug.Log("Animation ended, calling SkillManager.OnAnimationEnd()");
        }
    }
}
