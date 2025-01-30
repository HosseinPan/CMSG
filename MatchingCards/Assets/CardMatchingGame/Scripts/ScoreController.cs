using System;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private int matchCardGainScore = 3;
    [SerializeField] private int notMatchCardLoseScore = 1;

    private const string IN_GAME_SCORE_KEY = "InGameScore";
    private const string TOTAL_SCORE_KEY = "TotalScore";

    public int InGameScore
    {
        get
        {
            return PlayerPrefs.GetInt(IN_GAME_SCORE_KEY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(IN_GAME_SCORE_KEY, value);
            PlayerPrefs.Save();
            EventBus.RaiseGetInGameScore(value);
        }
    }

    public int TotalScore
    {
        get
        {
            return PlayerPrefs.GetInt(TOTAL_SCORE_KEY, 0);
        }
        set
        {
            if (value <= 0)
                value = 0;
            
            PlayerPrefs.SetInt(TOTAL_SCORE_KEY, value);
            PlayerPrefs.Save();
            EventBus.RaiseGetTotalScore(value);
        }
    }

    private void OnEnable()
    {
        EventBus.OnRequestInGameScore += OnRequestInGameScore;
        EventBus.OnRequestTotalScore += OnRequestTotalScore;
        EventBus.OnCardMatched += OnCardMatched;
        EventBus.OnCardNotMatched += OnCardNotMatched;
        EventBus.OnGameFinished += OnGameFinished;
        EventBus.OnNewLevelCreated += OnNewLevelCreated;
    }

    private void OnDisable()
    {
        EventBus.OnRequestInGameScore -= OnRequestInGameScore;
        EventBus.OnRequestTotalScore -= OnRequestTotalScore;
        EventBus.OnCardMatched -= OnCardMatched;
        EventBus.OnCardNotMatched -= OnCardNotMatched;
        EventBus.OnGameFinished -= OnGameFinished;
        EventBus.OnNewLevelCreated -= OnNewLevelCreated;
    }

    private void OnRequestTotalScore()
    {
        EventBus.RaiseGetTotalScore(TotalScore);
    }

    private void OnRequestInGameScore()
    {
        EventBus.RaiseGetInGameScore(InGameScore);
    }

    private void OnNewLevelCreated()
    {
        InGameScore = 0;
    }

    private void OnGameFinished()
    {
        TotalScore += InGameScore;
    }

    private void OnCardNotMatched(int arg1, int arg2)
    {
        InGameScore -= notMatchCardLoseScore;
    }

    private void OnCardMatched(int arg1, int arg2)
    {
        InGameScore += matchCardGainScore;
    }


}
