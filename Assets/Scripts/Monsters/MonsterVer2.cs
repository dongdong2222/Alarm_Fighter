using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVer2 : FieldObject
{
    Define.State nextBehavior = Define.State.IDLE;
    //to do : MonsterMove or MoveDirection
    Define.PlayerMove nextDirection = Define.PlayerMove.Stop;
    MonsterPattern attackPattern = new LinePattern();
    int maxHp = 3;
    int currentHp = 3;
    private void Start()
    {
        type = 2;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);

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
            case Define.State.IDLE:
                anim.Play("Idle");
                updateIdle();
                break;
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
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                nextDirection = Define.PlayerMove.Right;
                break;
            case 1:
                nextDirection = Define.PlayerMove.Left;
                break;
            case 2:
                nextDirection = Define.PlayerMove.Stop;
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
        nextBehavior = Define.State.IDLE;
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
        currentHp -= 1;
        Debug.Log("MonsterHit!");
        if (currentHp == 0)
            Die();
    }
    void Die()
    {
        Debug.Log("MonsterDie!");
    }

}
