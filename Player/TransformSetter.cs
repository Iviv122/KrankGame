using UnityEngine;

public class TransformSetter : MonoBehaviour
{
    [SerializeField] PlayerTransform P;
    private void Awake() {
        P.transform= gameObject.transform;
    }
}
