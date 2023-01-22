using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    [SerializeField] private Player activePlayer;
    [SerializeField] private CameraController activeCamera;

    public Player ActivePlayer { get => activePlayer; set => activePlayer = value; }
    public CameraController ActiveCamera { get => activeCamera; set => activeCamera = value; }

    public void Init(Player player, CameraController cam)
    {
        ActivePlayer = player; 
        ActiveCamera = cam;
        activeCamera.Target = player.transform;
    }

    public void UpdateClient()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        ActivePlayer.Move(horizontalInput, verticalInput);
    }

    public void LateUpdateClient()
    {
        activeCamera.Follow();
    }
}
