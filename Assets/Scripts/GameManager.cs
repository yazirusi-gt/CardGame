using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AI enemyAI;
    [SerializeField] UIManeger uiManager;
    public GamePlayerManager gamePlayer;
    public GamePlayerManager gameEnemy;

    //��D�̏��
    public Transform HandPlayerTransform,
        FieldPlayerTransform,
        HandEnemyTransform,
        FieldEnemyTransform;
    //�J�[�h�̐����g�p����
    [SerializeField] CardController CardPrefab;

    public bool isPlayerTurn;

    public Transform playerHero;

    //���ԊǗ�
    public int TIME = 5;

    int timeCount;

    //�V���O���g����
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        uiManager.HideResutPanel();
        gamePlayer.Init(new List<int>() {4,1,3,2,2 });
        gameEnemy.Init(new List<int>() {4,1,3,2,2 });
        uiManager.ShowHeroHP(gamePlayer.heroHp, gameEnemy.heroHp);

        gamePlayer.manaCost = 1;
        gameEnemy.manaCost = 1;
        uiManager.ShowManaCost(gamePlayer.manaCost, gameEnemy.manaCost);

        gamePlayer.defaultManaCost = 1;
        gameEnemy.defaultManaCost = 10;

        SettingInitHand();

        timeCount = TIME;
        uiManager.UpdateTime(timeCount);

        isPlayerTurn = true;
        TurnCalc();

    }
    public void OnClickTurnEndButton()
    {
        if (isPlayerTurn)
        {
            ChangeTurn();
        }
    }


    /// <summary>
    /// ������ true:�v���C���[ false:�G�l�~�[
    /// </summary>
    /// <param name="cost"></param>
    /// <param name="isPlayerCard"></param>
    public void ReduceManaCost(int cost, bool isPlayerCard)
    {
        if(isPlayerCard)
        {
            gamePlayer.manaCost -= cost;
        }
        else
        {
            gameEnemy.manaCost -= cost;
        }
        uiManager.ShowManaCost(gamePlayer.manaCost, gameEnemy.manaCost);
    }

    public void Restart()
    {
        //hand��Filed�̃J�[�h���폜
        foreach(Transform card in HandPlayerTransform)
        {
            Destroy(card.gameObject);
        }
        foreach (Transform card in HandEnemyTransform)
        {
            Destroy(card.gameObject);
        }
        foreach (Transform card in FieldPlayerTransform)
        {
            Destroy(card.gameObject);
        }
        foreach (Transform card in FieldEnemyTransform)
        {
            Destroy(card.gameObject);
        }
        //�f�b�L�𐶐�
        gamePlayer.Deck = new List<int>() { 1, 1, 2, 2 };
        gameEnemy.Deck = new List<int>() { 3, 1, 2, 2 };

        StartGame();
    }

    private void TurnCalc()
    {
        StopAllCoroutines();
        StartCoroutine(CountDown());
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            StartCoroutine(enemyAI.EnemyTurn());
            enemyAI.EnemyTurn();

        }
    }

    IEnumerator CountDown()
    {
        timeCount = TIME;
        uiManager.UpdateTime(timeCount);
        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1); //��b�ҋ@
            timeCount--;
            uiManager.UpdateTime(timeCount);
        }
        OnClickTurnEndButton();
    }
    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        CardController[] fieldEnemyCardList = FieldEnemyTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(fieldEnemyCardList, false);
        CardController[] fieldPlayerCardList = FieldPlayerTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(fieldPlayerCardList, false);

        if (isPlayerTurn)
        {
            gamePlayer.InCreaseManaCost();
            GiveCardToHand(gamePlayer.Deck, HandPlayerTransform);
        }
        else
        {
            gameEnemy.InCreaseManaCost();
            GiveCardToHand(gameEnemy.Deck, HandEnemyTransform);
        }
        uiManager.ShowManaCost(gamePlayer.manaCost, gameEnemy.manaCost);
        TurnCalc();
    }

    public CardController[] GetEnemyFieldCards()
    {
        
        return FieldEnemyTransform.GetComponentsInChildren<CardController>();
    }

    public void CardsBattle(CardController attacker, CardController defender)
    {
        Debug.Log("CardsBattle");
        Debug.Log("attacker HP:"+attacker.model.hp);
        Debug.Log("defender HP:" + defender.model.hp);
        attacker.Attack(defender);
        defender.Attack(attacker);
        Debug.Log("attacker HP:" + attacker.model.hp);
        Debug.Log("defender HP:" + defender.model.hp);
        attacker.CheckAlive();
        defender.CheckAlive();
    }

    public void AttackToHero(CardController attacker, bool isPlayerCard)
    {
        if (isPlayerCard)
        {
            gameEnemy.heroHp -= attacker.model.hp;
        }
        else
        {
            gamePlayer.heroHp -= attacker.model.hp;
        }
        attacker.SetCandAttack(false);
        uiManager.ShowHeroHP(gamePlayer.heroHp, gameEnemy.heroHp);

    }

    private void PlayerTurn()
    {

        //�t�B�[���h�̃J�[�h���X�g���擾
        CardController[] fieldPlayerCardList = FieldPlayerTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(fieldPlayerCardList,true);

    }
    public void SettingCanAttackView(CardController[] fieldCardList, bool canAttack)
    {
        foreach (CardController card in fieldCardList)
        {
            //caqrd���U���\�ɂ���
            card.SetCandAttack(canAttack);
        }
    }


    void SettingInitHand()
    {
        for (int i = 0; i < 3; i++)
        {
            GiveCardToHand(gamePlayer.Deck, HandPlayerTransform);
            GiveCardToHand(gameEnemy.Deck, HandEnemyTransform);
        }
    }
    void GiveCardToHand(List<int> deck,Transform hand)
    {
        if (deck.Count == 0)
        {
            return;
        }
        int cardID = deck[0];
        deck.RemoveAt(0);
        CardDraw(cardID, hand);

    }

    public void CheckHeroHP()
    {
        if (gamePlayer.heroHp <= 0 || gameEnemy.heroHp <= 0)
        {
            ShowResultPanel(gamePlayer.heroHp);
            //resultPanel.SetActive(true);//�s�v�ȃR�[�h�H
        }
    }
    void ShowResultPanel(int heroHp)
    {
        StopAllCoroutines();
        uiManager.ShowResultPanel(heroHp);
    }

    private void CardDraw(int cardID, Transform hand)
    {
        CardController card = Instantiate(CardPrefab, hand, false);
        if (hand.name == "HandPlayer")
        {
            card.Init(cardID, true);
        }
        else
        {
            card.Init(cardID, false);

        }

    }
}
