using DG.Tweening;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{

    private CanvasGroup canvasGroup;

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
        StartGame(2, 2);
    }

    public void OnLayout2x3Clicked()
    {
        StartGame(2, 3);
    }

    public void OnLayout5x6Clicked()
    {
        StartGame(5, 6);
    }

    private void StartGame(int column, int rows)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0f, 1).SetEase(Ease.Linear);

        EventBus.RaiseStartGame(column, rows);
    }

    private void ShowScreen()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
