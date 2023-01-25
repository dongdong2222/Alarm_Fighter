using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo //Pool�� �����ϰ��� �ϴ� ������Ʈ ������ ����
{
    public GameObject notePrefab;   //������ Prefab
    public int count;   //������ Prefab�� ����
    public Transform poolParent;    //Prefab�� ���� �� �θ�� �� ������Ʈ
}


public class ObjectPool : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfo = null;
    public static ObjectPool objectPool; //��𼭵� ���� ���� �ϱ� ���ؼ�

    public Queue<GameObject> noteQueue = new Queue<GameObject>();   //�������� �ϳ��� Pool

    void Start()
    {
        objectPool = this;
        noteQueue = CreatePool(objectInfo[0]);
        //noteQueue2 = CreatePool(objectInfo[1]);   //�����ϰ��� �ϴ� Pool�� �� ���� �� ���
    }

    Queue<GameObject> CreatePool(ObjectInfo objectInfo) //Pool ����
    {
        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < objectInfo.count; i++)
        {
            //Prefab Scene�� ����
            GameObject obj = Instantiate(objectInfo.notePrefab, transform.position, Quaternion.identity);
            obj.SetActive(false);//�������ڸ��� ��Ȱ��ȭ
            //obj�� �� �θ� ������Ʈ ����
            if (objectInfo.poolParent != null)
            {
                obj.transform.SetParent(objectInfo.poolParent);
            }
            else
            {
                obj.transform.SetParent (this.transform);
            }
            pool.Enqueue(obj); //������ obj�� Pool�� ����
        }

        return pool; //���� ���� Pool ��ȯ
            
    }

}


