using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� Ŭ������ PlayerTextŬ�����̸� FieldObject�� ��ӹ޾� ����. FieldObject�� MonoBehaviour�� ��ӹް� �ִ�. (1.17 ���� �߰�)
// Issue (1.17) ó�� ������ �� Ű���� �ѹ��� �����ϰ� ������
public class PlayerTest : FieldObject
{
    
    // Start is called before the first frame update
    void Start()
    {
        // type�� �ʱ�ȭ�ϰ� objectField�� �޾ƿ� ��, objectList�� PlayerField�� �޾ƿ´�.
        type = 1;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);
        
        currentInd = objectList.Count / 2 - 1; // �� �ʱ�ȭ�� ��ġ�� Field�� Width�� ���, ����� ������ �� ���� (1.18 ���� �߰�)
        transform.position = objectList[currentInd].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BitBehave();
    }

    protected override void BitBehave()
    {
        if (!Managers.Bpm.Able)
            return;
        if (Input.GetKeyDown(KeyCode.W))
            mayGo(Define.PlayerMove.Up);
        else if (Input.GetKeyDown(KeyCode.A))
            mayGo(Define.PlayerMove.Left);
        else if (Input.GetKeyDown(KeyCode.S))
            mayGo(Define.PlayerMove.Down);
        else if (Input.GetKeyDown(KeyCode.D))
            mayGo(Define.PlayerMove.Right);
        else if (Input.GetKeyDown(KeyCode.K))
            Attack();
    }

    protected override void Attack()
    {
        // Hierachy �󿡼� Player ���� �ִϸ��̼��� �޾ƿ��ڴٴ� �̾߱�
        Transform attack = transform.GetChild(0);
        attack.GetComponent<PlayerAttack>().Attacking();
    }

    protected override void Hit()
    {
        // hit �������̵� �ϱ�
    }
}
