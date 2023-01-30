using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object �� pool
class Pool
{
    //pool�� ���� ������Ʈ�� ����
    public GameObject Original { get; private set; }
    //pool�� root ������Ʈ : push�� ������Ʈ�� �θ� ������Ʈ
    public Transform Root { get; set; }

    //pool�� push�� ������Ʈ�� ����ִ� stack
    Stack<Poolable> _poolStack = new Stack<Poolable>();

    //pool�� root������Ʈ ����
    public void Init(GameObject original, int count = 5)
    {
        Original = original;
        Root = new GameObject().transform;
        Root.name = $"{original.name}_ROOT";

        for (int i = 0; i < count; i++)
            Push(Create());

        }

    //pool�� �� object ����
    Poolable Create()
    {
        GameObject go = Object.Instantiate<GameObject>(Original);
        go.name = Original.name;
        return go.GetOrAddComponent<Poolable>();
    }

    //������Ʈ�� pool�� ����ִ´� (= Destroy)
    public void Push(Poolable poolable)
    {
        if (poolable == null)
            return;

        //poolable�� pool�� �ű�� ��Ȱ��ȭ
        poolable.transform.parent = Root;
        poolable.gameObject.SetActive(false);
        poolable.IsUsing = false;

        _poolStack.Push(poolable);
    }

    //������Ʈ�� pool���� ������ (= Instantiate)
    public Poolable Pop(Transform parent)
    {
        //pop�� ������Ʈ
        Poolable poolable;

        //pool�� ���� ������Ʈ�� �ִٸ� 
        if (_poolStack.Count > 0)
            poolable = _poolStack.Pop();
        //���ٸ� ����
        else
            poolable = Create();

        //������Ʈ Ȱ��ȭ
        poolable.gameObject.SetActive(true);

        //DontDestroyOnLoad ������ : DontDestroyOnLoad ���Ͽ� ���� ��������� �������ʴ��� ������
        //if(parent == null)
        //    poolable.transform.parent = Managers.Scene.CurrentScene.transform;

        poolable.transform.parent = parent;
        poolable.IsUsing = true;

        return poolable;

    }
}
public class PoolManager
{
    //object name�� key�� ���� pool dictionary
    Dictionary<string, Pool> _poolDict = new Dictionary<string, Pool>();
    //pool���� �θ� ������Ʈ
    Transform _root;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    //pool �����ϰ� dictionary�� �߰�
    public void CreatePool(GameObject original, int count = 5)//prefab��ü�� pool�� ��� ������ΰ�
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _poolDict.Add(original.name, pool);
    }

    //pool�� �ִٸ� pool�� push ������ destroy
    public void Push(Poolable poolable)
    {
        //poolable�� pool�� ���ٸ� �׳� destroy
        string name = poolable.gameObject.name;
        if(_poolDict.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _poolDict[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent =null)
    {
        //pool�� ���ٸ� pool���� �� pop
        if (_poolDict.ContainsKey(original.name) == false)
            CreatePool(original);

        return _poolDict[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_poolDict.ContainsKey(name) == false)
            return null;
        return _poolDict[name].Original;
    }

    //Scene �̵��� ��� pool������Ʈ ����
    public void Clear()
    {
        foreach(Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }

        _poolDict.Clear();
    }
}
