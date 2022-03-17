using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixer music, effects;
    public AudioSource soundTrack;
    public static AudioManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(soundTrack);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }

}
