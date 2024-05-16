using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject firePrefab; // ��ƼŬ ������
    public int minFireCount = 1;  // �ּ� ���� ����
    public int maxFireCount = 10; // �ִ� ���� ����
    public BoxCollider spawnRange; // FireSpawnRange BoxCollider

    // Start is called before the first frame update
    void Start()
    {
        // ��ƼŬ�� ������ ������ �����մϴ�.
        int fireCount = Random.Range(minFireCount, maxFireCount);

        for (int i = 0; i < fireCount; i++)
        {
            SpawnFire();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnFire()
    {
        // BoxCollider�� ���� ������ ������ ��ġ�� ����մϴ�.
        Vector3 randomPosition = GetRandomPositionInBox(spawnRange);

        // ��ƼŬ �������� �����մϴ�.
        GameObject fireInstance = Instantiate(firePrefab, randomPosition, Quaternion.identity);
        fireInstance.AddComponent<FireController>(); // FireController ��ũ��Ʈ�� �߰��Ͽ� �Ҹ� ������ ó���մϴ�.
    }

    Vector3 GetRandomPositionInBox(BoxCollider box)
    {
        Vector3 boxSize = box.size;
        Vector3 boxCenter = box.center;

        // BoxCollider�� ���� ��ǥ�� �۷ι� ��ǥ�� ��ȯ
        Vector3 randomPosition = new Vector3(
            Random.Range(boxCenter.x - boxSize.x / 2, boxCenter.x + boxSize.x / 2),
            Random.Range(boxCenter.y - boxSize.y / 2, boxCenter.y + boxSize.y / 2),
            Random.Range(boxCenter.z - boxSize.z / 2, boxCenter.z + boxSize.z / 2)
        );

        return box.transform.TransformPoint(randomPosition);
    }
}
