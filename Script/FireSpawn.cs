using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject firePrefab; // 파티클 프리팹
    public int minFireCount = 1;  // 최소 생성 갯수
    public int maxFireCount = 10; // 최대 생성 갯수
    public BoxCollider spawnRange; // FireSpawnRange BoxCollider

    // Start is called before the first frame update
    void Start()
    {
        // 파티클을 랜덤한 갯수로 생성합니다.
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
        // BoxCollider의 범위 내에서 랜덤한 위치를 계산합니다.
        Vector3 randomPosition = GetRandomPositionInBox(spawnRange);

        // 파티클 프리팹을 생성합니다.
        GameObject fireInstance = Instantiate(firePrefab, randomPosition, Quaternion.identity);
        fireInstance.AddComponent<FireController>(); // FireController 스크립트를 추가하여 소멸 로직을 처리합니다.
    }

    Vector3 GetRandomPositionInBox(BoxCollider box)
    {
        Vector3 boxSize = box.size;
        Vector3 boxCenter = box.center;

        // BoxCollider의 로컬 좌표를 글로벌 좌표로 변환
        Vector3 randomPosition = new Vector3(
            Random.Range(boxCenter.x - boxSize.x / 2, boxCenter.x + boxSize.x / 2),
            Random.Range(boxCenter.y - boxSize.y / 2, boxCenter.y + boxSize.y / 2),
            Random.Range(boxCenter.z - boxSize.z / 2, boxCenter.z + boxSize.z / 2)
        );

        return box.transform.TransformPoint(randomPosition);
    }
}
