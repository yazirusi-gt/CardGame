using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h�f�[�^�Ƃ��̏���
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
        //ScriptableObject�f�[�^����荞��
        CardEntity card = Resources.Load<CardEntity>("CardEntityList/Card"+cardID);
        name = card.name;
        hp = card.hp;
        at = card.at;
        cost = card.cost;
        icon = card.icon;
    }
}
