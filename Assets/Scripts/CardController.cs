using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    //�������Ɋւ��邱�Ƃ𑀍�
    public CardModel model;
    //�f�[�^�Ɋւ��邱�Ƃ𑀍�
    CardView cardView;
    //�ړ��Ɋւ��邱�Ƃ𑀍�
    public CardMovement movement;
    public void Awake()
    {
        cardView = GetComponent<CardView>();
        movement = GetComponent<CardMovement>();
    }

    public void Init(int cardID)
    {
        model = new CardModel(cardID);
        cardView.Show(model);
    }
}
