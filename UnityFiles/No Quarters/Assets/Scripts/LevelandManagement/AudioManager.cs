using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public bool audioEnable = true;
    bool canToggle = true;
    public void PlayAudio()
    {
        if(audioEnable)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void SetAudioSource(AudioSource source)
    {
        audioSource = source;
    }

    public void SetAudioClip(string nameOfClip)
    {
        audioClip = Resources.Load(nameOfClip) as AudioClip;
    }

    public void ToggleMusic()
    {
        if(canToggle)
        {
            canToggle = false;
            StartCoroutine(WaitToToggle());
            audioEnable = !audioEnable;
        }
        
    }

    IEnumerator WaitToToggle()
    {
        yield return new WaitForSeconds(1);
        canToggle = true;
    }
}
