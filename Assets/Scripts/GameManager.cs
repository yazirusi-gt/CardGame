using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //手札の情報
    [SerializeField] Transform HandPlayerTransform;
    //カードの生成使用する
    [SerializeField] GameObject CardPrefab;
    private void Start()
    {
        CardDraw(HandPlayerTransform);
    }

    private void CardDraw(Transform hand)
    {
        Instantiate(CardPrefab, hand, false);
    }
}
