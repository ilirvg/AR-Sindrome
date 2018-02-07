using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    //   public AudioSource audio2;
    //void Start () {
    //       AudioSource audio = GetComponent<AudioSource>();
    //       audio.clip = Microphone.Start(null, true, 100, 44100);
    //       audio.loop = true;
    //       audio.volume = 1;
    //       while (!(Microphone.GetPosition(null) > 0)) { }
    //       Debug.Log("start playing... position is " + Microphone.GetPosition(null));
    //       audio.Play();
    //   }

    public AudioClip clipAmb;
    //public AudioClip clipEngine;


    private AudioSource audioAmb;
    private AudioSource audioEngine;

    public void Awake() {
    AudioClip clipEngine = Microphone.Start(null, true, 100, 44100);
        // add the necessary AudioSources:
    audioAmb = AddAudio(clipAmb, true, true, 0.5f);
    audioEngine = AddAudio(clipEngine, true, true, 1f);

    }
    private void Start() {
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioEngine.Play();
        audioAmb.Play();
    }
    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) {

        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;

        return newAudio;

    }

}
