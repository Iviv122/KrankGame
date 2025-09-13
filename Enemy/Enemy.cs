using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject);
    }
}
