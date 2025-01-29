using UnityEngine;

public class WinScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventBus.OnGameFinished += ShowScreen;
    }

    private void OnDisable()
    {
        EventBus.OnGameFinished -= ShowScreen;
    }

    private void ShowScreen()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnMeinMenuButonClicked()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;

        EventBus.RaiseGoMainMenu();
    }
}
