using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // The target the camera should follow
    public Transform target;

    // Reference to the CameraController object
    private CameraController cameraController;

    [SerializeField] private GameObject cameraObject;

    // Awake is called when the script instance is being loaded
    public void Init()
    {
        // Create a new CameraController object and store it in the cameraController variable
        cameraController = cameraObject.GetComponent<CameraController>();
        //cameraController.Target;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        cameraController.Target = target;
    }

    // Update is called once per frame
    public void UpdateCamera()
    {
        // Move the camera to follow the target
        cameraController.Follow();
    }
}