using TMPro;
using UnityEngine;

public class InGameScoreView : MonoBehaviour
{
    private TextMeshProUGUI inGameScore;

    private void OnEnable()
    {
        EventBus.OnGetInGameScore += OnGetInGameScore;
    }

    private void OnDisable()
    {
        EventBus.OnGetInGameScore -= OnGetInGameScore;
    }

    private void Awake()
    {
        inGameScore = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        EventBus.RaiseRequestInGameScore();
    }

    private void OnGetInGameScore(int score)
    {
        inGameScore.text = score.ToString();
    }
}
