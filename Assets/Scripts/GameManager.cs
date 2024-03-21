using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //手札の情報
    [SerializeField] Transform HandPlayerTransform,
        FieldPlayerTransform,
        HandEnemyTransform,
        FieldEnemyTransform;
    //カードの生成使用する
    [SerializeField] CardController CardPrefab;

    bool isPlayerTurn;
    private void Start()
    {
        StartGame();
        isPlayerTurn = true;
        TurnCalc();
    }

    private void TurnCalc()
    {
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();

        }
    }
    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        if (isPlayerTurn)
        {
            CardDraw(HandPlayerTransform);
        }
        else
        {
            CardDraw(HandEnemyTransform);
        }
        TurnCalc();
    }
    private void EnemyTurn()
    {
        Debug.Log("敵");
        /*場にカードを出す*/
        //手札のカードリストを取得
        CardController[] handCardList = HandEnemyTransform.GetComponentsInChildren<CardController>();
        //場に出すカードを千t無く
        CardController enemyCard = handCardList[0];
        //カードを移動
        enemyCard.movement.SetCardTransform(FieldEnemyTransform);

        /* 攻撃*/
        //フィールドのカードリストを取得
        CardController[] fieldCardList = FieldEnemyTransform.GetComponentsInChildren<CardController>();

        //攻撃カードを選択
        CardController attacker = fieldCardList[0];

        //防御カードを選択
        CardController[] playerFieldCardList = FieldPlayerTransform.GetComponentsInChildren<CardController>();
        CardController defender = playerFieldCardList[0];

        //攻撃と防御を戦わせる
        CardsBattle(attacker, defender);

        ChangeTurn();
    }

    void CardsBattle(CardController attacker, CardController defender)
    {
        Debug.Log("CardsBattle");
        Debug.Log("attacker HP:"+attacker.model.hp);
        Debug.Log("defender HP:" + defender.model.hp);
        attacker.model.Attack(defender);
        defender.model.Attack(attacker);
        Debug.Log("attacker HP:" + attacker.model.hp);
        Debug.Log("defender HP:" + defender.model.hp);
    }

    private void PlayerTurn()
    {
        Debug.Log("プレイヤー");

    }

    private void StartGame()
    {
        SettingInitHand();

    }
    void SettingInitHand()
    {
        for (int i = 0; i < 3; i++)
        {
            CardDraw(HandPlayerTransform);
            CardDraw(HandEnemyTransform);
        }
    }
    private void CardDraw(Transform hand)
    {
        CardController card = Instantiate(CardPrefab, hand, false);
        card.Init(2);
    }
}
