using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    //見かけに関することを操作
    //データに関することを操作
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
