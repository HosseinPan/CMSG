using System;

public static class EventBus
{
    public static event Action<CardShownEventData> OnCardShown;
    public static event Action<int, int> OnCardMatched;
    public static event Action<int, int> OnCardNotMatched;
    public static event Action<int, int> OnStartGame;
    public static event Action OnGoMainMenu;


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

    public static void RaiseStartGame(int width, int height)
    {
        OnStartGame?.Invoke(width, height);
    }

    public static void RaiseGoMainMenu()
    {
        OnGoMainMenu?.Invoke();
    }
}

public class CardShownEventData
{
    public int cardSlotIndex;
    public int cardDataId;
}
