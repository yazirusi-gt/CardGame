using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //��D�̏��
    [SerializeField] Transform HandPlayerTransform,
        FieldPlayerTransform,
        HandEnemyTransform,
        FieldEnemyTransform;
    //�J�[�h�̐����g�p����
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
        Debug.Log("�G");
        /*��ɃJ�[�h���o��*/
        //��D�̃J�[�h���X�g���擾
        CardController[] handCardList = HandEnemyTransform.GetComponentsInChildren<CardController>();
        //��ɏo���J�[�h���t����
        CardController enemyCard = handCardList[0];
        //�J�[�h���ړ�
        enemyCard.movement.SetCardTransform(FieldEnemyTransform);

        /* �U��*/
        //�t�B�[���h�̃J�[�h���X�g���擾
        CardController[] fieldCardList = FieldEnemyTransform.GetComponentsInChildren<CardController>();

        //�U���J�[�h��I��
        CardController attacker = fieldCardList[0];

        //�h��J�[�h��I��
        CardController[] playerFieldCardList = FieldPlayerTransform.GetComponentsInChildren<CardController>();
        CardController defender = playerFieldCardList[0];

        //�U���Ɩh����킹��
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
        Debug.Log("�v���C���[");

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
