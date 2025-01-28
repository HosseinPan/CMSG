using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class CardAnimation : MonoBehaviour
{
    public GameObject front;
    public GameObject back;
    public float flipDuration = 0.5f;

    public void FlipCard(bool isShowingFront, Action onFlipComplete = null)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DORotate(new Vector3(0, 90, 0), flipDuration / 2, RotateMode.Fast)
                   .SetEase(Ease.InCubic));

        seq.AppendCallback(() =>
        {
            front.gameObject.SetActive(isShowingFront);
            back.gameObject.SetActive(!isShowingFront);
        });

        seq.Append(transform.DORotate(Vector3.zero, flipDuration / 2, RotateMode.Fast)
                   .SetEase(Ease.OutCubic));

        seq.AppendCallback(() => { onFlipComplete?.Invoke(); });
    }



}
