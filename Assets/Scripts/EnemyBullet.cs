using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            coll.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity);
            coll.GetComponent<PlayerController>().DealDamage();
            Destroy(gameObject);
        }
    }
}
