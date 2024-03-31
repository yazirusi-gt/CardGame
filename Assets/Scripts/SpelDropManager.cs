using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�X�y���U������鑤
public class SpelDropManager : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        /* �U��*/
        //�U���J�[�h��I��
        CardController spellCard = eventData.pointerDrag.GetComponent<CardController>();
        CardController target = GetComponent<CardController>(); //null�̉\��������

        if (spellCard == null)
        {
            return;
        }
        spellCard.UseSpellTo(target);
    }
}
