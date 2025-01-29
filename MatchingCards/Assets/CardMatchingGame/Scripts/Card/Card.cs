using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardDataSO cardData;
    [SerializeField] private GameObject backCard;
    [SerializeField] private GameObject frontCard;
    [SerializeField] private Image frontImage;
    [SerializeField] private Image frontBackground;

    [Header("MatchedMechanicColors")]
    [SerializeField] private Color naturalColor;
    [SerializeField] private Color matchedColor;
    [SerializeField] private Color notMatchedColor;

    private CardState _state;
    private int _cardSlotId;
    private CardShownEventData _cardShownEventData = new CardShownEventData();
    private CardAnimation _cardAnimation;

    public CardState State => _state;
    public CardDataSO CardData => cardData;

    private void Awake()
    {
        _cardAnimation = GetComponent<CardAnimation>();
    }

    private void OnEnable()
    {
        EventBus.OnCardMatched += OnCardsMatched;
        EventBus.OnCardNotMatched += OnCardsNotMatched;
    }

    private void OnDisable()
    {
        EventBus.OnCardMatched -= OnCardsMatched;
        EventBus.OnCardNotMatched -= OnCardsNotMatched;
    }

    public void ResetCard(CardDataSO cardData)
    {
        this.cardData = cardData;
        frontImage.sprite = cardData.FrontContent;
        _cardSlotId = transform.GetSiblingIndex();
        _state = CardState.Back;
        backCard.SetActive(true);
        frontCard.SetActive(false);
    }

    public void LoadCard(CardDataSO cardData, CardState state)
    {
        if (state == CardState.Done)
        {
            this.cardData = cardData;
            frontImage.sprite = cardData.FrontContent;
            _cardSlotId = transform.GetSiblingIndex();
            _state = CardState.Done;
            backCard.SetActive(false);
            frontCard.SetActive(false);
        }
        else 
        {
            ResetCard(cardData);
        }
    }

    private void OnCardsMatched(int cardSlot1, int cardSlot2)
    {
        Debug.Log("OnCardsMatched");

        if (_cardSlotId != cardSlot1 && _cardSlotId != cardSlot2)
            return;

        frontBackground.color = matchedColor;
        StartCoroutine(CardMatchedAnimation());
    }

    private void OnCardsNotMatched(int cardSlot1, int cardSlot2)
    {
        Debug.Log("OnCardsNotMatched");

        if (_cardSlotId != cardSlot1 && _cardSlotId != cardSlot2)
            return;

        frontBackground.color = notMatchedColor;
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
        frontBackground.color = naturalColor;
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
        _state = CardState.Done;
        yield return new WaitForSeconds(1.5f);      
        backCard.SetActive(false);
        frontCard.SetActive(false);      
    }

}

public enum CardState
{
    Back = 0,
    Front = 1,
    Done = 2
}
