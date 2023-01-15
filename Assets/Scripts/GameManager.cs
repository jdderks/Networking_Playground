using UnityEngine;

public class GameManager : MonoBehaviour
{

    int amountofplayers = 1; //TODO: to handle this with networkmanager

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

        // initialize the grid generator
        gridGenerator.Init();

        // initialize the player manager
        playerManager.Init();



        //Set camera follow target to player
        cameraManager.SetTarget(playerManager.PlayerGOs[0].transform);
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
