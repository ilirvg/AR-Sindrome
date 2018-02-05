using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture2DArray snap;
    //private Texture2D snap;
    public Texture defaultBackground;

    public RawImage background;
    public RawImage snapground;
    public AspectRatioFitter fit;

	void Start () {
         snapground.texture = defaultBackground;

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0) {
            Debug.Log("There is no camera recognized");
            camAvailable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++) {
            Debug.Log(devices[i].name);
            //if (!devices[i].isFrontFacing) {
            //    backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            //}
            if (devices[i].isFrontFacing) { // TODO Leave the other lines that are commentet above and delete this one
                backCam = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCam == null) {
            Debug.Log("There is not back camera detected");
            return;
        }

        backCam.Play();
        background.texture = backCam;
        background.material.mainTexture = backCam;
        camAvailable = true;
    }
	
	void Update () {
        if (!camAvailable) {
            return;
        }
        if (Input.GetButton("Fire1")) {
            TakeSnapshot();
        }
        if (Input.GetKeyDown("space")) {
            ShowSnapshot();
        }

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    private void TakeSnapshot() {
        int depthValue = 5;
        snap = new Texture2DArray(snapground.texture.width, snapground.texture.height, depthValue, TextureFormat.RGBA32, false);
        Color[] pix = backCam.GetPixels();
        for (int i = 0; i < depthValue; i++) { 
            snap.SetPixels(pix[i], i);
        }
        snap.Apply();
    }

    private void ShowSnapshot() {
        //Add saved frame to the background
        snapground.texture = snap;

        //Make the snaped frame image visable
        float alpha = 0.5f;
        Color currColor = snapground.color;
        currColor.a = alpha;
        snapground.color = currColor;
    }
}
