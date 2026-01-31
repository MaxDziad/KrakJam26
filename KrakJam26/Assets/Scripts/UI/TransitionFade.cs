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

    private Tween fadeTween;


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

    public void Transition(Action onFadedOut = null)
    {
        fadeTween.Stop();
        
        fadeTween = FadeOutTween().OnComplete(() =>
        {
            fadeTween = FadeInTween();
            onFadedOut?.Invoke();
        });
    }
}
