using Unity.VisualScripting;
using UnityEngine;

public class MovementSoundWaveEmitter : MonoBehaviour
{
    [SerializeField] private float emissionDistanceInterval = 1.0f;
    [SerializeField] private float firstEmissionDistanceInterval = 0.1f;
    [SerializeField] private SoundWaveVFX soundWavePrefab;
    [SerializeField] private float velocityThreshold = 0.1f;
    [SerializeField] private float velocitySnappingFactor = 0.9f;
    [SerializeField] private float soundWaveVelocityInheritanceFactor = 0.5f;
    [SerializeField] private RandomSoundEmitter soundEmitter;

    Vector3 positionLastFrame;
    private Vector3 accumulatedVelocity;
    private float accumulatedDistanceTravelled;

    private void Start()
    {
        positionLastFrame = transform.position;
        accumulatedDistanceTravelled = emissionDistanceInterval - firstEmissionDistanceInterval;
    }

    private void FixedUpdate()
    {
        Vector3 velocityThisFrame = (transform.position - positionLastFrame) / Time.fixedDeltaTime;
        positionLastFrame = transform.position;
        accumulatedVelocity = Vector3.Lerp(accumulatedVelocity, velocityThisFrame, velocitySnappingFactor);
        if (accumulatedVelocity.magnitude < velocityThreshold)
        {
            accumulatedDistanceTravelled = emissionDistanceInterval - firstEmissionDistanceInterval;
            return;
        }
        accumulatedDistanceTravelled += accumulatedVelocity.magnitude * Time.fixedDeltaTime;

        if (accumulatedDistanceTravelled >= emissionDistanceInterval)
        {
            accumulatedDistanceTravelled -= emissionDistanceInterval;
            Emit();
        }
    }

    private void Emit()
    {
        var soundWave = Instantiate(soundWavePrefab, transform.position, Quaternion.identity);
        var soundWaveMovement = soundWave.AddComponent<LinearMovement>();
        soundWaveMovement.velocity = accumulatedVelocity * soundWaveVelocityInheritanceFactor;
        soundEmitter?.Emit();
    }
}
