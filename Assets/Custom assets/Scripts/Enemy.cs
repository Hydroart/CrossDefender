using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float health = 10;
    public float speed = 5f;
    public bool isInvincible = false;
    private bool isHitted = false;
    private Rigidbody2D rb;
    private Transform player;
    private PlayerMovement playerMovement;

    public float goldDropChance;
    public int goldAmount;

    private bool eventTrigger = false;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();

        // Find the player object and get the PlayerMovement script
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
        }
    }
    
    void FixedUpdate () {
        if (health <= 0) {
            HandleDeath();
        }

        if (!isHitted && health > 0 && player != null)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer() {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;

        // Flip the enemy to face the player
        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            Flip();
        }
    }

    void HandleDeath()
    {
        MyEvent();
        // Start death animation and destruction process
        transform.GetComponent<Animator>().SetBool("IsDead", true);
        StartCoroutine(DestroyEnemy());
    }

    void Flip() {
        // Switch the way the player is labelled as facing.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ApplyDamage(float damage) {
        if (!isInvincible) 
        {
            float direction = damage / Mathf.Abs(damage);
            damage = Mathf.Abs(damage);
            transform.GetComponent<Animator>().SetBool("Hit", true);
            health -= damage;
            SoundManager.instance.PlaySFX("Hit");
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(direction * 500f, 100f));
            StartCoroutine(HitTime());
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && health > 0)
        {
            collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(2f, transform.position);
        }
    }

    IEnumerator HitTime()
    {
        isHitted = true;
        isInvincible = true;
        yield return new WaitForSeconds(0.1f);
        isHitted = false;
        isInvincible = false;
    }

    IEnumerator DestroyEnemy()
    {
        CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
        capsule.size = new Vector2(1f, 0.25f);
        capsule.offset = new Vector2(0f, -0.8f);
        capsule.direction = CapsuleDirection2D.Horizontal;
        yield return new WaitForSeconds(0.25f);
        rb.velocity = new Vector2(0, rb.velocity.y);

        Destroy(gameObject);
    }

    public void MyEvent()
    {
        if (!eventTrigger)
        {
            eventTrigger = true;
            playerMovement.AddKill();
            if (Random.Range(0f, 100f) <= goldDropChance)
            {
                playerMovement.AddGold(goldAmount);
            }
        }
    }
}
