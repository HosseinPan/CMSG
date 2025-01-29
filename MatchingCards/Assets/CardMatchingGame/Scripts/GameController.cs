using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private RectTransform cardsContainer;
    [SerializeField] private CardDataSO[] cardsData;

    private CanvasGroup canvasGroup;
    private List<int> tempCardIndexes = new List<int>();
    private List<int> tempCardDataIndexes = new List<int>();
    private List<Card> cards = new List<Card>();

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventBus.OnStartGame += StartGame;
        EventBus.OnCardMatched += CheckGameFinished;
    }

    private void OnDisable()
    {
        EventBus.OnStartGame -= StartGame;
        EventBus.OnCardMatched -= CheckGameFinished;
    }

    private void StartGame(int columns, int rows)
    {
        SetCards(columns, rows);
        SetGridCellSize(columns, rows);
        EnableCanvasGroup();
    }

    private void CheckGameFinished(int a, int b)
    {
        StartCoroutine(WaitForCheckGameFinished());
    }

    IEnumerator WaitForCheckGameFinished()
    {
        yield return new WaitForSeconds(0.1f);

        bool gameFinished = true;
        foreach (var card in cards)
        {
            if (card.State != Card.CardState.Done)
            {
                gameFinished = false;
                break;
            }
        }

        if (gameFinished)
        {
            yield return new WaitForSeconds(2f);
            EventBus.RaiseGameFinished();
        }
    }

    private void SetCards(int columns, int rows)
    {
        foreach (Transform child in gridLayout.transform)
        {
            child.gameObject.SetActive(false);
        }

        cards.Clear();
        int cardsCount = columns * rows;
        for (int i = 0; i < cardsCount; i++) 
        {
            var card = gridLayout.transform.GetChild(i).GetComponent<Card>();
            card.gameObject.SetActive(true);
            cards.Add(card);
        }

        tempCardIndexes.Clear();
        tempCardDataIndexes.Clear();

        int cardDataCount = cardsCount / 2;
        for (int i = 0; i < cardDataCount; i++)
        {
            int randomCardDataIndex = Random.Range(0, cardsData.Length);
            tempCardDataIndexes.Add(randomCardDataIndex);
        }
        for (int i = 0; i < cardsCount; i++)
        {
            tempCardIndexes.Add(i);
        }
        for (int i = 0; i < cardDataCount; i++)
        {
            int firstRandomIndex = Random.Range(0, tempCardIndexes.Count);
            cards[tempCardIndexes[firstRandomIndex]].ResetCard(cardsData[tempCardDataIndexes[i]]);
            tempCardIndexes.RemoveAt(firstRandomIndex);

            int secondRandomIndex = Random.Range(0, tempCardIndexes.Count);
            cards[tempCardIndexes[secondRandomIndex]].ResetCard(cardsData[tempCardDataIndexes[i]]);
            tempCardIndexes.RemoveAt(secondRandomIndex);
        }
    }

    private void SetGridCellSize(int columns, int rows)
    {
        float totalWidth = cardsContainer.rect.width;
        float totalHeight = cardsContainer.rect.height;

        float spacingX = gridLayout.spacing.x;
        float spacingY = gridLayout.spacing.y;

        float widthMinusSpacing = totalWidth - (columns - 1) * spacingX;
        float heightMinusSpacing = totalHeight - (rows - 1) * spacingY;

        float cellWidth = widthMinusSpacing / columns;
        float cellHeight = heightMinusSpacing / rows;

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }

    private void EnableCanvasGroup()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
