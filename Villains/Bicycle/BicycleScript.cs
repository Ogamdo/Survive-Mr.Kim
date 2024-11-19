using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BicycleScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent nvAgent;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        nvAgent.SetDestination(player.position);
        anim.SetBool("Kim", true);
    }
}
