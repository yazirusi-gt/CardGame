using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    //見かけに関することを操作
    public CardModel model;
    //データに関することを操作
    CardView cardView;
    //移動に関することを操作
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

    public void Attack(CardController enemyCard)
    {
        model.Attack(enemyCard);
        SetCandAttack(false);
    }
    public void SetCandAttack(bool canAttack)
    {
        cardView.SetActiveSelectablePanel(canAttack);
        model.canAttack = canAttack;
    }

    public void CheckAlive()
    {
        if (model.isAlive)
        {
            cardView.Refresh(model);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
