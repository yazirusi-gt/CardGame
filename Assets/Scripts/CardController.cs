using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    //�������Ɋւ��邱�Ƃ𑀍�
    //�f�[�^�Ɋւ��邱�Ƃ𑀍�
    CardModel model;
    CardView cardView;

    public void Awake()
    {
        cardView = GetComponent<CardView>();
    }

    public void Init(int cardID)
    {
        model = new CardModel(cardID);
        cardView.Show(model);
    }
}
