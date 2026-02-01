using System;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
public class TransitionFade : MonoBehaviour
{
    [SerializeField] private Image blackFadeScreen;

    [SerializeField] private TweenSettings fadeOutSettings;
    [SerializeField] private TweenSettings fadeInSettings;

    [SerializeField] private Color fadeColor = Color.black;

    [SerializeField] private bool fadeInOnStart = false;

    private Tween fadeTween;

    private static TransitionFade _instance;

    private void Awake()
    {
        _instance = this;
    }

    private Tween FadeOutTween()
    {
        Color fadeOutOpaqueColor = fadeColor;
        fadeOutOpaqueColor.a = 1.0f;

        return Tween.Color(
            blackFadeScreen,
            new TweenSettings<Color>(blackFadeScreen.color, fadeOutOpaqueColor, fadeOutSettings)
        );
    }

    private Tween FadeInTween()
    {
        Color fadeInTransparentColor = fadeColor;
        fadeInTransparentColor.a = 0.0f;

        return Tween.Color(
            blackFadeScreen,
            new TweenSettings<Color>(blackFadeScreen.color, fadeInTransparentColor, fadeInSettings)
        );
    }

    public static void Transition(Action onFadedOut = null)
    {
        if(_instance == null)
        {
            Debug.LogWarning("No TransitionFade instance found in the scene to perform transition.");
            onFadedOut?.Invoke();
            return;
        }

        _instance.fadeTween.Stop();
        
        _instance.fadeTween = _instance.FadeOutTween().OnComplete(() =>
        {
            _instance.fadeTween = _instance.FadeInTween();
            onFadedOut?.Invoke();
        });
    }

    private void Start()
    {
        if (fadeInOnStart)
        {
            Color fadeOutOpaqueColor = fadeColor;
            fadeOutOpaqueColor.a = 1.0f;
            blackFadeScreen.color = fadeOutOpaqueColor;

            fadeTween = FadeInTween();
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}