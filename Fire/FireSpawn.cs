using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour
{
    [Header("불의 모양이 될 Prefab")]
    public GameObject firePrefab; // 생성할 불꽃 프리팹

    [Header("불꽃의 개수")]
    public int minFireCount = 6; // 최소 생성 개수
    public int maxFireCount = 10; // 최대 생성 개수
    private int fireCount=6; // 생성할 불꽃의 개수

    [Header("불꽃의 생성 범위와 대기 시간.")]
    public BoxCollider spawnRange; // 불꽃 생성 범위
    public float delayBeforeSpawn = 5f; // 생성 전 대기 시간
    public Transform fireSpwanObject; // 부모 오브젝트 참조

    void Start()
    {
        // [변경점 1] fireCount 초기화 위치 변경
        // 기존에는 필드 선언부에서 Random.Range로 초기화했으나,
        // Unity의 필드 초기화 순서에 따라 Inspector에서 설정한 값을 반영하지 못할 수 있어 Start에서 초기화
        fireCount = Random.Range(minFireCount, maxFireCount);

        // [변경점 2] Invoke 오타 수정
        // 기존 코드에서는 "InVoke"로 오타가 있었음.
        Invoke(nameof(SpawnFire), delayBeforeSpawn);
    }

    void SpawnFire()
    {
        // [변경점 3] BoxCollider 참조 수정
        // 기존 코드에서 "box"라는 정의되지 않은 변수를 참조했으나, spawnRange로 수정
        Vector3 boxSize = spawnRange.size;
        Vector3 boxCenter = spawnRange.center;

        for (int i = 0; i < fireCount; i++) // [변경점 4] while 루프를 for 루프로 변경
        {
            // 기존에는 while 루프와 count 변수를 사용했으나,
            // for 루프를 사용해 가독성과 안전성을 향상
            Vector3 fireSpwanRanPos = new Vector3(
                Random.Range(boxCenter.x - boxSize.x / 2, boxCenter.x + boxSize.x / 2),
                Random.Range(boxCenter.y - boxSize.y / 2, boxCenter.y + boxSize.y / 2),
                Random.Range(boxCenter.z - boxSize.z / 2, boxCenter.z + boxSize.z / 2)
            );

            // [변경점 5] 랜덤 위치 변수 이름 수정 및 올바른 참조 사용
            // 기존 코드에서는 randomPosition 변수를 선언하지 않고 사용했으나,
            // fireSpwanRanPos로 변수명을 통일하여 사용
            Instantiate(firePrefab, spawnRange.transform.TransformPoint(fireSpwanRanPos), Quaternion.identity, fireSpwanObject);
        }
    }
}
