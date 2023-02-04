using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� Ŭ������ PlayerTextŬ�����̸� FieldObject�� ��ӹ޾� ����. FieldObject�� MonoBehaviour�� ��ӹް� �ִ�. (1.17 ���� �߰�)
// Issue (1.17) ó�� ������ �� Ű���� �ѹ��� �����ϰ� ������
public class PlayerTest : FieldObject       //Player(GameObject)���� �ٿ���
{
    
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

    void Start()
    {
        currentHp = maxHp;

        hpBar = Util.FindChild<HpBar>(gameObject, null, true);    //gameObject �ڽĵ� �� HpBar(������Ʈ)�� ����ִ� �ڽ� ����� HpBar(������Ʈ) ��ȯ

        // type�� �ʱ�ȭ�ϰ� objectField�� �޾ƿ� ��, objectList�� PlayerField�� �޾ƿ´�.
        type = 1;
        objectField = Managers.Field.getField();            //(ex)objectField�� BasicField(��ũ��Ʈ)�� ���Ե�
        objectList = objectField.getGridArray(type);        //playergridArray�� ������

        currentInd = objectList.Count / 2; // �� �ʱ�ȭ�� ��ġ�� Field�� Width�� ���, ����� ������ �� ���� (1.18 ���� �߰�)
        transform.position = objectList[currentInd].transform.position;
      
    }

    void Update()
    {
        BitBehave();
    }

    protected override void BitBehave()     //Player�� BitBehave()�� Update()�� �ȿ� �ִ� ���� �´�!
    {
        //�� �ٲٴ°� FieldObject�� �Լ��� ����?
        //���� currentInd�� �ٽ� ���� ������ ��������
        SpriteRenderer temp = objectList[currentInd].GetComponent<SpriteRenderer>();
        temp.color = new Color(87/255f, 87 / 255f, 87 / 255f,1);        //Isolated Diamond(prefab)�� ��

        if (Input.GetKeyDown(KeyCode.W) && Managers.Timing.CheckTiming()) { mayGo(Define.PlayerMove.Up); Managers.Sound.Play("Click"); }
        else if (Input.GetKeyDown(KeyCode.A) && Managers.Timing.CheckTiming()) { mayGo(Define.PlayerMove.Left); Managers.Sound.Play("Click"); }
        else if (Input.GetKeyDown(KeyCode.S) && Managers.Timing.CheckTiming()) { mayGo(Define.PlayerMove.Down); Managers.Sound.Play("Click"); }
        else if (Input.GetKeyDown(KeyCode.D) && Managers.Timing.CheckTiming()) { mayGo(Define.PlayerMove.Right); Managers.Sound.Play("Click"); }
        else if (Input.GetKeyDown(KeyCode.K) && Managers.Timing.CheckTiming()) { Attack(); Managers.Sound.Play("KnifeAttack1"); }

        //����� currentInd�� ���� �ٲ���
        temp = objectList[currentInd].GetComponent<SpriteRenderer>();
        temp.color = Color.magenta;
    }

    protected override void Attack()
    {
        int[] pattern = GetComponent<Weapon>().CalculateAttackRange(currentInd);      //������ ���⿡ �´� ���ݹ��� ���
        //Managers.Field.WarningAttack(pattern);                                      //���ݹ��� ����ȭ(�ʹ� ª�� �ð�)

        //Invoke("AttackTempColor", 0.2f);                                               
        Managers.Field.Attack(pattern);                                               //���ݹ��� collider Ȱ��ȭ + ����ȭ
        Managers.Field.AttackedArea(pattern);
        //Managers.Field.WarningAttack(pattern);    //���ݹ��� ����ȭ(�ʹ� ª�� �ð�) //�߸ŷ� Player�� Attack()�� WarningAttack() ���� �ٲ� 

        //Invoke("GridOriginalColor", 0.2f);        //Field(��ũ��Ʈ)�� GridOriginalColor�� �Լ� ���� «

        Transform attack = transform.GetChild(0);                                       //Attack(GameObject)��ȯ    
        attack.GetComponent<PlayerAttack>().Attacking();                                //PlayerAttack(��ũ��Ʈ)�� Attacking()�Լ� ����
    }



    

   /* //���� �� �����Ѵٸ� Field(��ũ��)�� �ִ� ���� ����(Invoke()���� �Ű������� �ѱ� ����)(�ڷ�ƾ ���?)
    private void GridOriginalColor(int[] indexs)        //player�� ����ȭ�� ������ Grid�� �ٽ� ���� ������ �����ִ� �Լ�
    {
        Debug.Log("GridOriginalColor �����");
        List<GameObject> gridArray =  Managers.Field.getField().getGridArray(3);
        for (int i = 0; i < indexs.Length; i++)
        {
            SpriteRenderer temp = gridArray[indexs[i]].GetComponent<SpriteRenderer>();
            temp.color = new Color(87 / 255f, 87 / 255f, 87 / 255f, 1);
        }
    }*/

    protected override void Hit()       
    {
        Debug.Log("Hit!!!!");
        GetComponent<Animator>().SetTrigger("isHit");       //Player�� Animator�� Idle ���� Hit������ transition�� ���µ�???
        CurrentHp -= 1;
        Managers.Sound.Play("Hit");
        if (CurrentHp <= 0)
            Die();

    }
    void Die()
    {
        Debug.Log("Player Die!!");
        Managers.Game.GameOver();
        Managers.Sound.Play("Die");
        
    }
    private void OnTriggerEnter2D(Collider2D collision)     //������ �������� PlayerField�� collider�� ���� �浹 ��
    {
        Hit();
    }

}
