using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private GameObject backCard;
    [SerializeField] private GameObject frontCard;

    private CardState _state;

    private void Awake()
    {
        SetBackCardState();
    }


    public void SetBackCardState()
    {
        _state = CardState.Back;
        backCard.SetActive(true);
        frontCard.SetActive(false);
    }

    public void OnClick()
    {
        if (_state != CardState.Back)
            return;

        _state = CardState.Front;
        PlayAnimationShowCard();
    }

    private void PlayAnimationShowCard()
    {
        backCard.SetActive(false);
        frontCard.SetActive(true);
        StartCoroutine(ShowCardAnimation());
    }

    IEnumerator ShowCardAnimation()
    {
        backCard.SetActive(false);
        frontCard.SetActive(true);
        yield return new WaitForSeconds(2f);
        SetBackCardState();
    }


    public enum CardState
    {
        Back,
        Front,
        Done
    }
}
