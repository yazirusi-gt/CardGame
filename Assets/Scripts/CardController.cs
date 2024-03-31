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

    public void Init(int cardID,bool isPlayer)
    {
        model = new CardModel(cardID, isPlayer);
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

    public void OnFiled(bool isPlayer)
    {
        GameManager.instance.ReduceManaCost(model.cost, isPlayer);
        model.isFieldCard = true;
        if(model.ability == CardEntity.ABILITY.INIT_ATTACKABLE)
        {
            SetCandAttack(true);
        }
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

    public void UseSpellTo(CardController target)
    {
        switch(model.spell)
        {
            case CardEntity.SPELL.DAMAGE_ENEMY_CARD:
                //����̓G���U������
                Attack(target);
                target.CheckAlive();
                break;
            case CardEntity.SPELL.DAMAGE_ENEMY_CARDS:
                break;
            case CardEntity.SPELL.DAMAGE_ENEMY_HERO:
                break;
            case CardEntity.SPELL.HEAL_FRIEND_CARD:
                break;
            case CardEntity.SPELL.HEAL_FRIEND_CARDS:
                break;
            case CardEntity.SPELL.HEAL_FRIEND_HERO:
                break;
            case CardEntity.SPELL.NONE:
                return;
        }
        Destroy(this.gameObject);
    }
}
