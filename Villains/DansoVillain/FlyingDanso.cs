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
        rb.AddForce(force, ForceMode.Impulse); // ���� ��� ����
        Destroy(gameObject, time); // ���� �ð� �� ������Ʈ ����
       
        
    }

    // �浹 �߻� �� ȣ��Ǵ� �޼���
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FireEx fireEx = collision.gameObject.GetComponent<FireEx>();
            Debug.Log("�÷��̾�� �浹�߽��ϴ�!");
            if (fireEx != null)
            {
                fireEx.isSteamActive = false; // Steam ��Ȱ��ȭ
            }
            Destroy(gameObject); // �浹 �� ��� ����
        }
        else
        {
            Destroy(gameObject); // �ٸ� ������Ʈ�� �浹 �� ��� ����
        }
    }
}
