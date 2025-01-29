using DG.Tweening;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject newLevelPanel;
    [SerializeField] private GameObject loadLevelPanel;


    private CanvasGroup canvasGroup;
    private LevelData _levelData;

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
        EventBus.OnLoadedLevel += OnLoadedLevel;
    }

    private void OnDisable()
    {
        EventBus.OnGoMainMenu -= ShowScreen;
        EventBus.OnLoadedLevel -= OnLoadedLevel;
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

    public void ContinueButtonClicked()
    {
        StartGame(0, 0, _levelData);
    }

    private void StartGame(int column, int rows, LevelData levelData = null)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0f, 1).SetEase(Ease.Linear);

        EventBus.RaiseStartGame(column, rows, levelData);
    }

    private void ShowScreen()
    {
        EventBus.RaiseRequestLoadLevel();

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    private void OnLoadedLevel(LevelData levelData)
    {
        _levelData = levelData;
        if (levelData == null)
        {
            newLevelPanel.SetActive(true);
            loadLevelPanel.SetActive(false);
        }
        else
        {
            newLevelPanel.SetActive(false);
            loadLevelPanel.SetActive(true);
        }
    }
}
