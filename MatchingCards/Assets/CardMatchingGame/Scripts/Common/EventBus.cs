using System;

public static class EventBus
{
    public static event Action<CardShownEventData> OnCardShown;
    public static event Action<int, int> OnCardMatched;
    public static event Action<int, int> OnCardNotMatched;
    public static event Action<int, int, LevelData> OnStartGame;
    public static event Action OnGoMainMenu;
    public static event Action OnGameFinished;
    public static event Action<LevelData> OnSaveLevel;
    public static event Action OnRequestLoadLevel;
    public static event Action<LevelData> OnLoadedLevel;



    public static void RaiseCardShown(CardShownEventData cardShownEventData)
    {
        OnCardShown?.Invoke(cardShownEventData);
    }

    public static void RaiseCardMatched(int cardSlotIndex1, int cardSlotIndex2)
    {
        OnCardMatched?.Invoke(cardSlotIndex1, cardSlotIndex2);
    }

    public static void RaiseCardNotMatched(int cardSlotIndex1, int cardSlotIndex2)
    {
        OnCardNotMatched?.Invoke(cardSlotIndex1, cardSlotIndex2);
    }

    public static void RaiseStartGame(int column, int rows, LevelData levelData)
    {
        OnStartGame?.Invoke(column, rows, levelData);
    }

    public static void RaiseGoMainMenu()
    {
        OnGoMainMenu?.Invoke();
    }

    public static void RaiseGameFinished()
    {
        OnGameFinished?.Invoke();
    }

    public static void RaiseSaveLevel(LevelData levelData)
    {
        OnSaveLevel?.Invoke(levelData);
    }

    public static void RaiseRequestLoadLevel()
    {
        OnRequestLoadLevel?.Invoke();
    }

    public static void RaiseLoadedLevel(LevelData levelData)
    {
        OnLoadedLevel?.Invoke(levelData);
    }
}

public class CardShownEventData
{
    public int cardSlotIndex;
    public int cardDataId;
}
