using TMPro;
using UnityEngine;

public class TotalScoreView : MonoBehaviour
{
    private TextMeshProUGUI totalScore;

    private void OnEnable()
    {
        EventBus.OnGetTotalScore += OnGetTotalScore;
    }

    private void OnDisable()
    {
        EventBus.OnGetTotalScore -= OnGetTotalScore;
    }

    private void Awake()
    {
        totalScore = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        EventBus.RaiseRequestTotalScore();
    }

    private void OnGetTotalScore(int score)
    {
        totalScore.text = score.ToString();
    }
}
