using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The target the camera should follow
    private Transform target;

    // The speed at which the camera should follow the target
    [SerializeField] private float followSpeed = 0f; //BUG: JITTERY MOVEMENT WHEN HIGHER THAN 0

    private Vector3 velocity = new Vector3();

    public Transform Target { get => target; set => target = value; }

    // Move the camera to follow the target
    public void Follow()
    {
        // Calculate the position the camera should be in
        Vector3 targetPosition = Target.position;
        targetPosition.z = transform.position.z;

        // Smoothly move the camera towards the target position
        //transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);
    }
}