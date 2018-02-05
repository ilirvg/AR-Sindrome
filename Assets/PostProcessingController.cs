using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingController : MonoBehaviour {

    public PostProcessingProfile ppProfile;
    public float focalValue = 50;
    public float hueValue = 0;
    private ColorGradingModel.Settings hueSettings;
    private DepthOfFieldModel.Settings focalSettings;


    public void Start () {
        //DepthOfFieldAtRuntime();
        //HueColorAtRuntime();
    }

    public void DepthOfFieldAtRuntime(float newFocalValue) {
        focalValue = newFocalValue;
        ppProfile.depthOfField.enabled = true;
        focalSettings = ppProfile.depthOfField.settings;
        focalSettings.focalLength = focalValue;
        ppProfile.depthOfField.settings = focalSettings;
    }
    public void HueColorAtRuntime(float newHueValue) {
        hueValue = newHueValue;
        ppProfile.colorGrading.enabled = true;
        hueSettings = ppProfile.colorGrading.settings;
        hueSettings.basic.hueShift = hueValue;
        ppProfile.colorGrading.settings = hueSettings;
    }

    public void ValueReset() {
        HueColorAtRuntime(0);
        DepthOfFieldAtRuntime(50);
    }

}
