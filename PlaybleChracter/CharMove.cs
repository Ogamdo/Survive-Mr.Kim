using UnityEngine;

public class CharMove : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

    Animator anim;
    GameTimer gameTimer;

   

    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
    }

    void FixedUpdate()
    {
        

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
      

        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
