using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//XyU³êé¤
public class SpelDropManager : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* U*/
        //UJ[hðIð
        CardController spellCard = eventData.pointerDrag.GetComponent<CardController>();
        CardController target = GetComponent<CardController>(); //nullÌÂ\«à é

        if (spellCard == null)
        {
            return;
        }
        spellCard.UseSpellTo(target);
    }
}
