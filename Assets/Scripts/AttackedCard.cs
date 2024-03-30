using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//攻撃される側
public class AttackedCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* 攻撃*/
        //攻撃カードを選択
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();
        //防御カードを選択
        CardController defender = GetComponent<CardController>();

        if (attacker == null || defender == null)
        {
            return;
        }
        if(attacker.model.isPlayerCard == defender.model.isPlayerCard)
        {
            return;
        }
        //シールドカードがあればシールドカード以外は攻撃できない。
        CardController[] enemyFieldCards = GameManager.instance.GetEnemyFieldCards();
        if (Array.Exists(enemyFieldCards,
                card => card.model.ability == CardEntity.ABILITY.SHIELD) && defender.model.ability != CardEntity.ABILITY.SHIELD)
        {
            return;
        }
        if (attacker.model.canAttack)
        {
            //attackerとdefenderを戦わせる
            GameManager.instance.CardsBattle(attacker, defender);
        }


    }
}
