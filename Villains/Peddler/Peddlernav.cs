using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Peddlernav : MonoBehaviour
{
    public Transform player;
    NavMeshAgent nvAgent;
    public float followRange = 3.0f; // ���� ����

    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    { // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // �Ÿ��� ���� ���� �̳��� ��� �÷��̾ ����
        if (distanceToPlayer <= followRange)
        {
            nvAgent.SetDestination(player.position);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(NavmeshStop());
        }
    }
    private IEnumerator NavmeshStop()
    {
        Debug.Log("���󰡱� ����");
        nvAgent.enabled = false;
        yield return new WaitForSeconds(2f);
        nvAgent.enabled = true;
    }

}
