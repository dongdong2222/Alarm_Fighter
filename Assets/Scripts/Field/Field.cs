using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Field : MonoBehaviour
{
    List<GameObject> gridArray = new List<GameObject>();//��ü field data
    List<GameObject> playergridArray = new List<GameObject>();//Player field data
    List<GameObject> monstergridArray = new List<GameObject>();//Monster field data

    protected GameObject grid_All;

    [SerializeField]
    GameObject gridPrefab;//Diamond

    protected float height;//����
    protected float width;//����

    private float scale_x;//������ x��
    private float scale_y;//������ y��
    private float location_x;//grid�� ó�� x��ǥ 
    private float location_y;//grid�� ó�� y��ǥ

    const double x_size = 0.5;//Diamond ����
    const double y_size = 0.25;//Diamond ����
    const double gap = 0.1;//����

    public abstract void Setheight();
    public abstract void setWidth();
    public void Rotation(GameObject go)//�⺻ ȸ��
    {
        go.transform.Rotate(0f, 0f, 30.0f);
    }
    protected virtual void prepabRotation(GameObject go, float theta)//theta��ŭ ȸ�� �Լ�
    {
        go.transform.Rotate(0f, 0f, theta);
    }

    protected virtual void prepabMove(GameObject go)
    {
        go.transform.position = new Vector3(-1.49f, 0.54f, 0f);
    }
    private void gridInit()
    {
        scale_x = gridPrefab.transform.localScale.x;//grid scale x��
        scale_y = gridPrefab.transform.localScale.y;//grid scale y��
        location_x = gridPrefab.transform.localPosition.x;//grid �ʱ� x��ǥ 
        location_y = gridPrefab.transform.localPosition.y;//grid �ʱ� y��ǥ
    }
    protected virtual void createObject()
    {
        grid_All = new GameObject() { name = "grid_All" };

    }
    protected virtual void createField()//�ʵ� �����
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject grid = Instantiate(gridPrefab) as GameObject;
                grid.transform.position = new Vector3((float)(((x_size + gap) * scale_x * x + (x_size + gap) * scale_x * y) + location_x), (float)(((-gap - y_size) * scale_y * x + (gap + y_size) * scale_y * y) + location_y), 0f);
                grid.transform.SetParent(grid_All.transform);//�ϳ��� ��ġ��
                gridArray.Add(grid);//������ ���� ����
            }
        }
    }
    protected virtual void seperatedGridArea()
    {
        for (int i = 0; i < gridArray.Count; i++)//Basic Player Field 
        {
            if (i < 6) playergridArray.Add(gridArray[i]);
            if (i > 6) monstergridArray.Add(gridArray[i]);
        }

    }
    void Start()
    {
        setWidth();
        Setheight();
        gridInit();
        createObject();
        createField();
        Rotation(grid_All);
        prepabMove(grid_All);
        seperatedGridArea();
    }
}