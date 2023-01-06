using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 10;
    int currHealth;
    public float moveSpeed = 5f;
    public float maxSpeed = 5f;
    public float bulletSpeed = 5f;
    public float knockback = 40f;
    public float fireRate = 0.1f;
    float fireRateCounter;

    public GameObject bullet;
    public Image hpBar;
    public AudioClip shotSound;
    public AudioClip dmgSound;

    AudioSource audioSrc;
    Transform shootingPoint;
    Rigidbody2D rb;

    Vector2 mousePos;

    float inputX;
    float inputY;
    
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        shootingPoint = transform.Find("ShootingPoint");
        currHealth = maxHealth;
    }

    void Update()
    {
        fireRateCounter += Time.deltaTime;
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LookAtMouse();

        if (Input.GetKey(KeyCode.W))
        {
            inputY += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputY += -1; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputX += -1; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputX += 1;
        }
        Move();
        inputX = 0;
        inputY = 0;

        if (Input.GetMouseButton(0) && fireRateCounter > fireRate)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        fireRateCounter = 0f;

        audioSrc.PlayOneShot(shotSound);
        
        Vector2 direction = (mousePos - (Vector2)shootingPoint.position).normalized;
        Vector2 opposite = -direction;

        var go = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Destroy(go, 4);

        rb.AddForce(opposite * knockback);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            DealDamage();
        }
    }

    public void DealDamage()
    {
        audioSrc.PlayOneShot(dmgSound);

        currHealth--;
        hpBar.fillAmount = (float)currHealth/(float)maxHealth;
        if (currHealth <= 0)
        {
            FindObjectOfType<SceneController>().GameOver();
        }
    }
    
    void Move()
    {
        rb.AddForce(Vector2.ClampMagnitude(new Vector2(inputX, inputY), 1f) * moveSpeed);
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    void LookAtMouse()
    {
        float AngleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg+90);
    }
}
