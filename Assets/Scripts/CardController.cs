using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardController : MonoBehaviour
{
    //Œ©‚©‚¯‚ÉŠÖ‚·‚é‚±‚Æ‚ğ‘€ì
    //ƒf[ƒ^‚ÉŠÖ‚·‚é‚±‚Æ‚ğ‘€ì
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
