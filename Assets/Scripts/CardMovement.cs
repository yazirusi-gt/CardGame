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
    //カードの移動
    public void OnBeginDrag(PointerEventData eventData)
    {
        //カードのコストとPlayerのManaコストを比較
        CardController card = GetComponent<CardController>();
        if (card.model.isPlayerCard && GameManager.instance.isPlayerTurn && !card.model.isFieldCard && card.model.cost <= GameManager.instance.gamePlayer.manaCost)
        {
            isDraggable = true;
        }
        else if(card.model.isPlayerCard && GameManager.instance.isPlayerTurn && card.model.isFieldCard && card.model.canAttack)
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
        //一度親をCanvsに変更する
        transform.SetParent(defaultParent.parent);
        //DOTween１でカードをフィールドに移動
        transform.DOMove(field.position, 0.25f);
        yield return new WaitForSeconds(0.25f);
        defaultParent = field;
        transform.SetParent(defaultParent);
    }
    public IEnumerator MoveToTarget(Transform target)
    {
        Vector3 currentPosition = transform.position;
        int getSiblingIndex = transform.GetSiblingIndex();

        //一度親をCanvsに変更する
        transform.SetParent(defaultParent.parent);
        //DOTween１でカードをTargetに移動
        Tweener moveTween = transform.DOMove(target.position, 0.25f);
        yield return new WaitForSeconds(0.25f);

        //元の位置に戻る
        transform.DOMove(currentPosition, 0.25f);
        yield return new WaitForSeconds(0.25f);
        transform.SetParent(defaultParent);
        transform.SetSiblingIndex(getSiblingIndex);

    }
    public void test(Transform field)
    {
        defaultParent = field;
        transform.SetParent(defaultParent);
    }

}
