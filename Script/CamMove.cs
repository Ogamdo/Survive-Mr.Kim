using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform playerTransform;

    public float distance = 4.0f;

    public float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
