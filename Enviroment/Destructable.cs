using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] GameObject DestroyEffect;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Throwed>(out Throwed item))
        {
            if (DestroyEffect != null)
            {
                Instantiate(DestroyEffect, transform.position, Quaternion.identity, null);
            }
            Destroy(gameObject);
        }
    }
}
