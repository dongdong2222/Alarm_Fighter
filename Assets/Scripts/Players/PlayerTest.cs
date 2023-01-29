using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �� Ŭ������ PlayerTextŬ�����̸� FieldObject�� ��ӹ޾� ����. FieldObject�� MonoBehaviour�� ��ӹް� �ִ�. (1.17 ���� �߰�)
// Issue (1.17) ó�� ������ �� Ű���� �ѹ��� �����ϰ� ������
public class PlayerTest : FieldObject
{

    TimingManager timingManager;
    PlayerPattern attackPattern = new DefaultOnetilePattern();
    PlayerHPbar hpBar;
    // Start is called before the first frame update
    void Start()
    {
        timingManager=FindObjectOfType<TimingManager>();

        // playerHPbar ���� �ʱ�ȭ (1.29 ���� �߰�)
        hpBar = GameObject.FindObjectOfType<PlayerHPbar>();

        // type�� �ʱ�ȭ�ϰ� objectField�� �޾ƿ� ��, objectList�� PlayerField�� �޾ƿ´�.
        type = 1;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);

        // maxHP�� currentHP �ʱ�ȭ (1.25 ���� �߰�)
        maxHP = 3;
        currentHP = maxHP;
        // playerHPbar ���� �ʱ�ȭ (1.29 ���� �߰�)
        hpBar.UpdateValue(currentHP, maxHP);

        currentInd = objectList.Count / 2; // �� �ʱ�ȭ�� ��ġ�� Field�� Width�� ���, ����� ������ �� ���� (1.18 ���� �߰�)
        transform.position = objectList[currentInd].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // �� �� ����ϰ� ������ ���� �� �ִ� ��� �ʿ� (1.29 ���� �߰�)
        if (!Managers.isPlayingGame) return;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentHP -= 1;
        hpBar.UpdateValue(currentHP, maxHP);
        if (currentHP <= 0)
        {
            Debug.Log("Game Over");
            Managers.isPlayingGame = false;
            return;
        }
    }
}
