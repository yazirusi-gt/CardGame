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

    public CardModel(int cardID)
    {
        //ScriptableObjectデータを取り込み
        CardEntity card = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
        name = card.name;
        hp = card.hp;
        at = card.at;
        cost = card.cost;
        icon = card.icon;
    }
}
