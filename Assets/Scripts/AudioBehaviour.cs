using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
    public static AudioBehaviour Instance;
    AudioSource ada;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        ada = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayASound(AudioClip nono, float vol, float pitch)
    {
        ada.pitch = pitch;
        ada.clip = nono;
        ada.PlayOneShot(nono, vol);
    }
}
