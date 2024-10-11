using UnityEngine;

public class CamMove : MonoBehaviour
{
   [SerializeField] Transform playerTransform;

    [SerializeField]float distance = 4.0f;
    [SerializeField]float height;
    [SerializeField] float smoothSpeed = 0.125f;

    void FixedUpdate()
    {
        CamMovement();
        
    }

    void CamMovement()
    {
        Vector3 desiredPosition = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
