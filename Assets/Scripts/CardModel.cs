using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カードデータとその処理
/// </summary>
public class CardModel
{
    public string name;
    public int hp;
    public int at;
    public int cost;
    public Sprite icon;
    public bool isAlive;
    public bool canAttack;
    public bool isFieldCard;

    public CardModel(int cardID)
    {
        //ScriptableObjectデータを取り込み
        CardEntity card = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
        name = card.name;
        hp = card.hp;
        at = card.at;
        cost = card.cost;
        icon = card.icon;
        isAlive = true;
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
