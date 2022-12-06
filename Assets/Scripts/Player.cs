using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // The speed at which the player moves
    private float speed;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    public float Speed { get => speed; set => speed = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }

    // Move the player in the specified direction
    public void Move(float horizontalInput, float verticalInput)
    {
        // Calculate the direction the player should move in
        Vector2 direction = new Vector2(horizontalInput, verticalInput);

        // Normalize the direction vector
        direction = direction.normalized;

        // Move the player in that direction
        Rb.velocity = direction * Speed;
    }
}