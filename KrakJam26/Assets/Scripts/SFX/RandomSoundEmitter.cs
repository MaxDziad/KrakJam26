using UnityEngine;

public class RandomSoundEmitter : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundClips;
    [SerializeField] protected AudioSource audioSource;

    [SerializeField] private float minPitch = 1.0f;
    [SerializeField] private float maxPitch = 1.0f;

    public void Emit()
    {
        if (soundClips.Length == 0)
        {
            return;
        }

        audioSource.pitch = Random.Range(minPitch, maxPitch);

        int randomIndex = Random.Range(0, soundClips.Length);
        AudioClip selectedClip = soundClips[randomIndex];
        audioSource.clip = selectedClip;
        audioSource.PlayOneShot(selectedClip);
    }
}
