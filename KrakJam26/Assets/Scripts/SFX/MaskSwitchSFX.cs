using System.Collections.Generic;
using UnityEngine;

public class MaskSwitchSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float silentPitch = 1.0f;
    [SerializeField] private float deafPitch = 0.8f;
    [SerializeField] private float blindPitch = 1.2f;

    private void Start()
    {
        MaskSystemManager.Instance.OnMaskChangeStartedEvent += HandleMaskChanged;
    }

    private void OnDestroy()
    {
        MaskSystemManager.Instance.OnMaskChangeStartedEvent -= HandleMaskChanged;
    }

    private void HandleMaskChanged(MaskType newMask)
    {
        audioSource.pitch = newMask switch
        {
            MaskType.Silent => silentPitch,
            MaskType.Deaf => deafPitch,
            MaskType.Blind => blindPitch,
            _ => 1.0f,
        };
        audioSource.Play();
    }
}
