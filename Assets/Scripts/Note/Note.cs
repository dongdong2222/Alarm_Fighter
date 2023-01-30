using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 0;

    Image noteImage;//Note(��ü)�� Image 

    void OnEnable()//��ü�� Ȱ��ȭ �� �� ���� ����
    {
        if (noteImage == null)
        {
            noteImage = GetComponent<Image>();
        }
        noteImage.enabled = true;//space �� �������� ��ü�� �̹����� ��Ȱ��ȭ �Ͽ��� ������ ����
    }

    private void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;  

    }

    //Note�� �̹����� ��Ȱ��ȭ �ϴ� �Լ�
    public void HideNote()
    {
        noteImage.enabled = false;//enabled�� SetActive�Լ��ʹ� �޸� �Ѱ��� ������Ʈ�� ��Ȱ��Ȱ�� ���
    }
    //--------------------------------------------------------------------------
    public void CreateNote()
    {
        Transform parent = transform.parent;
        GameObject go = Managers.Resource.Instantiate("Note", parent);
        go.GetComponent<Animator>().speed = Managers.Bpm.GetAnimSpeed();
    }

    public void Destroy()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
