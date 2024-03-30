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

        //�t�B�[���h�̃J�[�h���X�g���擾
        CardController[] fieldEnemyCardList = gameManager.FieldEnemyTransform.GetComponentsInChildren<CardController>();
        gameManager.SettingCanAttackView(fieldEnemyCardList, true);

        yield return new WaitForSeconds(1);

        /*��ɃJ�[�h���o��*/
        //��D�̃J�[�h���X�g���擾
        CardController[] handCardList = gameManager.HandEnemyTransform.GetComponentsInChildren<CardController>();

        //�R�X�g�ȉ��̃J�[�h������΃J�[�h���t�B�[���h�ɏo��������
        while (Array.Exists(handCardList, card => card.model.cost <= gameManager.enemyManaCost))
        {
            //��ɏo���J�[�h���t����
            CardController[] selectableHandCardList = Array.FindAll(handCardList, card => card.model.cost <= gameManager.enemyManaCost);
            //�R�X�g�ȉ��̃J�[�h���X�g���擾
            CardController enemyCard = selectableHandCardList[0];
            //�J�[�h���ړ�
            StartCoroutine(enemyCard.movement.MoveToField(gameManager.FieldEnemyTransform));

            //enemy�}�i����
            gameManager.ReduceManaCost(enemyCard.model.cost, false);
            enemyCard.model.isFieldCard = true;
            enemyCard.OnFiled(false);
            handCardList = gameManager.HandEnemyTransform.GetComponentsInChildren<CardController>();

            yield return new WaitForSeconds(1);

        }

        /* �U��*/
        //�t�B�[���h�̃J�[�h���X�g���擾
        CardController[] fieldCardList = gameManager.FieldEnemyTransform.GetComponentsInChildren<CardController>();

        //�U���\�J�[�h������΍U�����J��Ԃ�
        while (Array.Exists(fieldCardList, card => card.model.canAttack))
        {
            //�U���\�J�[�h���擾
            CardController[] enemyCanAttackList = Array.FindAll(fieldCardList, card => card.model.canAttack); //����:Array.FindAll

            //�h��J�[�h��I��
            CardController[] playerFieldCardList = gameManager.FieldPlayerTransform.GetComponentsInChildren<CardController>();

            //attacker�J�[�h��I��
            CardController attacker = enemyCanAttackList[0];

            if (playerFieldCardList.Length > 0)
            {
                //defender�J�[�h��I��
                CardController defender = playerFieldCardList[0];

                //attacker��defender���킹��
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
