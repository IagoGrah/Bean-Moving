using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            coll.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity);
            coll.GetComponent<Enemy>().DealDamage();
            Destroy(gameObject);
        }
        else if (coll.CompareTag("Campbean"))
        {
            coll.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity);
            coll.GetComponent<Campbean>().DealDamage();
            Destroy(gameObject);
        }
    }
}
