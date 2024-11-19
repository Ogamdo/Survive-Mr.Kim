using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDanso : MonoBehaviour
{
    [SerializeField] float time = 3.0f;
    [SerializeField] Vector3 force = Vector3.zero;
  
    private Rigidbody rb;
    private Transform tr;
    public int speedRot = 100;
    
    public float rotX = 15f;
    public float rotY = 15f;
    public float rotZ = 30f;
    
    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        rb.AddForce(force, ForceMode.Impulse); // ���� ��� ����
        
        Destroy(gameObject, time); // ���� �ð� �� ������Ʈ ����
       
        
    }
    void FixedUpdate(){
        tr.Rotate(rotX, rotY, rotZ*speedRot*Time.deltaTime);
    }

    // �浹 �߻� �� ȣ��Ǵ� �޼���
   public void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        FireEx fireEx = other.GetComponent<FireEx>();
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