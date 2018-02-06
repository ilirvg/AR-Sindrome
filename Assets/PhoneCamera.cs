using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

    public RawImage background;
    public RawImage snapground;
    public AspectRatioFitter fit;

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture2D[] snapArray;
    private IEnumerator coroutine;
    private IEnumerator coroutines;
    private int buffer = 10;

	void Start () {

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
        if (Input.GetButtonDown("Fire1")) {
            coroutines = TakeSnapshots();
            StartCoroutine(coroutines);
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

    private void ShowSnapshot() {
        coroutine = AddSnapshots();
        StartCoroutine(coroutine);

        //Make the snaped frame image visable
        float alpha = 0.5f;
        Color currColor = snapground.color;
        currColor.a = alpha;
        snapground.color = currColor;
    }
    private void HideSnapshotLayer() {
        float alpha = 0f;
        Color currColor = snapground.color;
        currColor.a = alpha;
        snapground.color = currColor;
    }

    private IEnumerator TakeSnapshots() {
        snapArray = new Texture2D[buffer];
        for (int i = 0; i < buffer; i++) {
            snapArray[i] = new Texture2D(background.texture.width, background.texture.height);
            snapArray[i].SetPixels(backCam.GetPixels());
            snapArray[i].Apply();
            yield return new WaitForSeconds(0.2f);
        }
        ShowSnapshot();
    }

    private IEnumerator AddSnapshots() {
        for (int i = 0; i < buffer; i++) {
            snapground.texture = snapArray[i];
            yield return new WaitForSeconds(0.2f);
        }
        HideSnapshotLayer();
    }
}
