using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�U������鑤
public class AttackedHero : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* �U��*/
        //�U���J�[�h��I��
        CardController attacker = eventData.pointerDrag.GetComponent<CardController>();

        if (attacker == null)
        {
            return;
        }
        if (attacker.model.canAttack)
        {
            //attacker��Hero�ɍU������
            GameManager.instance.AttackToHero(attacker, true);
        }


    }
}
