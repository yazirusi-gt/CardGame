using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardEntity;

/// <summary>
/// カードデータとその処理
/// </summary>
public class CardModel
{
    public string name;
    public int hp;
    public int at;
    public int cost;
    public ABILITY ability;
    public Sprite icon;
    public bool isAlive;
    public bool canAttack;
    public bool isFieldCard;
    public bool isPlayerCard;

    public CardModel(int cardID, bool isPlayerCard)
    {
        //ScriptableObjectデータを取り込み
        CardEntity card = Resources.Load<CardEntity>("CardEntityList/Card" + cardID);
        name = card.name;
        hp = card.hp;
        at = card.at;
        cost = card.cost;
        ability = card.ability;
        icon = card.icon;
        isAlive = true;
        this.isPlayerCard = isPlayerCard;
    }

    void Damage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            isAlive=false;
        }
    }

    public void Attack(CardController card)
    {
        card.model.Damage(at);
    }
}
