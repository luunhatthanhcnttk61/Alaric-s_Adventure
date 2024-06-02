using System.Collections;
using UnityEngine;

public class SkillAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip skillAudioClip;
    public AudioSource hurtSource;
    public AudioClip hurtAudioClip;

    void Start()
    {
        audioSource.clip = skillAudioClip;
        hurtSource.clip = hurtAudioClip;
        audioSource.playOnAwake = false;
        hurtSource.playOnAwake = false;
    }

    public void PlaySkillSound(float startTime, float duration)
    {
        StartCoroutine(PlayClipSegment(startTime, duration));
    }

    private IEnumerator PlayClipSegment(float startTime, float duration)
    {
        audioSource.time = startTime;
        audioSource.Play();
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
    }
    public void PlayHurtSound(float startTime, float duration)
    {
        StartCoroutine(PlayHurtClipSegment(startTime, duration));
    }

    private IEnumerator PlayHurtClipSegment(float startTime, float duration)
    {
        hurtSource.time = startTime;
        hurtSource.Play();
        yield return new WaitForSeconds(duration);
        hurtSource.Stop();
    }
    public void PlaySkill1Sound()
    {
        Debug.Log("Audio skill1");
        PlaySkillSound(2f, 1f); 
    }

    public void PlaySkill2Sound()
    {
        Debug.Log("audio skill2");
        PlaySkillSound(3.5f, 3f); 
    }

    public void PlaySkill3Sound()
    {
        Debug.Log("Audio skill 3");
        PlaySkillSound(2f, 1f);
    }

    public void PlaySkill4Sound()
    {
        PlaySkillSound(3f, 3f);
    }
    public void HurtSound()
    {
        Debug.Log("Hurt Sound");
        PlayHurtSound(0f, 1f); 
    }
}
