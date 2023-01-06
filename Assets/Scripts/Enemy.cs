using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 10;
    int currHealth;

    public float moveSpeed = 5f;
    public float maxSpeed = 5f;

    public AudioClip dmgSound;

    AudioSource audioSource;
    GameObject player;
    Rigidbody2D rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currHealth = maxHealth;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 playerPos = player.transform.position;
        float AngleRad = Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg-150);

        rb.AddForce(Vector2.ClampMagnitude((playerPos - (Vector2)transform.position), 1f) * moveSpeed);
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    public void DealDamage()
    {
        audioSource.PlayOneShot(dmgSound);
        currHealth--;
        if (currHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
