using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    double currentTime = 0;

    [SerializeField] Transform noteAppearLocation = null;//notePrefab�� ������ ��ġ
    //[SerializeField] GameObject notePrefab = null;//������ Note ������ ����

    TimingManager timingManager;
    
    void Start()
    {
        //timingManager = Managers.Timing;
        timingManager = FindObjectOfType<TimingManager>();
    }
    


    public void Update()
    {
        //Ư�� �ð� �������� ��Ʈ ����
        currentTime += Time.deltaTime;
        if (currentTime >= 60d / Managers.Bpm.BPM)
        {
            GameObject t_note = ObjectPool.objectPool.noteQueue.Dequeue();//notePool���� obj(Note) �ϳ� ����
            t_note.transform.position = noteAppearLocation.position;//obj�� Scene�� Ȱ��ȭ�� �ڸ� ����
            t_note.SetActive(true); //������ obj Scene�� Ȱ��ȭ
            //GameObject t_note = GameObject.Instantiate(notePrefab, noteAppearLocation.position, Quaternion.identity);
            //t_note.transform.SetParent(this.transform);
            timingManager.noteList.Add(t_note);//TimingManager2�� noteList�� ������ Note �߰�
            currentTime -= 60d / Managers.Bpm.BPM;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //��Ʈ�� ȭ�� ������ ������ ����
        if (collision.CompareTag("Note2"))
        {
            timingManager.noteList.Remove(collision.gameObject);//TimingManager2�� noteList���� ����
            ObjectPool.objectPool.noteQueue.Enqueue(collision.gameObject);//notePool�� obj(Note) ��ȯ
            collision.gameObject.SetActive(false);//obj ��Ȱ��ȭ

            //Destroy(collision.gameObject);
        }
    }
}
