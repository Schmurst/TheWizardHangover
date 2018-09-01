using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;

public class PostProcessingDrink : MonoBehaviour {

    public float BloomValue = 1.0f;
    public float BloomThreshold = 0.5f;
    public float ChromaticAberrationValue = 1.0f;
    public float HueShift = 180.0f;

    public float EffectSpeed = 1f;

    private float InitialBloomValue;
    private float InitialThreshold;
    private float InitialChromaticAberrationValue;
    private float InitialHueShift;

    public LeanTweenType AnimationCurveType = LeanTweenType.easeOutQuad;
    
    private void Start()
    {
        var PPProfile = GetComponent<PostProcessingBehaviour>().profile;

        var bloomsettings = PPProfile.bloom.settings;
        InitialBloomValue = bloomsettings.bloom.intensity;
        InitialThreshold = bloomsettings.bloom.threshold;

        var chromaticsettings = PPProfile.chromaticAberration.settings;
        InitialChromaticAberrationValue = chromaticsettings.intensity;

        var ColourGradsettings = PPProfile.colorGrading.settings;
        InitialHueShift = ColourGradsettings.basic.hueShift;
    }

    public void IncreasePP()
    {
        var post_processing = GetComponent<PostProcessingBehaviour>();
        if (post_processing != null)
        {
            var PPProfile = post_processing.profile;

            if (PPProfile != null)
            {
                //Bloom
                LeanTween.value(gameObject, UpdateBloom, InitialBloomValue, BloomValue, EffectSpeed).setEase(AnimationCurveType);
                LeanTween.value(gameObject, UpdateThreshold, InitialThreshold, BloomThreshold, EffectSpeed).setEase(AnimationCurveType);

                //ChromaticAberration
                LeanTween.value(gameObject, UpdateChromaticAberration, InitialChromaticAberrationValue, ChromaticAberrationValue, EffectSpeed).setEase(AnimationCurveType);

                //HueShift
                LeanTween.value(gameObject, UpdateHueShift, InitialHueShift, HueShift, EffectSpeed).setEase(AnimationCurveType);
            }
        }
    }

    public void DecreasePP()
    {
        var post_processing = GetComponent<PostProcessingBehaviour>();
        if (post_processing != null)
        {
            var PPProfile = post_processing.profile;

            if (PPProfile != null)
            {
                //Bloom
                LeanTween.value(gameObject, UpdateBloom, BloomValue, InitialBloomValue, EffectSpeed).setEase(AnimationCurveType);
                LeanTween.value(gameObject, UpdateThreshold, BloomThreshold, InitialThreshold, EffectSpeed).setEase(AnimationCurveType);

                //ChromaticAberration
                LeanTween.value(gameObject, UpdateChromaticAberration, ChromaticAberrationValue, InitialChromaticAberrationValue, EffectSpeed).setEase(AnimationCurveType);

                //HueShift
                LeanTween.value(gameObject, UpdateHueShift, HueShift, InitialHueShift, EffectSpeed).setEase(AnimationCurveType);
            }
        }
    }

    public void UpdateBloom(float v)
    {
        var PPProfile = GetComponent<PostProcessingBehaviour>().profile;
        var settings = PPProfile.bloom.settings;

        settings.bloom.intensity = v;

        PPProfile.bloom.settings = settings;
    }

    public void UpdateThreshold(float v)
    {
        var PPProfile = GetComponent<PostProcessingBehaviour>().profile;
        var settings = PPProfile.bloom.settings;

        settings.bloom.threshold = v;

        PPProfile.bloom.settings = settings;
    }

    public void UpdateChromaticAberration(float v)
    {
        var PPProfile = GetComponent<PostProcessingBehaviour>().profile;
        var settings = PPProfile.chromaticAberration.settings;

        settings.intensity = v;

        PPProfile.chromaticAberration.settings = settings;
    }

    public void UpdateHueShift(float v)
    {
        var PPProfile = GetComponent<PostProcessingBehaviour>().profile;
        var settings = PPProfile.colorGrading.settings;

        settings.basic.hueShift = v;

        PPProfile.colorGrading.settings = settings;
    }
}
