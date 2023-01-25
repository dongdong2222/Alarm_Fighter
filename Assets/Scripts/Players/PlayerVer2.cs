using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVer2 : FieldObject
{
    Define.State nextBehavior = Define.State.IDLE;
    Define.PlayerMove nextDirection = Define.PlayerMove.Right;
    PlayerPattern attackPattern = new BasicAttack();
    int maxHp = 3;
    public int currentHp = 3;
    private void Start()
    {
        type = 1;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);

        currentInd = objectList.Count / 2;
        transform.position = objectList[currentInd].transform.position;


        Managers.Timing.BehaveAction -= BitBehave;
        Managers.Timing.BehaveAction += BitBehave;
    }

    protected override void BitBehave()
    {
        if (nextBehavior == Define.State.IDLE)
        {
            Debug.Log("Idle!");
        }
        else if (nextBehavior == Define.State.MOVE)
        {
            if (nextDirection == Define.PlayerMove.Left)
                mayGo(Define.PlayerMove.Left);
            else if (nextDirection == Define.PlayerMove.Right)
                mayGo(Define.PlayerMove.Right);
            else if (nextDirection != Define.PlayerMove.Up)
                mayGo(Define.PlayerMove.Down);
            else if (nextDirection != Define.PlayerMove.Down)
                mayGo(Define.PlayerMove.Up);
        }
        else if (nextBehavior == Define.State.ATTACK)
            Attack();

        nextBehavior = Define.State.IDLE;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            nextBehavior = Define.State.MOVE;
            nextDirection = Define.PlayerMove.Up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            nextBehavior = Define.State.MOVE;
            nextDirection = Define.PlayerMove.Left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            nextBehavior = Define.State.MOVE;
            nextDirection = Define.PlayerMove.Down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            nextBehavior = Define.State.MOVE;
            nextDirection = Define.PlayerMove.Right;
        }
        else if (Input.GetKeyDown(KeyCode.K))
            nextBehavior = Define.State.ATTACK;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentHp -= 1;
        Debug.Log("Hit!");
        if (currentHp == 0)
            Die();
    }

    protected override void Attack()
    {
        int[] pattern = attackPattern.calculateIndex(currentInd);
        Managers.Field.WarningAttack(pattern);
        Managers.Field.Attack(pattern);
    }

    void Die()
    {
        Debug.Log("dIe!");
        //Managers.Scene.CurrentScene.GameOver();
    }
}
