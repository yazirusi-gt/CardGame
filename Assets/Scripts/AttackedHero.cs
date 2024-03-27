using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//U³κι€
public class AttackedHero : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* U*/
        //UJ[hπIπ
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();

        if (attacker == null)
        {
            return;
        }
        if (attacker.model.canAttack)
        {
            //attackerͺHeroΙU·ι
            GameManager.instance.AttackToHero(attacker, true);
        }


    }
}
