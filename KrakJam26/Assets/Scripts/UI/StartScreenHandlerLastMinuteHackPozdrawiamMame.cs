using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenHandlerLastMinuteHackPozdrawiamMame : MonoBehaviour
{
    [SerializeField] private GameObject cover1;
    [SerializeField] private GameObject cover2;
    [SerializeField] private GameObject cover3;
    [SerializeField] private GameObject startTextCover;

    [SerializeField] private float cover1Delay = 0.5f;
    [SerializeField] private float cover2Delay = 1.0f;
    [SerializeField] private float cover3Delay = 1.5f;
    [SerializeField] private float startTextInterval = 2.0f;

    [SerializeField] private AudioSource playerControllerThingy;
    [SerializeField] private string sceneToLoad;

    private void Update()
    {
        bool anyPressed = false;
        var kb = UnityEngine.InputSystem.Keyboard.current;
        if (kb != null) anyPressed |= kb.anyKey.wasPressedThisFrame;

        if (playerControllerThingy != null)
        {
            float t = playerControllerThingy != null && playerControllerThingy.isPlaying ? playerControllerThingy.time : 99999.999f;

            cover1.SetActive(t < cover1Delay);
            cover2.SetActive(t < cover2Delay);
            cover3.SetActive(t < cover3Delay);
            startTextCover.SetActive(Time.time % startTextInterval < startTextInterval * 0.5f);

            anyPressed = anyPressed && t > cover3Delay;
        }

        if (anyPressed)
        {
            TransitionFade.Transition(() => SceneManager.LoadScene(sceneToLoad));
        }
    }
}
