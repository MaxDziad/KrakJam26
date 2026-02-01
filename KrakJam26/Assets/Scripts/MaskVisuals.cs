using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MaskVisuals : MonoBehaviour
{
    [Header("Visual Settings")]
    [SerializeField] private float visibilityAffectOnSoundWaves = 0.9f;
    [Header("Tween Settings")]
    [SerializeField] private TweenSettings blindTweenSettings;
    [SerializeField] private TweenSettings deafTweenSettings;
    [SerializeField] private TweenSettings muteTweenSettings;

    [Header("References")]
    [SerializeField] private FullScreenPassRendererFeature blindnessRenderPass;

    private MaskType targetMaskType;
    private float blindVisualState = 0.0f;
    private float deafVisualState = 0.0f;
    private float muteVisualState = 0.0f;
    
    private Sequence visualSequence;
    private Material blindnessOriginalMaterial;
    
    public bool IsChangeInProgress => visualSequence.isAlive;

    private void OnEnable()
    {
        blindnessOriginalMaterial = blindnessRenderPass.passMaterial;
        blindnessRenderPass.passMaterial = new Material(blindnessOriginalMaterial);
        SoundWaveVFX.GlobalOpacity = 1.0f;
    }

    private void OnDisable()
    {
        Destroy(blindnessRenderPass.passMaterial);
        blindnessRenderPass.passMaterial = blindnessOriginalMaterial;
        blindnessOriginalMaterial = null;
        SoundWaveVFX.GlobalOpacity = 1.0f;
    }

    private void Update()
    {
        float maxSoundWaveOpacity = Mathf.Lerp(1.0f - visibilityAffectOnSoundWaves, 1.0f, blindVisualState);
        SoundWaveVFX.GlobalOpacity = Mathf.Min(maxSoundWaveOpacity, 1.0f - deafVisualState);
        blindnessRenderPass.passMaterial.SetFloat("_Value", blindVisualState);
    }

    public void StartVisualChange(MaskType maskType, Action onComplete = null)
    {
        targetMaskType = maskType;

        if (visualSequence.isAlive)
        {
            visualSequence.Stop();
        }

        visualSequence = Sequence.Create()
            .Group(Tween.Custom(new TweenSettings<float>(blindVisualState, 1.0f, blindTweenSettings), value => blindVisualState = value))
            .Group(Tween.Custom(new TweenSettings<float>(deafVisualState, 1.0f, deafTweenSettings), value => deafVisualState = value))
            .Group(Tween.Custom(new TweenSettings<float>(muteVisualState, 1.0f, muteTweenSettings), value => muteVisualState = value))
        .OnComplete(() => OnVisualMidpointReached(onComplete)); 
    }

    private void OnVisualMidpointReached(Action onComplete = null)
    {
        var maskTexture = MaskSystemManager.Instance.MasksData.GetMaskSprite(targetMaskType).texture;
        blindnessRenderPass.passMaterial.SetTexture("_Mask", maskTexture);

        float blindTarget = GetBlindTarget(targetMaskType);
        float deafTarget = GetDeafTarget(targetMaskType);
        float muteTarget = GetMuteTarget(targetMaskType);

        visualSequence = Sequence.Create()
            .Chain(Tween.Custom(new TweenSettings<float>(1.0f, blindTarget, blindTweenSettings), value => blindVisualState = value))
            .Group(Tween.Custom(new TweenSettings<float>(1.0f, deafTarget, deafTweenSettings), value => deafVisualState = value))
            .Group(Tween.Custom(new TweenSettings<float>(1.0f, muteTarget, muteTweenSettings), value => muteVisualState = value))
        .OnComplete(onComplete);
    }

    private float GetBlindTarget(MaskType maskType)
    {
        return (maskType is MaskType.Blind or MaskType.None) ? 1.0f : 0.0f;
    }

    private float GetDeafTarget(MaskType maskType)
    {
        return maskType switch
        {
            MaskType.Deaf => 1.0f,
            MaskType.None => 1.0f,
            not MaskType.Blind => 0.5f,
            _ => 0.0f,
        };
    }

    private float GetMuteTarget(MaskType maskType)
    {
        return (maskType is MaskType.Silent or MaskType.None) ? 1.0f : 0.0f;
    }
}