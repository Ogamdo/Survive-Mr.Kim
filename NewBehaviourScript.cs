using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawn : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider rangeCollider;

    public GameObject Sphere;


    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider>();
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Z = rangeCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    private void Start()
    {
        StartCoroutine(RandomRespawn_Coroutine());
    }

    IEnumerator RandomRespawn_Coroutine()
    {
        //while (true)
        //  {
        //  yield return new WaitForSeconds(1f);

        // 생성 위치 부분에 위에서 만든 함수 Return_RandomPosition() 함수 대입
        // GameObject instantCapsul = Instantiate(Sphere, Return_RandomPosition(), Quaternion.identity);
       // }

        int numberOfSpawns = Random.Range(1, 5);

        for (int i = 0; i < numberOfSpawns; i++)
        {
            // 생성 위치 부분에 위에서 만든 함수 Return_RandomPosition() 함수 대입
            GameObject instantCapsul = Instantiate(Sphere, Return_RandomPosition(), Quaternion.identity);
        }

        yield return null; 
    }
}
