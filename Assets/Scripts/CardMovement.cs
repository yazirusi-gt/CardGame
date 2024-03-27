using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CardMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public Transform defaultParent;

    public bool isDraggable;
    private void Start()
    {
        defaultParent = transform.parent;
    }
    //�J�[�h�̈ړ�
    public void OnBeginDrag(PointerEventData eventData)
    {
        //�J�[�h�̃R�X�g��Player��Mana�R�X�g���r
        CardController card = GetComponent<CardController>();
        if (!card.model.isFieldCard && card.model.cost <= GameManager.instance.playerManaCost)
        {
            isDraggable = true;
        }
        else if(card.model.isFieldCard && card.model.canAttack)
        {
            isDraggable = true;
        }
        else
        {
            isDraggable = false;
        }

        if (!isDraggable )
        {
            return;
        }
        defaultParent = transform.parent;
        transform.SetParent(defaultParent.parent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }
        transform.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }
        transform.SetParent(defaultParent, false);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }

    public IEnumerator MoveToField(Transform field)
    {
        //��x�e��Canvs�ɕύX����
        transform.SetParent(defaultParent.parent);
        //DOTween�P�ŃJ�[�h���t�B�[���h�Ɉړ�
        transform.DOMove(field.position, 0.25f);
        yield return new WaitForSeconds(0.25f);
        defaultParent = field;
        transform.SetParent(defaultParent);
    }
    public IEnumerator MoveToTarget(Transform target)
    {
        Vector3 currentPosition = transform.position;

        //��x�e��Canvs�ɕύX����
        transform.SetParent(defaultParent.parent);
        //DOTween�P�ŃJ�[�h��Target�Ɉړ�
        Tweener moveTween = transform.DOMove(target.position, 0.25f);
        yield return moveTween.WaitForCompletion();

        //���̈ʒu�ɖ߂�
        Tweener returnTween = transform.DOMove(currentPosition, 0.25f);
        Debug.Log(currentPosition);
        yield return returnTween.WaitForCompletion();
        transform.SetParent(defaultParent);
        //Debug.Log(defaultParent.parent);


    }
    public void test(Transform field)
    {
        defaultParent = field;
        transform.SetParent(defaultParent);
    }

}
