using UnityEngine;
using UnityEngine.InputSystem;

public class DebugSoundWaveSpawner : MonoBehaviour
{

    [SerializeField] private SoundWaveVFX soundWavePrefab;
    [SerializeField] private InputActionReference spawnAction;

    private void OnEnable()
    {
        if (spawnAction?.action != null)
        {
            spawnAction.action.performed += OnSpawnPerformed;
            spawnAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (spawnAction?.action != null)
        {
            spawnAction.action.performed -= OnSpawnPerformed;
            spawnAction.action.Disable();
        }
    }

    private void OnSpawnPerformed(InputAction.CallbackContext ctx)
    {
        SpawnSoundWaveAtMouse();
    }

    private void SpawnSoundWaveAtMouse()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
        {
            SpawnSoundWaveAt(hit.point);
        }
    }

    private void SpawnSoundWaveAt(Vector3 position)
    {
        var soundWave = Instantiate(soundWavePrefab, position, Quaternion.identity);
    }
}
