using UnityEngine;

public class GameManager : MonoBehaviour
{
    // reference to the camera manager
    public CameraManager cameraManager;

    // reference to the player manager
    public PlayerManager playerManager;

    // reference to the gridGenerator
    public GridGenerator gridGenerator;

    // Use this for initialization
    void Start()
    {
        // initialize the camera manager
        cameraManager.Init();

        // initialize the player manager
        playerManager.Init();

        // initialize the grid generator
        gridGenerator.Init();

        //Set camera follow target to player
        cameraManager.SetTarget(playerManager.PlayerGO.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // update the player manager
        playerManager.UpdatePlayer();
    }

    private void FixedUpdate()
    {
        // update the camera manager
        cameraManager.UpdateCamera();
    }
}
