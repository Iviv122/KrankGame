using UnityEngine;

public class Bullet : Enemy 
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float Speed;
    private void Awake()
    {
        Invoke(nameof(KullYourself), 5);
    }
    private void KullYourself() {
        Destroy(gameObject);   
    }
    private void Update() {
        rb.linearVelocity = transform.forward * Speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<Player>()?.Die();
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponentInParent<Player>()?.Die();
        }
        Destroy(gameObject);
    }
}
