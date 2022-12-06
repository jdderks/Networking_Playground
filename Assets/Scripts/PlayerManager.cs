using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // The speed at which the player moves
    public float speed = 5.0f;

    // Reference to the Player object
    private Player player;

    // Reference to the player prefab
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerGO;

    public GameObject PlayerGO { get => playerGO; set => playerGO = value; }

    // Start
    public void Init()
    {
        PlayerGO = Instantiate(playerPrefab);
        player = PlayerGO.GetComponent<Player>();
        player.Rb = playerGO.GetComponent<Rigidbody2D>();
        player.Speed = speed;
    }

    // Update is called once per frame
    public void UpdatePlayer()
    {
        // Get input from the user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the player in the specified direction
        player.Move(horizontalInput, verticalInput);
    }
}