using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 5f;  // Speed at which the enemy flies

    public float health = 10f;  // Enemy health

    private Transform player;  // Reference to the player
    private Animator animator;  // Reference to the Animator component
    private bool isDead = false;

    void Start()
    {
        // Find the player GameObject
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;  // If the enemy is dead, stop all actions

        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            // Trigger take hit animation
            animator.SetTrigger("Hit");
        }
    }

    void Die()
    {
        isDead = true;
        // Trigger death animation
        animator.SetTrigger("Death");
        // Destroy the enemy after the death animation finishes
        Destroy(gameObject, 1f);  // Adjust the delay to match the death animation length
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player
            collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(2f, transform.position);
        }
    }
}
