using UnityEngine;

public class Throwed : MonoBehaviour
{
    private void Awake()
    {
        Invoke(nameof(KullYourself), 10);
    }
    private void KullYourself() {
        Destroy(gameObject);   
    }
    void OnDestroy()
    {
        foreach (Transform thing in transform)
        {
            Destroy(thing.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Enemy e = other.gameObject.GetComponent<Enemy>();
        if (e == null)
        {
            Destroy(gameObject);
        }
        else
        {
            e.Die();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Enemy e = collision.gameObject.GetComponent<Enemy>();
        if (e == null)
        {
            Destroy(gameObject);
        }
        else
        {
            e.Die();
        }
    }
}
