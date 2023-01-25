using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTest : FieldObject
{
    double currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        // type�� �ʱ�ȭ�ϰ� objectField�� �޾ƿ� ��, objectList�� PlayerField�� �޾ƿ´�.
        type = 2;
        objectField = Managers.Field.getField();
        objectList = objectField.getGridArray(type);

        currentInd = objectList.Count - (objectList.Count / 2); // player�� ���������� �� ���� �� �߾ӿ� �����ϴ� �ʱ�ȭ�̴�. (1.18 ���� �߰�)
        transform.position = objectList[currentInd].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 60d / Managers.Bpm.BPM)
        {
            BitBehave();
            currentTime -= 60d / Managers.Bpm.BPM;
        }
    }
    protected override void BitBehave()
    {
        // ������ ���������� ���� randNum ���� (1.17 ���� �߰�) -> �� �κ��� ���Ϳ� ���� �ٸ��� �����ϸ� �Ǵ� �κ��̹Ƿ� ���� ���ɼ� ����
        int randNum = Random.Range(0, 5);
        if(randNum == 0)
        {
            mayGo(Define.PlayerMove.Up);
        }
        else if(randNum == 1)
        {
            mayGo(Define.PlayerMove.Down);
        }
        else if (randNum == 2)
        {
            mayGo(Define.PlayerMove.Left);
        }
        else if (randNum == 3)
        {
            mayGo(Define.PlayerMove.Right);
        }
        else if(randNum == 4)
        {

        }
        else
        {

        }
    }

    protected override void Attack()
    {

    }

    void AttackReady()
    {

    }

    protected override void Hit()
    {

    }
}
