using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� Ŭ������ PlayerTextŬ�����̸� FieldObject�� ��ӹ޾� ����. FieldObject�� MonoBehaviour�� ��ӹް� �ִ�. (1.17 ���� �߰�)
// Issue (1.17) ó�� ������ �� Ű���� �ѹ��� �����ϰ� ������
public class PlayerTest : FieldObject
{

    TimingManager timingManager;
    int maxHp = 3;
    int currentHp;


    HpBar hpBar;
    public int CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            hpBar.updateValue(currentHp);
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        timingManager=FindObjectOfType<TimingManager>();
        hpBar = Util.FindChild<HpBar>(gameObject, null, true);

        CurrentHp = maxHp;
        // type�� �ʱ�ȭ�ϰ� objectField�� �޾ƿ� ��, objectList�� PlayerField�� �޾ƿ´�.
        type = 1;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);
        
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
       /* if (!Managers.Bpm.Able)
            return;*/
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Managers.Timing.CheckTiming();
            //timingManager.CheckTiming();
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Up);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //Managers.Timing.CheckTiming();
            //timingManager.CheckTiming();
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Left);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            //Managers.Timing.CheckTiming();
            //timingManager.CheckTiming();
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Down);
            }

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //Managers.Timing.CheckTiming();
            //timingManager.CheckTiming();
            if (timingManager.CheckTiming())
            {
                mayGo(Define.PlayerMove.Right);
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            //Managers.Timing.CheckTiming();
            //timingManager.CheckTiming();
            if (timingManager.CheckTiming())
            {
                Attack();
            }
        }
        
            
        
    }

    protected override void Attack()
    {
        int[] pattern = GetComponent<Weapon>().CalculateAttackRange(currentInd);
        Managers.Field.WarningAttack(pattern);
        Managers.Field.Attack(pattern);


        Transform attack = transform.GetChild(0);
        attack.GetComponent<PlayerAttack>().Attacking();
    }

    protected override void Hit()
    {
        Debug.Log("Hit!!!!");
        GetComponent<Animator>().SetTrigger("isHit");
        CurrentHp -= 1;
        if (CurrentHp <= 0)
            Die();
    }
    void Die()
    {
        Debug.Log("Player Die!!");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit();
    }
    
}
