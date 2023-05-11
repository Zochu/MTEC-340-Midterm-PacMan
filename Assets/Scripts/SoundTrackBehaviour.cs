using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackBehaviour : MonoBehaviour
{
    public static SoundTrackBehaviour Instance;

    public AudioSource playIt;
    [SerializeField] AudioClip basic;
    [SerializeField] AudioClip fight;
    [SerializeField] float fightLenth;

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
        playIt = GetComponent<AudioSource>();
        fightLenth = fight.length;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (playIt.isPlaying == false)
            BackToNormal();
    }


    private void LateUpdate()
    {
        if(GameBehaviour.Instance.state == State.Power && playIt.clip != fight)
        {
            Debug.Log("Let's Fight");
            LetsFight();
            Invoke(nameof(BackToNormal), fightLenth + 0.5f);
        }
    }

    void BackToNormal()
    {
        playIt.Stop();
        playIt.clip = basic;
        playIt.Play();
        playIt.loop = true;
    }

    void LetsFight()
    {
        playIt.Stop();
        playIt.clip = fight;
        playIt.Play();
        playIt.loop = false;
    }

    

}
