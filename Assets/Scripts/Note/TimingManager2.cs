using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TimingManager2 : MonoBehaviour
{


  /*  // �����Ǵ� �� note�� ���� List
    public List<GameObject> noteList = new List<GameObject>();

    [SerializeField] Transform centerFlame = null;  //CenterFlame�� ��ġ
    [SerializeField] RectTransform[] timingRect = null; //�����ִ� �̹��� �ڽ�
    Vector2[] timingRange = null; //timingRect�� x����

    void Start()
    {
        timingRange = new Vector2[timingRect.Length]; //ũ�� 4

        for (int i = 0; i < timingRect.Length; i++)
        {
            //timingRange[0]�� perfectRect�� ���� ��
            timingRange[i] = new Vector2(timingRect[i].localPosition.x - timingRect[i].rect.width / 2,
                timingRect[i].anchoredPosition.x + timingRect[i].rect.width / 2);
        }
    }

    //������ Note�� timingRange�� ���ϴ� Note�� �ִ��� Ȯ��
    public void CheckTiming()
    {
        for (int i = 0; i < noteList.Count; i++)//������ Note�� ���� Ȯ��
        {
            float notePosx = noteList[i].transform.localPosition.x;// Note�Ѱ��� x��

            for(int j = 0; j < timingRange.Length; j++)//4���� timingRange�� ���ϴ� Ȯ��
            {
                if((timingRange[j].x <= notePosx) && (notePosx <= timingRange[j].y))
                {
                    //Note�� timingRange�� ���ϸ� �ش� Note ����
                    //Destroy(noteList[i]);
                    noteList[i].GetComponent<Note>().HideNote();//Note ���� ��ſ� Note�� �̹����� ��Ȱ��ȭ//����: BGM�� �� ����
                    noteList.RemoveAt(i);
                    Debug.Log("HIT" + j);
                    return;
                }
            }
        }


        Debug.Log("Miss");//������ Note���� timingRange�� ������ ������ Miss
    }*/
    
}
