using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBounce : MonoBehaviour
{
    void Start()
    {
        Managers.Timing.BehaveAction -= Bounce;      
        Managers.Timing.BehaveAction += Bounce;
        
    }

    public void Bounce()                         //�� ��Ʈ���� Ÿ���� �ٿ
    {
       //GetComponent<Animator>().Play("Bounce");     //Bounce(AnimationClip) ���
       GetComponent<Animator>().SetTrigger("Bounce");
        Debug.Log("Bounced");
    }
    
    
    
}
