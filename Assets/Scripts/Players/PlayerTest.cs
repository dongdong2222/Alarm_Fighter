using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� Ŭ������ PlayerTextŬ�����̸� FieldObject�� ��ӹ޾� ����. FieldObject�� MonoBehaviour�� ��ӹް� �ִ�. (1.17 ���� �߰�)
// Issue (1.17) ó�� ������ �� Ű���� �ѹ��� �����ϰ� ������
public class PlayerTest : FieldObject
{

    TimingManager timingManager;
    MonsterPattern attackPattern = new LinePattern();
    // Start is called before the first frame update
    void Start()
    {
        timingManager=FindObjectOfType<TimingManager>();

        // type�� �ʱ�ȭ�ϰ� objectField�� �޾ƿ� ��, objectList�� PlayerField�� �޾ƿ´�.
        type = 1;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);

        // maxHP�� currentHP �ʱ�ȭ (1.25 ���� �߰�)
        maxHP = 3;

        currentHP = maxHP;

        currentInd = objectList.Count / 2; // �� �ʱ�ȭ�� ��ġ�� Field�� Width�� ���, ����� ������ �� ���� (1.18 ���� �߰�)
        transform.position = objectList[currentInd].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BitBehave();
    }

    protected override void BitBehave()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Up);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Left);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Down);
            }

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (timingManager.CheckTiming())
            {
                Attack();
            }
        }
        
            
        
    }

    protected override void Attack()
    {
        // Hierachy �󿡼� Player ���� �ִϸ��̼��� �޾ƿ��ڴٴ� �̾߱�
        Transform attack = transform.GetChild(0);
        attack.GetComponent<PlayerAttack>().Attacking();
        int[] pattern = attackPattern.calculateIndex(currentInd);
        Managers.Field.Attack(pattern);
    }

    protected override void Hit()
    {
        // hit �������̵� �ϱ�

    }
}
