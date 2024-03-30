using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�U������鑤
public class AttackedCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* �U��*/
        //�U���J�[�h��I��
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();
        //�h��J�[�h��I��
        CardController defender = GetComponent<CardController>();

        if (attacker == null || defender == null)
        {
            return;
        }
        if(attacker.model.isPlayerCard == defender.model.isPlayerCard)
        {
            return;
        }
        //�V�[���h�J�[�h������΃V�[���h�J�[�h�ȊO�͍U���ł��Ȃ��B
        CardController[] enemyFieldCards = GameManager.instance.GetEnemyFieldCards();
        if (Array.Exists(enemyFieldCards,
                card => card.model.ability == CardEntity.ABILITY.SHIELD) && defender.model.ability != CardEntity.ABILITY.SHIELD)
        {
            return;
        }
        if (attacker.model.canAttack)
        {
            //attacker��defender���킹��
            GameManager.instance.CardsBattle(attacker, defender);
        }


    }
}
