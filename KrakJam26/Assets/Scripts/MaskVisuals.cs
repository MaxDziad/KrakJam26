using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MaskVisuals : MonoBehaviour
{
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

        float blindTarget = (targetMaskType == MaskType.Blind) ? 1.0f : 0.0f;
        float deafTarget = (targetMaskType == MaskType.Deaf) ? 1.0f : 0.0f;
        float muteTarget = (targetMaskType == MaskType.Silent) ? 1.0f : 0.0f;

        visualSequence = Sequence.Create()
            .Group(Tween.Custom(new TweenSettings<float>(blindVisualState, blindTarget, blindTweenSettings), value => blindVisualState = value))
            .Group(Tween.Custom(new TweenSettings<float>(deafVisualState, deafTarget, deafTweenSettings), value => deafVisualState = value))
            .Group(Tween.Custom(new TweenSettings<float>(muteVisualState, muteTarget, muteTweenSettings), value => muteVisualState = value))
        .OnComplete(onComplete);
    }
}