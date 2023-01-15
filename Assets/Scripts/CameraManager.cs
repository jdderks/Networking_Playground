using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // The target the camera should follow
    public List<Transform> targets;

    [SerializeField] private GameObject cameraPrefab;

    // Reference to the CameraController object
    private List<CameraController> cameraControllers;

    [SerializeField] private List<GameObject> cameraObjects = new List<GameObject>();

    public List<CameraController> CameraControllers { get => cameraControllers; set => cameraControllers = value; }
    public List<GameObject> CameraObjects { get => cameraObjects; set => cameraObjects = value; }

    // Awake is called when the script instance is being loaded
    public void Init(int amountOfPlayers)
    {
        CameraObjects = new List<GameObject>();
        CameraControllers = new List<CameraController>();

        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameObject camObject = Instantiate(cameraPrefab);
            camObject.name = "Camera " + i;
            CameraObjects.Add(camObject);

            CameraController camController = camObject.GetComponent<CameraController>();
            CameraControllers.Add(camController);
        }
    }

    public void SetTargets(List<GameObject> targets)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            cameraControllers[i].Target = targets[i].transform;
        }
        
    }
}