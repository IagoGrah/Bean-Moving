using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Campbean : MonoBehaviour
{
    public int maxHealth = 150;
    int currHealth;

    public float moveSpeed = 5f;
    public float maxSpeed = 5f;

    public float bulletSpeed = 5f;
    public float fireRate = 0.1f;
    float fireRateCounter;

    public GameObject bullet;
    public Image hpBar;

    public GameObject family;
    public AudioClip dmgSound;
    public AudioClip shotSound;

    AudioSource audioSource;
    Transform shootingPoint;
    GameObject player;
    Rigidbody2D rb;

    bool dead;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        shootingPoint = transform.Find("ShootingPoint");
        currHealth = maxHealth;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 200f);
    }

    void Update()
    {
        if (!dead)
        {
            fireRateCounter += Time.deltaTime;

            Vector2 playerPos = player.transform.position;
            float AngleRad = Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x);
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg-188);

            // rb.AddForce(Vector2.ClampMagnitude(-(playerPos - (Vector2)transform.position), 1f) * moveSpeed);
            rb.AddForce(Vector2.ClampMagnitude(new Vector2(Random.Range(-100f,100f), Random.Range(-100f,100f)), 1f) * moveSpeed);

            if(rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }

            if (fireRateCounter > fireRate)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shotSound);

        fireRateCounter = 0f;
        
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)shootingPoint.position).normalized;

        var go = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Destroy(go, 4);
    }

    public void DealDamage()
    {
        audioSource.PlayOneShot(dmgSound);
        currHealth--;
        hpBar.fillAmount = (float)currHealth/(float)maxHealth;
        if (currHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        dead = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        var bullets = GameObject.FindObjectsOfType<EnemyBullet>();
        foreach (var b in bullets)
        {
            Destroy(b.gameObject);
        }

        family.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150f);

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GG");
    }
}
