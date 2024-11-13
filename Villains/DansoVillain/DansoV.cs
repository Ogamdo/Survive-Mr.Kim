using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DansoV : MonoBehaviour
{
    [SerializeField] float speedRot = 5; // Speed of rotation for this character
    
    private NavMeshAgent nav; // Reference to the NavMeshAgent component
    private float dis; // Distance to the player
    private Transform tr; // Transform component of this game object
 
    [SerializeField] GameObject danso; // Prefab for the projectile (Danso)
    private GameObject player; // Reference to the player object
    
    [SerializeField] float speed = 10f; // Movement speed of the NavMeshAgent
    [SerializeField] float range = 5.5f; // Range within which the player can be detected
    
    // Public coroutine to handle projectile tossing, so it can be started and stopped as needed
    public Coroutine tossCoroutine;

    void Start()
    {
        tr = GetComponent<Transform>(); // Initialize the Transform component
        nav = GetComponent<NavMeshAgent>(); // Initialize the NavMeshAgent component
        nav.speed = speed; // Set the movement speed of the NavMeshAgent
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag "Player"
    }

    void FixedUpdate()
    {
        // Calculate the distance between the player and this game object
        dis = Vector3.Distance(player.transform.position, tr.position);
        
        // If the player is within range
        if (dis < range)
        {
            // Start the toss coroutine if it is not already running
            if (tossCoroutine == null)
            {
                tossCoroutine = StartCoroutine(TossDansoCoroutine());
            }
        }
        else
        {
            // If the player is out of range, stop the coroutine if it is running
            if (tossCoroutine != null)
            {
                StopCoroutine(tossCoroutine);
                tossCoroutine = null;
            }
            // Continue to follow the player
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        // Set the destination of the NavMeshAgent to the player's position
        nav.SetDestination(player.transform.position);
        
        // Rotate smoothly towards the player
        Vector3 direction = (player.transform.position - tr.position).normalized;
        Quaternion Rotation = Quaternion.LookRotation(direction);
        tr.rotation = Quaternion.Slerp(tr.rotation, Rotation, Time.deltaTime * speedRot);
    }

    IEnumerator TossDansoCoroutine()
    {
        while (true) // Loop infinitely
        {
            tossDanso(); // Throw the projectile
            yield return new WaitForSeconds(1f); // Wait for 1 second before repeating
        }
    }

    void tossDanso()
    {
        // Calculate the position from which to toss the projectile
        Vector3 tossPosition = tr.position + tr.forward.normalized;
        // Instantiate a new projectile at the calculated position with the current rotation
        GameObject flyingDanso = Instantiate(danso, tossPosition, tr.rotation);
    }
}
