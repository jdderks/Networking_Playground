using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The target the camera should follow
    private Transform target;

    // The speed at which the camera should follow the target
    private float followSpeed = 5.0f;

    public Transform Target { get => target; set => target = value; }

    // Move the camera to follow the target
    public void Follow()
    {
        // Calculate the position the camera should be in
        Vector3 targetPosition = Target.position;
        targetPosition.z = transform.position.z;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}