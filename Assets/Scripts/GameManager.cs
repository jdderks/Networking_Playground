using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int amountofplayers = 1;
    [SerializeField] private int activePlayer = 0;
    // reference to the camera manager
    public CameraManager cameraManager;

    // reference to the player manager
    public PlayerManager playerManager;

    // reference to the gridGenerator
    public GridGenerator gridGenerator;

    private NetworkClient networkClient;

    // Use this for initialization
    void Start()
    {

        // initialize the grid generator
        gridGenerator.Init();
        gridGenerator.SetSpawnPoints(amountofplayers);
        // initialize the player manager
        playerManager.Init(amountofplayers, gridGenerator.SpawnPoints);

        //Set camera follow target to player
        cameraManager.Init(amountofplayers);

        SetNetworkClient(0);

        cameraManager.SetTargets(playerManager.PlayerGOs);
    }

    public void SetNetworkClient(int currentIndex)
    {
        networkClient = gameObject.AddComponent<NetworkClient>();
        
        for (int i = 0; i < amountofplayers; i++)
        {
            if (i != currentIndex)
            {
                cameraManager.CameraObjects[i].SetActive(false);
            } 
            else
            {
                networkClient.ActivePlayer = playerManager.Players[i];
                networkClient.ActiveCamera = cameraManager.CameraControllers[i];
                networkClient.ActiveCamera.gameObject.SetActive(true);
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        networkClient.UpdateClient();
    }

    //LateUpdate for camera behaviours
    private void LateUpdate()
    {
        networkClient.LateUpdateClient();
    }
}
