using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DansoV : MonoBehaviour
{   
    //Namvesh ���� ������Ʈ�� ����
    private NavMeshAgent navAgent; 
    private float dis; // �ܼҿ� ���� ������ �Ÿ�
  public Transform dansoTr;//�ܼ��� position�� �����ϱ� ���� ����
    [SerializeField] GameObject danso;//�ܼ�

    private Transform villainTr; // ������ Ʈ������
   
    private bool dansoReady; //danso�� ���� ��ó�� �ִ��� �ľ��Ѵ�.
   
    private Rigidbody rb; //danso�� Rigidbody ��������
    [SerializeField] Vector3 force = new Vector3(3, 1, 100); // ��ü�� �߻��� ���� ����� ũ�� ����

    void Start()
    
    {   dansoTr = danso.GetComponent<Transform> ();
        rb = danso.GetComponent<Rigidbody>();
        dansoReady = true;
        villainTr = GetComponent<Transform>();
     
        navAgent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {  
         if (!dansoReady){

        FindDanso();

            }
        else if(dansoReady){
            Throwing();

                }
      
    }

    void Throwing()
        {
       
     rb.AddRelativeForce (force, ForceMode.Impulse);
     dansoReady =false;
      Debug.Log("dansoready: " + dansoReady);
        }
      
    

    void FindDanso()
    {
        dis = Vector3.Distance(villainTr.position, dansoTr.position);
          navAgent.destination = dansoTr.position;  
        if (dis<1){
        dansoReady = true;
          Debug.Log("dansoready: " + dansoReady);
    }
        
    }
}
