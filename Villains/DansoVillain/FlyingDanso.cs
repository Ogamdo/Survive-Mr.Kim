using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDanso : MonoBehaviour
{
    [SerializeField] float time = 3.0f;
    [SerializeField] Vector3 force = Vector3.zero;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse); // 힘을 즉시 적용
        Destroy(gameObject, time); // 일정 시간 후 오브젝트 삭제
       
        
    }

    // 충돌 발생 시 호출되는 메서드
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FireEx fireEx = collision.gameObject.GetComponent<FireEx>();
            Debug.Log("플레이어와 충돌했습니다!");
            if (fireEx != null)
            {
                fireEx.isSteamActive = false; // Steam 비활성화
            }
            Destroy(gameObject); // 충돌 시 즉시 삭제
        }
        else
        {
            Destroy(gameObject); // 다른 오브젝트와 충돌 시 즉시 삭제
        }
    }
}
