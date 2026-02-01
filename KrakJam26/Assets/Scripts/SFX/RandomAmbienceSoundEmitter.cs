using UnityEngine;

public class RandomAmbienceSoundEmitter : RandomSoundEmitter
{
    [SerializeField] private Vector3 positionRange;

    [SerializeField] private float maxDelayBetweenSounds = 5.0f;

    private Vector3 initialPosition;
    private float nextEmitTime = 0.0f;

    private void Start()
    {
        initialPosition = transform.position;
        ScheduleNextEmit();
    }

    private void ScheduleNextEmit()
    {
        float clipLength = audioSource.clip != null ? audioSource.clip.length : 0.0f;
        nextEmitTime = Time.time + clipLength + Random.Range(0.0f, maxDelayBetweenSounds);
    }

    private void Update()
    {
        if (Time.time >= nextEmitTime)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-positionRange.x, positionRange.x),
                Random.Range(-positionRange.y, positionRange.y),
                Random.Range(-positionRange.z, positionRange.z)
            );
            transform.position = initialPosition + randomOffset;

            Emit();
            ScheduleNextEmit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, positionRange * 2);
    }
}