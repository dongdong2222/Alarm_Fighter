using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVer2 : FieldObject
{
    //idle ���� (1.25)
    Define.State nextBehavior = Define.State.MOVE;
    //to do : MonsterMove or MoveDirection
    Define.PlayerMove nextDirection = Define.PlayerMove.Right;
    MonsterPattern attackPattern = new LinePattern();

    MonsterHPbar hpBar;
    private void Start()
    {
        type = 2;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);

        hpBar = GameObject.FindObjectOfType<MonsterHPbar>();
        // maxHP�� currentHP �ʱ�ȭ (1.25 ���� �߰�)
        maxHP = 3;
        currentHP = maxHP;

        // ó���� ���� 4�� �ε��� ��ġ (���� �߾� ��ġ) - (1.29 ���� �߰�)
        currentInd = objectList.Count / 2 + 1;
        transform.position = objectList[currentInd].transform.position;

        Managers.Timing.BehaveAction -= BitBehave;
        Managers.Timing.BehaveAction += BitBehave;
    }

    protected override void BitBehave()
    {
        Animator anim = GetComponent<Animator>();
        switch(nextBehavior)
        {
            // idle ����
            /*
            case Define.State.IDLE:
                anim.Play("Idle");
                updateIdle();
                break;*/
            case Define.State.ATTACKREADY:
                anim.Play("AttackReady");
                updateAtttackReady();
                break;
            case Define.State.ATTACK:
                anim.Play("Attack");
                updateAttack();
                break;
            case Define.State.MOVE:
                anim.Play("Move");
                updateMove();
                break;

        }
    }

    void ChaseCheck()
    {
        //to do : left or right or Stop Check
        // �پ��� ���� move (1.29 ���� �߰�) -> Random.Ranage(0, 4)�� ���� 0, 1, 2, 3 ������ ���� �߻�
        int rand = Random.Range(0, 4);
        switch(rand)
        {
            case 0:
                nextDirection = Define.PlayerMove.Right;
                break;
            case 1:
                nextDirection = Define.PlayerMove.Left;
                break;
            /*case 2:
                nextDirection = Define.PlayerMove.Stop;
                break;*/
            case 2:
                nextDirection = Define.PlayerMove.Up;
                break;
            case 3:
                nextDirection = Define.PlayerMove.Down;
                break;

        }
    }


    void updateIdle()
    {
        nextBehavior = Define.State.MOVE;
    }
    void updateMove()
    {
        ChaseCheck();
        mayGo(nextDirection);
        nextBehavior = Define.State.ATTACKREADY;
    }
    void updateAtttackReady()
    {
        AttackReady();
        nextBehavior = Define.State.ATTACK;
    }
    void updateAttack()
    {
        Attack();
        nextBehavior = Define.State.MOVE;
        //nextBehavior = Define.State.IDLE;
        //idle ����(1.25)
    }
    protected override void Attack()
    {
        int[] pattern = attackPattern.calculateIndex(currentInd);
        Managers.Field.Attack(pattern);
    }
    void AttackReady()
    {
        int[] pattern = attackPattern.calculateIndex(currentInd);
        Managers.Field.WarningAttack(pattern);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("monster hit enter");
        currentHP -= 1;
        hpBar.UpdateValue(currentHP, maxHP);
        if (currentHP <= 0)
        {
            Debug.Log("You Win!");
            Managers.isPlayingGame = false;
            return;
        }
    }
}
