using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text resultText;


    //��D�̏��
    [SerializeField] Transform HandPlayerTransform,
        FieldPlayerTransform,
        HandEnemyTransform,
        FieldEnemyTransform;
    //�J�[�h�̐����g�p����
    [SerializeField] CardController CardPrefab;

    public bool isPlayerTurn;

    List<int> playerDeck = new List<int>() { 1,3,2,2},
                enemyDeck = new List<int>() { 3, 1, 2, 2 };

    [SerializeField] Transform playerHero;

    [SerializeField] Text playerHeroHpText;
    [SerializeField] Text enemyHeroHpText;

    int playerHeroHp;
    int enemyHeroHp;

    [SerializeField] Text playerManaCostText;
    [SerializeField] Text enemyManaCostText;

    public int playerManaCost;
    int enemyManaCost;
    int playerDefaultManaCost;
    int enemyDefaultManaCost;

    //���ԊǗ�
    public int TIME = 5;
    [SerializeField] Text timeCountText;
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
        resultPanel.SetActive(false);
        playerHeroHp = 10;
        enemyHeroHp = 10;
        ShowHeroHP();

        playerManaCost = 1;
        enemyManaCost = 1;
        ShowManaCost();

        playerDefaultManaCost = 1;
        enemyDefaultManaCost = 10;

        SettingInitHand();

        timeCount = TIME;
        timeCountText.text = timeCount.ToString();

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
    void ShowManaCost()
    {
        playerManaCostText.text = playerManaCost.ToString();
        enemyManaCostText.text = enemyManaCost.ToString();
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
            playerManaCost -= cost;
        }
        else
        {
            enemyManaCost -= cost;
        }
        ShowManaCost();
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
        playerDeck = new List<int>() { 1, 1, 2, 2 };
        enemyDeck = new List<int>() { 3, 1, 2, 2 };

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
            StartCoroutine(EnemyTurn());
            EnemyTurn();

        }
    }

    IEnumerator CountDown()
    {
        timeCount = TIME;
        timeCountText.text = timeCount.ToString();
        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1); //��b�ҋ@
            timeCount--;
            timeCountText.text = timeCount.ToString();
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
            playerDefaultManaCost++;
            playerManaCost = playerDefaultManaCost;
            GiveCardToHand(playerDeck, HandPlayerTransform);
        }
        else
        {
            enemyDefaultManaCost++;
            enemyManaCost = enemyDefaultManaCost;
            GiveCardToHand(enemyDeck, HandEnemyTransform);
        }
        ShowManaCost();
        TurnCalc();
    }
    IEnumerator EnemyTurn()
    {

        //�t�B�[���h�̃J�[�h���X�g���擾
        CardController[] fieldEnemyCardList = FieldEnemyTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(fieldEnemyCardList, true);

        yield return new WaitForSeconds(1);

        /*��ɃJ�[�h���o��*/
        //��D�̃J�[�h���X�g���擾
        CardController[] handCardList = HandEnemyTransform.GetComponentsInChildren<CardController>();

        //�R�X�g�ȉ��̃J�[�h������΃J�[�h���t�B�[���h�ɏo��������
        while (Array.Exists(handCardList, card => card.model.cost <= enemyManaCost))
        {
            //��ɏo���J�[�h���t����
            CardController[] selectableHandCardList = Array.FindAll(handCardList, card => card.model.cost <= enemyManaCost);
            //�R�X�g�ȉ��̃J�[�h���X�g���擾
            CardController enemyCard = selectableHandCardList[0];
            //�J�[�h���ړ�
            StartCoroutine(enemyCard.movement.MoveToField(FieldEnemyTransform));

            //enemy�}�i����
            ReduceManaCost(enemyCard.model.cost, false);
            enemyCard.model.isFieldCard = true;
            enemyCard.OnFiled(false);
            handCardList = HandEnemyTransform.GetComponentsInChildren<CardController>();

            yield return new WaitForSeconds(1);

        }

        /* �U��*/
        //�t�B�[���h�̃J�[�h���X�g���擾
        CardController[] fieldCardList = FieldEnemyTransform.GetComponentsInChildren<CardController>();

        //�U���\�J�[�h������΍U�����J��Ԃ�
        while (Array.Exists(fieldCardList, card => card.model.canAttack))
        {
            //�U���\�J�[�h���擾
            CardController[] enemyCanAttackList = Array.FindAll(fieldCardList, card => card.model.canAttack); //����:Array.FindAll

            //�h��J�[�h��I��
            CardController[] playerFieldCardList = FieldPlayerTransform.GetComponentsInChildren<CardController>();

            //attacker�J�[�h��I��
            CardController attacker = enemyCanAttackList[0];

            if (playerFieldCardList.Length > 0)
            {
                //defender�J�[�h��I��
                CardController defender = playerFieldCardList[0];

                //attacker��defender���킹��
                StartCoroutine(attacker.movement.MoveToTarget(defender.transform));
                yield return new WaitForSeconds(0.51f);
                CardsBattle(attacker, defender);
            }
            else
            {
                StartCoroutine(attacker.movement.MoveToTarget(playerHero.transform));
                yield return new WaitForSeconds(0.25f);
                AttackToHero(attacker, false);
                yield return new WaitForSeconds(0.25f);
                CheckHeroHP();
            }

            fieldCardList = FieldEnemyTransform.GetComponentsInChildren<CardController>();
            yield return new WaitForSeconds(1);
        }

        ChangeTurn();
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

    void ShowHeroHP()
    {
        playerHeroHpText.text = playerHeroHp.ToString();
        enemyHeroHpText.text = enemyHeroHp.ToString();
    }

    public void AttackToHero(CardController attacker, bool isPlayerCard)
    {
        if (isPlayerCard)
        {
            enemyHeroHp -= attacker.model.hp;
        }
        else
        {
           playerHeroHp -= attacker.model.hp;
        }
        attacker.SetCandAttack(false);
        ShowHeroHP();

    }

    private void PlayerTurn()
    {

        //�t�B�[���h�̃J�[�h���X�g���擾
        CardController[] fieldPlayerCardList = FieldPlayerTransform.GetComponentsInChildren<CardController>();
        SettingCanAttackView(fieldPlayerCardList,true);

    }
    private void SettingCanAttackView(CardController[] fieldCardList, bool canAttack)
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
            GiveCardToHand(playerDeck, HandPlayerTransform);
            GiveCardToHand(enemyDeck, HandEnemyTransform);
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
        if (playerHeroHp <= 0 || enemyHeroHp <= 0)
        {
            ShowResultPanel(playerHeroHp);
            resultPanel.SetActive(true);
        }
    }
    void ShowResultPanel(int heroHp)
    {
        StopAllCoroutines();
        resultPanel.SetActive(true);
        if (playerHeroHp <= 0)
        {
            resultText.text = "LOSE";
        }
        else
        {
            resultText.text = "WIN";

        }
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
