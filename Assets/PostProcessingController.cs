using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingController : MonoBehaviour {

    public PostProcessingProfile ppProfile;

    private float focalValue = 50;
    private float hueValue = 0;
    private float chromValue = 0;
    private float grainValue = 0;
    private float vignetteValue = 0;
    private ColorGradingModel.Settings hueSettings;
    private ChromaticAberrationModel.Settings chromSettings;
    private DepthOfFieldModel.Settings focalSettings;
    private GrainModel.Settings grainSettings;
    private VignetteModel.Settings vignetteSettings;

    public void DepthOfFieldAtRuntime(float newFocalValue) {
        // value set to 300
        focalValue = newFocalValue;
        if (!ppProfile.depthOfField.enabled) {
            ppProfile.depthOfField.enabled = true;
        }  
        focalSettings = ppProfile.depthOfField.settings;
        focalSettings.focalLength = focalValue;
        ppProfile.depthOfField.settings = focalSettings;
    }
    public void HueColorAtRuntime(float newHueValue) {
        // randomly generate the vale between -180 - 180 default 10
        hueValue = newHueValue;
        if (!ppProfile.colorGrading.enabled) {
            ppProfile.colorGrading.enabled = true;
        }
        
        hueSettings = ppProfile.colorGrading.settings;
        hueSettings.basic.hueShift = hueValue;
        ppProfile.colorGrading.settings = hueSettings;
    }
    public void ChromaticAtRuntime(float newChromValue) {
        //Value set set to 1
        chromValue = newChromValue;
        if (!ppProfile.chromaticAberration.enabled) {
            ppProfile.chromaticAberration.enabled = true;
        }
        chromSettings = ppProfile.chromaticAberration.settings;
        chromSettings.intensity = chromValue;
        ppProfile.chromaticAberration.settings = chromSettings;
    }
    public void GrainAtRuntime(float newGrainValue) {
        //value set to 1
        grainValue = newGrainValue;
        if (!ppProfile.grain.enabled) {
            ppProfile.grain.enabled = true;
        }
        grainSettings = ppProfile.grain.settings;
        grainSettings.intensity = grainValue;
        ppProfile.grain.settings = grainSettings;
    }
    public void VintageAtRuntime(float newVignetteValue) {
        //value need to be generated randomly 0-0.35
        vignetteValue = newVignetteValue;
        if (!ppProfile.vignette.enabled) {
            ppProfile.vignette.enabled = true;
        }
        vignetteSettings = ppProfile.vignette.settings;
        vignetteSettings.intensity = vignetteValue;
        ppProfile.vignette.settings = vignetteSettings;
    }
    public void ValueReset() {
        HueColorAtRuntime(0);
        DepthOfFieldAtRuntime(50);
        ChromaticAtRuntime(0);
        GrainAtRuntime(0);
        VintageAtRuntime(0);
    }

}
