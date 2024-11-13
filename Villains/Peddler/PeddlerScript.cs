using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeddlerScript : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Kim", true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 forceDirection = collision.transform.position - transform.position;
                forceDirection.y = 0; // y�� �̵� �����Ͽ� �������θ� �б�
                forceDirection.Normalize(); // ���� ���͸� ���� ���ͷ� ����


                playerRigidbody.AddForce(forceDirection * pushForce, ForceMode.Impulse);
            }
        }
    }
}
