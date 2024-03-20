using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //��D�̏��
    [SerializeField] Transform HandPlayerTransform;
    //�J�[�h�̐����g�p����
    [SerializeField] CardController CardPrefab;
    private void Start()
    {
        CardDraw(HandPlayerTransform);
    }

    private void CardDraw(Transform hand)
    {
        CardController card = Instantiate(CardPrefab, hand, false);
        card.Init(2);
    }
}
