using UnityEngine;
using static EventBus;

public class MatchCardsController : MonoBehaviour
{

    private CardShownEventData firstCardShownEventData;
    private CardShownEventData secondCardShownEventData;

    private void OnEnable()
    {
        Reset();
        EventBus.OnCardShown += OnCardShown;
    }

    private void OnDisable()
    {
        EventBus.OnCardShown -= OnCardShown;
    }

    private void Reset()
    {
        firstCardShownEventData = null;
        secondCardShownEventData = null;
    }

    private void OnCardShown(CardShownEventData cardShownEventData)
    {
        Debug.Log("OnCardShown");

        if (firstCardShownEventData == null)
        {
            firstCardShownEventData = cardShownEventData;
        }
        else if (secondCardShownEventData == null)
        {
            secondCardShownEventData = cardShownEventData;

            bool isMatchingCards = CheckMatchingCards(firstCardShownEventData.cardDataId, secondCardShownEventData.cardDataId);
            if (isMatchingCards)
            {
                EventBus.RaiseCardMatched(firstCardShownEventData.cardSlotIndex, secondCardShownEventData.cardSlotIndex);
                EventBus.RaisePlaySfx(SfxType.CardMatch);
            }
            else
            {
                EventBus.RaiseCardMisMatched(firstCardShownEventData.cardSlotIndex, secondCardShownEventData.cardSlotIndex);
                EventBus.RaisePlaySfx(SfxType.CardMisMatch);
            }
        }
        else
        {
            firstCardShownEventData = cardShownEventData;
            secondCardShownEventData = null;
        }
    }

    private bool CheckMatchingCards(int cardDataId1, int cardDataId2)
    {
        return cardDataId1 == cardDataId2;
    }
}
