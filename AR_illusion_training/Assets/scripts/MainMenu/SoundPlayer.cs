using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip ButtonAudio;
    public AudioClip ButtonAudio2;
    public AudioClip InvasionAudio;
    private AudioSource currentButtonAudio;
    private AudioSource currentBGMAudio;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        currentButtonAudio = gameObject.AddComponent<AudioSource>();
        currentBGMAudio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonAudio(){
        currentButtonAudio.clip = ButtonAudio;
        currentButtonAudio.Play();
    }

    public void PlayButton2Audio(){
        currentButtonAudio.clip = ButtonAudio2;
        currentButtonAudio.Play();
    }

    public void PlayInvasionAudio(){
        currentBGMAudio.clip = InvasionAudio;
        currentBGMAudio.Play();
        currentBGMAudio.loop = true;
    }
}
