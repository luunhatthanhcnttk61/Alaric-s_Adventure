using UnityEngine;
using System.Collections;

public class BotAudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip takeDamageClip; 
    public AudioClip dieClip;

    void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned.");
        }
        if (takeDamageClip == null)
        {
            Debug.LogError("TakeDamageClip is not assigned.");
        }
        if (dieClip == null)
        {
            Debug.LogError("DieClip is not assigned.");
        }
    }

    public void PlayTakeDamageSound(float startTime, float duration)
    {
        if (audioSource != null && takeDamageClip != null)
        {
            StartCoroutine(PlayClipSegment(takeDamageClip, startTime, duration));
        }
    }

    public void PlayDieSound(float startTime, float duration)
    {
        if (audioSource != null && dieClip != null)
        {
            StartCoroutine(PlayClipSegment(dieClip, startTime, duration));
        }
    }

    private IEnumerator PlayClipSegment(AudioClip clip, float startTime, float duration)
    {
        audioSource.clip = clip;
        audioSource.time = startTime;
        audioSource.Play();
        yield return new WaitForSeconds(duration);
        audioSource.Stop();
    }
}
