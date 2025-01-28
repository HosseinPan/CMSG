using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardDataSO cardData;
    [SerializeField] private GameObject backCard;
    [SerializeField] private GameObject frontCard;
    [SerializeField] private Image frontImage;

    private CardState _state;
    private int _cardSlotId;
    private CardShownEventData cardShownEventData = new CardShownEventData();

    private void OnEnable()
    {
        Reset();
        EventBus.OnCardMatched += OnCardsMatched;
        EventBus.OnCardNotMatched += OnCardsNotMatched;
    }

    private void OnDisable()
    {
        EventBus.OnCardMatched -= OnCardsMatched;
        EventBus.OnCardNotMatched -= OnCardsNotMatched;
    }

    public void Reset()
    {
        frontImage.sprite = cardData.FrontContent;
        _cardSlotId = transform.GetSiblingIndex();
        _state = CardState.Back;
        backCard.SetActive(true);
        frontCard.SetActive(false);
    }

    private void OnCardsMatched(int cardSlot1, int cardSlot2)
    {
        Debug.Log("OnCardsMatched");

        if (_cardSlotId != cardSlot1 && _cardSlotId != cardSlot2)
            return;
        
        StartCoroutine(CardMatchedAnimation());
    }

    private void OnCardsNotMatched(int cardSlot1, int cardSlot2)
    {
        Debug.Log("OnCardsNotMatched");

        if (_cardSlotId != cardSlot1 && _cardSlotId != cardSlot2)
            return;

        StartCoroutine(ShowBackAnimation());
    }

    public void OnClick()
    {
        if (_state != CardState.Back)
            return;

        ShowFrontCard();
    }

    private void ShowFrontCard()
    {
        _state = CardState.Front;
        ShowFrontAnimation();

        cardShownEventData.cardSlotIndex = _cardSlotId;
        cardShownEventData.cardDataId = cardData.Id;
        EventBus.RaiseCardShown(cardShownEventData);
    }


    private void ShowFrontAnimation()
    {
        backCard.SetActive(false);
        frontCard.SetActive(true);
        //yield return new WaitForSeconds(0.2f);
    }

    IEnumerator ShowBackAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        _state = CardState.Back;
        backCard.SetActive(true);
        frontCard.SetActive(false);
    }

    IEnumerator CardMatchedAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        _state = CardState.Done;
        backCard.SetActive(false);
        frontCard.SetActive(false);      
    }


    public enum CardState
    {
        Back,
        Front,
        Done
    }
}
