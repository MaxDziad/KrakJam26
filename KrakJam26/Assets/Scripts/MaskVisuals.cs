using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MaskVisuals : MonoBehaviour
{
    [Header("Tween Settings")]
    [SerializeField] private TweenSettings<float> blindTweenSettings;
    [SerializeField] private TweenSettings<float> deafTweenSettings;
    [SerializeField] private TweenSettings<float> muteTweenSettings;

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
        SoundWaveVFX.GlobalOpacity = 1.0f - deafVisualState;
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
            .Group(Tween.Custom(blindTweenSettings.WithDirection(maskType is MaskType.Blind), value => blindVisualState = value))
            .Group(Tween.Custom(deafTweenSettings.WithDirection(maskType is MaskType.Deaf), value => deafVisualState = value))
            .Group(Tween.Custom(muteTweenSettings.WithDirection(maskType is MaskType.Silent), value => muteVisualState = value))
        .OnComplete(onComplete);
    }
}