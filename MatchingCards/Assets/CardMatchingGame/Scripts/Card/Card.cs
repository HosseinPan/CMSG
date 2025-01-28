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
    private CardShownEventData _cardShownEventData = new CardShownEventData();
    private CardAnimation _cardAnimation;

    private void Awake()
    {
        _cardAnimation = GetComponent<CardAnimation>();
    }

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

        StartCoroutine(WaitForShowBackCard());
    }

    IEnumerator WaitForShowBackCard()
    {
        yield return new WaitForSeconds(2f);

        _cardAnimation.FlipCard(false, ShowBackAnimationFinished);
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
        _cardAnimation.FlipCard(true, ShowFrontAnimationFinished);       
    }

    private void ShowFrontAnimationFinished()
    {
        _cardShownEventData.cardSlotIndex = _cardSlotId;
        _cardShownEventData.cardDataId = cardData.Id;
        EventBus.RaiseCardShown(_cardShownEventData);
    }

    private void ShowBackAnimationFinished()
    {
        _state = CardState.Back;
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
