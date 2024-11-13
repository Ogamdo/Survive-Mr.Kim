using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoundaryKeeper : MonoBehaviour
{
    public float rotation = 120.0f;
    void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(0, rotation, 0);
    }
}