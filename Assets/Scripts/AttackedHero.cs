using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//UŒ‚‚³‚ê‚é‘¤
public class AttackedHero : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* UŒ‚*/
        //UŒ‚ƒJ[ƒh‚ğ‘I‘ğ
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();

        if (attacker == null)
        {
            return;
        }
        if (attacker.model.canAttack)
        {
            //attacker‚ªHero‚ÉUŒ‚‚·‚é
            GameManager.instance.AttackToHero(attacker, true);
        }


    }
}
