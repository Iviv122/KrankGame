using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float JumpPower;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody f))
        {
            f.linearVelocity += Vector3.up * JumpPower; 
        }
    }
}
