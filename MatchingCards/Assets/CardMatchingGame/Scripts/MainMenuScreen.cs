using DG.Tweening;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    private bool isActive = true;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        ShowScreen();
    }

    private void OnEnable()
    {
        EventBus.OnGoMainMenu += ShowScreen;
    }

    private void OnDisable()
    {
        EventBus.OnGoMainMenu -= ShowScreen;
    }

    public void OnLayout2x2Clicked()
    {
        if (isActive == false)
            return;

        isActive = false;
        StartGame(2, 2);
    }

    public void OnLayout2x3Clicked()
    {
        if (isActive == false)
            return;

        isActive = false;
        StartGame(2, 3);
    }

    public void OnLayout5x6Clicked()
    {
        if (isActive == false)
            return;

        isActive = false;
        StartGame(5, 6);
    }

    private void StartGame(int width, int height)
    {
        FadeOut();
        EventBus.RaiseStartGame(width, height);
    }

    private void FadeOut()
    {
        canvasGroup.DOFade(0f, 1).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
    }

    private void ShowScreen()
    {
        isActive = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
