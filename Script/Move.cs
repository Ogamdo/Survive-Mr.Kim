using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed; // Accessible in the inspector for QA adjustments
    Vector3 dir = Vector3.zero;

    private Rigidbody rb; 
    private Transform tr; 
    private Animator anim; 
    float hAxis; 
    float vAxis; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        dir = new Vector3(hAxis, 0.0f, vAxis).normalized;

        rb.AddForce(dir * speed * Time.fixedDeltaTime); // Adjust movement speed based on time
    }
}
