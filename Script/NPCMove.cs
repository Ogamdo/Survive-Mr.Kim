using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(SetNPCMTrueAfterDelay(6f)); // 6초 대기 후 anim.SetBool("NPCM", true) 실행
    }

    IEnumerator SetNPCMTrueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("NPCM", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
