using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Parameters")]

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(0f,2.25f, -1.5f);

    [SerializeField, Range(0.01f,1f)]
    private float smoothSpeed = 0.125f;

    //For smooth Damp function
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }


}
