using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // The speed at which the player moves
    public float speed = 5.0f;

    //Amount of players
    private int amountOfPlayers = 1;
    
    // Reference to the Player object
    private List<Player> players;
    private List<GameObject> playerGOs;

    // Reference to the player prefab
    [SerializeField] private GameObject playerPrefab;

    public List<GameObject> PlayerGOs { get => playerGOs; set => playerGOs = value; }
    public int AmountOfPlayers { get => amountOfPlayers; set => amountOfPlayers = value; }

    // Start
    public void Init()
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            PlayerGOs[i] = Instantiate(playerPrefab);
            players[i] = PlayerGOs[i].GetComponent<Player>();
            players[i].Rb = playerGOs[i].GetComponent<Rigidbody2D>();
            players[i].Speed = speed;
        }
        
    }

    // Update is called once per frame
    public void UpdatePlayer()
    {
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the player in the specified direction
        players[0].Move(horizontalInput, verticalInput);
    }
}