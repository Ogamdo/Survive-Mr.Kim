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
        //fireCount는 minFireCount, maxFireCount의 범위내에서 정수를 설정하고 설정된 정수만큼 SpawnFire()메서드를 반복한다.
        int fireCount = Random.Range(minFireCount, maxFireCount);

        for (int i = 0; i < fireCount; i++)
        {
            SpawnFire();
        }
    }

    void SpawnFire()
    {
       
        //randomPosition 3차원 변수는 spawnRange로 설정한 3차원 박스내에 범위에서 위치한다.
        Vector3 randomPosition = GetRandomPositionInBox(spawnRange);

        // firePrefab이 randomPosition 위치에서 회전 없이 생성되고 생성된 오브젝트는 fireInstance라는 변수에 저장한다.
        //Instantiate은 프리펩을 복제하는함수
        GameObject fireInstance = Instantiate(firePrefab, randomPosition, Quaternion.identity);
 
        fireInstance.AddComponent<FireController>();
    }

    Vector3 GetRandomPositionInBox(BoxCollider box)
    {
        //Vector3의 값을 가져온다
        Vector3 boxSize = box.size;
        Vector3 boxCenter = box.center;

        //가져온 박스내의 임의의 위치를 계산하고 Random.Range()안에 설정한 범위를 반환해서 무작위 자표를 설정 
        Vector3 randomPosition = new Vector3(
            Random.Range(boxCenter.x - boxSize.x / 2, boxCenter.x + boxSize.x / 2),
            Random.Range(boxCenter.y - boxSize.y / 2, boxCenter.y + boxSize.y / 2),
            Random.Range(boxCenter.z - boxSize.z / 2, boxCenter.z + boxSize.z / 2)
        );
        
        //무작위 자표값이 담긴 randomPosition 변수의 로컬 좌표값을 월드 좌표로 변환
        return box.transform.TransformPoint(randomPosition);
    }
}

