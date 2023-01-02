using UnityEngine;
using DG.Tweening;

public class MenuTweening : MonoBehaviour
{
    public Transform titleText;
    public RectTransform menuButtons;
    public float tweenDuration;

    private const int buttonStartPosY = -1200;

    private void Awake()
    {
        TitleTextTweening();
        ButtonTweening();
    }

    private void TitleTextTweening()
    {
        titleText.localScale = Vector3.zero;
        titleText.DOScale(Vector3.one, tweenDuration).SetEase(Ease.Linear);
    }

    private void ButtonTweening()
    {
        menuButtons.anchoredPosition = new Vector2(0, buttonStartPosY);
        menuButtons.DOAnchorPos(Vector2.zero, tweenDuration);
    }
}
