using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�U������鑤
public class AttackedHero : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* �U��*/
        //�U���J�[�h��I��
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();

        if (attacker == null)
        {
            return;
        }
        //�G�t�B�[���h�ɃV�[���h������΍U���ł��Ȃ�
        CardController[] enemyFieldCards = GameManager.instance.GetEnemyFieldCards();
        if (Array.Exists(enemyFieldCards, card => card.model.ability == CardEntity.ABILITY.SHIELD))
        {
            return;
        }

        if (attacker.model.canAttack)
        {
            //attacker��Hero�ɍU������
            GameManager.instance.AttackToHero(attacker, true);
            GameManager.instance.CheckHeroHP();
        }


    }
}
