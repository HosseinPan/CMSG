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

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventBus.OnStartGame += StartGame;
    }

    private void OnDisable()
    {
        EventBus.OnStartGame -= StartGame;
    }

    private void StartGame(int columns, int rows)
    {
        SetCards(columns, rows);
        SetGridCellSize(columns, rows);
        EnableCanvasGroup();
    }

    private void SetCards(int columns, int rows)
    {
        foreach (Transform child in gridLayout.transform)
        {
            child.gameObject.SetActive(false);
        }

        int cardsCount = columns * rows;
        for (int i = 0; i < cardsCount; i++) 
        {
            gridLayout.transform.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = 0; i < cardsCount; i++)
        {
            int randomCard = UnityEngine.Random.Range(0, cardsData.Length);
            gridLayout.transform.GetChild(i).GetComponent<Card>().ResetCard(cardsData[randomCard]);
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
