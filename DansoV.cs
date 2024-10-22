using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DansoV : MonoBehaviour
{   
    //Namvesh 관련 오브젝트들 선언
    private NavMeshAgent navAgent; 
    private float dis; // 단소와 빌런 사이의 거리
  public Transform dansoTr;//단소의 position을 추적하기 위한 변수
    [SerializeField] GameObject danso;//단소

    private Transform villainTr; // 빌런의 트랜스폼
   
    private bool dansoReady; //danso가 빌런 근처에 있는지 파악한다.
   
    private Rigidbody rb; //danso의 Rigidbody 가져오기
    [SerializeField] Vector3 force = new Vector3(3, 1, 100); // 물체를 발사할 힘의 방향과 크기 설정

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
