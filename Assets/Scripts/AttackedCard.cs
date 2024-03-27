using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Uณ๊้ค
public class AttackedCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* U*/
        //UJ[h๐I๐
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();
        //hไJ[h๐I๐
        CardController defender = GetComponent<CardController>();

        if (attacker == null || defender == null)
        {
            return;
        }
        if (attacker.model.canAttack)
        {
            //attackerฦdefender๐ํํน้
            GameManager.instance.CardsBattle(attacker, defender);
        }


    }
}
