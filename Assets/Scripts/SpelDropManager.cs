using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//スペル攻撃される側
public class SpelDropManager : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* 攻撃*/
        //攻撃カードを選択
        CardController spellCard = eventData.pointerDrag.GetComponent<CardController>();
        CardController target = GetComponent<CardController>(); //nullの可能性もある

        if (spellCard == null)
        {
            return;
        }
        spellCard.UseSpellTo(target);
    }
}
