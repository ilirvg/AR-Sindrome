using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Replay : MonoBehaviour {
    private const int bufferFrames = 100;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private GameManager manger;
    RawImage ri;
    Texture rit;

    void Start () {
        manger = FindObjectOfType<GameManager>();
        ri = GetComponent<RawImage>();
        rit = GetComponent<RawImage>().texture;
    }
	
	void Update () {
        if (manger.recording) {
            Record();
        }
        else {
            PlayBack();
        }
    }
    private void PlayBack() {
        int frame = Time.frameCount % bufferFrames;
        print("Reading frames " + frame);
        rit = keyFrames[frame].texture;
    }

    private void Record() {
        int frame = Time.frameCount % bufferFrames;
        float time = Time.time;
        print("Writing frames " + frame);
        keyFrames[frame] = new MyKeyFrame(time, rit);
    }

    public struct MyKeyFrame {
        public float frameTime;
        public Texture texture;
        public MyKeyFrame(float aTime, Texture text) {
            frameTime = aTime;
            texture = text;
        }
    }
}
