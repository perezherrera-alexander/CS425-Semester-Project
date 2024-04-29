using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource startMusic;
    public AudioSource loopMusic;
    // Start is called before the first frame update
    void Start()
    {
        //Play the first background music file and then start the other one on loop
        startMusic.Play();
        loopMusic.PlayDelayed(startMusic.clip.length);
    }

    //Update is called once per frame

    void Update()
    {
        //If the loop music is not playing, play it again
        // if (!loopMusic.isPlaying)
        // {
        //     startMusic.volume = (float)SettingsValues.gameVolume / 100.0f;
        // }
        // else
        // {
        //     loopMusic.volume = (float)SettingsValues.gameVolume / 100.0f;
        // }
        
    }
}