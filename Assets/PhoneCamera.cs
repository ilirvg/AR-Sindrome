using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PhoneCamera : MonoBehaviour {

    public RawImage background;
    public RawImage snapground;
    public AspectRatioFitter fit;

    private PostProcessingController postProcessingController;
    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture2D[] snapArray;
    private int buffer = 10;

	void Start () {
        postProcessingController = FindObjectOfType<PostProcessingController>();
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
        if (Input.GetKeyDown(KeyCode.R)) {
            postProcessingController.ValueReset();
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            float randomValue = UnityEngine.Random.Range(-180f, 180f);
            postProcessingController.HueColorAtRuntime(1);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            postProcessingController.DepthOfFieldAtRuntime(300);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            postProcessingController.ChromaticAtRuntime(1);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            postProcessingController.GrainAtRuntime(1);
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            StartCoroutine(Vignette());
        }
        if (Input.GetButtonDown("Fire1")) {
            StartCoroutine(TakeSnapshots());
        }

        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;

        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }

    private void ShowSnapshot() {
        StartCoroutine(AddSnapshots());

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

    // record frames
    private IEnumerator TakeSnapshots() {
        snapArray = new Texture2D[buffer];                                                      //pre difine size of the array
        for (int i = 0; i < buffer; i++) {
            snapArray[i] = new Texture2D(background.texture.width, background.texture.height);  // populate the array with new textures/ frames
            snapArray[i].SetPixels(backCam.GetPixels());                                        // make the freshly create texture same as the frame that was rendered from camera
            snapArray[i].Apply();
            yield return new WaitForSeconds(0.2f);
        }
        ShowSnapshot();
    }

    // add the recorded frame to the RawImage / display them
    private IEnumerator AddSnapshots() {
        for (int i = 0; i < buffer; i++) {
            snapground.texture = snapArray[i];                                                  // add the textures from the array to the RawImage so they will be visable
            yield return new WaitForSeconds(0.2f);
        }
        HideSnapshotLayer();
    }
    
    // is called when the post processing effect of vignette is added
    private IEnumerator Vignette() {
        for (int i = 0; i < 20; i++) {
            float randomValue = UnityEngine.Random.Range(0, 0.35f);
            postProcessingController.VignetteAtRunTime(randomValue);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
