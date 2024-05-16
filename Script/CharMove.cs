using UnityEngine;

public class CharMove : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

    Animator anim;
    GameTimer gameTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();
    }

    void FixedUpdate()
    {
        if (gameTimer != null && !gameTimer.IsGameActive())
        {
            anim.SetBool("Walk", false);
            return;  // 게임이 활성 상태가 아니면 움직임을 멈춥니다.
        }

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
        anim.SetBool("Walk", moveVec != Vector3.zero);

        if (moveVec != Vector3.zero)
        {
            transform.LookAt(transform.position + moveVec);
        }
    }
}
