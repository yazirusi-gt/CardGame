using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    //Œ©‚©‚¯‚ÉŠÖ‚·‚é‚±‚Æ‚ğ‘€ì
    public CardModel model;
    //ƒf[ƒ^‚ÉŠÖ‚·‚é‚±‚Æ‚ğ‘€ì
    CardView cardView;
    //ˆÚ“®‚ÉŠÖ‚·‚é‚±‚Æ‚ğ‘€ì
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
