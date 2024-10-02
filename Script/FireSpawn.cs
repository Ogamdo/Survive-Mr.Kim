using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject firePrefab; 
    public int minFireCount = 1;  
    public int maxFireCount = 10; 
    public BoxCollider spawnRange; 

    // Start is called before the first frame update
    void Start()
    {
        //fireCount�� minFireCount, maxFireCount�� ���������� ������ �����ϰ� ������ ������ŭ SpawnFire()�޼��带 �ݺ��Ѵ�.
        int fireCount = Random.Range(minFireCount, maxFireCount);

        for (int i = 0; i < fireCount; i++)
        {
            SpawnFire();
        }
    }

    void SpawnFire()
    {
       
        //randomPosition 3���� ������ spawnRange�� ������ 3���� �ڽ����� �������� ��ġ�Ѵ�.
        Vector3 randomPosition = GetRandomPositionInBox(spawnRange);

        // firePrefab�� randomPosition ��ġ���� ȸ�� ���� �����ǰ� ������ ������Ʈ�� fireInstance��� ������ �����Ѵ�.
        //Instantiate�� �������� �����ϴ��Լ�
        GameObject fireInstance = Instantiate(firePrefab, randomPosition, Quaternion.identity);
 
        fireInstance.AddComponent<FireController>();
    }

    Vector3 GetRandomPositionInBox(BoxCollider box)
    {
        //Vector3�� ���� �����´�
        Vector3 boxSize = box.size;
        Vector3 boxCenter = box.center;

        //������ �ڽ����� ������ ��ġ�� ����ϰ� Random.Range()�ȿ� ������ ������ ��ȯ�ؼ� ������ ��ǥ�� ���� 
        Vector3 randomPosition = new Vector3(
            Random.Range(boxCenter.x - boxSize.x / 2, boxCenter.x + boxSize.x / 2),
            Random.Range(boxCenter.y - boxSize.y / 2, boxCenter.y + boxSize.y / 2),
            Random.Range(boxCenter.z - boxSize.z / 2, boxCenter.z + boxSize.z / 2)
        );
        
        //������ ��ǥ���� ��� randomPosition ������ ���� ��ǥ���� ���� ��ǥ�� ��ȯ
        return box.transform.TransformPoint(randomPosition);
    }
}

