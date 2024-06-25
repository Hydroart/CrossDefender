using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    public float jumpForce = 10f; // Adjust this value to control the jump height
    public float jumpIntervalMin = 1f; // Minimum time between jumps
    public float jumpIntervalMax = 3f; // Maximum time between jumps

    private Rigidbody2D rb;
    private float nextJumpTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
    }

    private void Update()
    {
        if (Time.time >= nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + Random.Range(jumpIntervalMin, jumpIntervalMax);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}