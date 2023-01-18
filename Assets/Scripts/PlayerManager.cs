using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // The speed at which the player moves
    public float speed = 5.0f;

    // Reference to the Player object
    private List<Player> players;
    private List<GameObject> playerGOs;

    //The current active player
    [SerializeField] private Player activePlayer;

    // Reference to the player prefab
    [SerializeField] private GameObject playerPrefab;

    public List<Player> Players { get => players; set => players = value; }
    public List<GameObject> PlayerGOs { get => playerGOs; set => playerGOs = value; }

    // Start
    public void Init(int amountOfPlayers, List<Vector2> spawnPoints)
    {
        Players = new List<Player>();
        PlayerGOs = new List<GameObject>();

        for (int i = 0; i < amountOfPlayers; i++)
        {
            GameObject playerGO = Instantiate(playerPrefab, spawnPoints[i], Quaternion.identity);
            playerGOs.Add(playerGO);

            Player player = playerGO.GetComponent<Player>();
            players.Add(player);

            Players[i] = PlayerGOs[i].GetComponent<Player>();
            Players[i].Rb = PlayerGOs[i].GetComponent<Rigidbody2D>();
            Players[i].Speed = speed;
        }
        activePlayer = Players[0];
    }
}