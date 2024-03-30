using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public IEnumerator EnemyTurn()
    {

        //フィールドのカードリストを取得
        CardController[] fieldEnemyCardList = gameManager.FieldEnemyTransform.GetComponentsInChildren<CardController>();
        gameManager.SettingCanAttackView(fieldEnemyCardList, true);

        yield return new WaitForSeconds(1);

        /*場にカードを出す*/
        //手札のカードリストを取得
        CardController[] handCardList = gameManager.HandEnemyTransform.GetComponentsInChildren<CardController>();

        //コスト以下のカードがあればカードをフィールドに出し続ける
        while (Array.Exists(handCardList, card => card.model.cost <= gameManager.enemyManaCost))
        {
            //場に出すカードを千t無く
            CardController[] selectableHandCardList = Array.FindAll(handCardList, card => card.model.cost <= gameManager.enemyManaCost);
            //コスト以下のカードリストを取得
            CardController enemyCard = selectableHandCardList[0];
            //カードを移動
            StartCoroutine(enemyCard.movement.MoveToField(gameManager.FieldEnemyTransform));

            //enemyマナ消費
            gameManager.ReduceManaCost(enemyCard.model.cost, false);
            enemyCard.model.isFieldCard = true;
            enemyCard.OnFiled(false);
            handCardList = gameManager.HandEnemyTransform.GetComponentsInChildren<CardController>();

            yield return new WaitForSeconds(1);

        }

        /* 攻撃*/
        //フィールドのカードリストを取得
        CardController[] fieldCardList = gameManager.FieldEnemyTransform.GetComponentsInChildren<CardController>();

        //攻撃可能カードがあれば攻撃を繰り返す
        while (Array.Exists(fieldCardList, card => card.model.canAttack))
        {
            //攻撃可能カードを取得
            CardController[] enemyCanAttackList = Array.FindAll(fieldCardList, card => card.model.canAttack); //検索:Array.FindAll

            //防御カードを選択
            CardController[] playerFieldCardList = gameManager.FieldPlayerTransform.GetComponentsInChildren<CardController>();

            //attackerカードを選択
            CardController attacker = enemyCanAttackList[0];

            if (playerFieldCardList.Length > 0)
            {
                //defenderカードを選択
                CardController defender = playerFieldCardList[0];

                //attackerとdefenderを戦わせる
                StartCoroutine(attacker.movement.MoveToTarget(defender.transform));
                yield return new WaitForSeconds(0.51f);
                gameManager.CardsBattle(attacker, defender);
            }
            else
            {
                StartCoroutine(attacker.movement.MoveToTarget(gameManager.playerHero.transform));
                yield return new WaitForSeconds(0.25f);
                gameManager.AttackToHero(attacker, false);
                yield return new WaitForSeconds(0.25f);
                gameManager.CheckHeroHP();
            }

            fieldCardList = gameManager.FieldEnemyTransform.GetComponentsInChildren<CardController>();
            yield return new WaitForSeconds(1);
        }

        gameManager.ChangeTurn();
    }

}
