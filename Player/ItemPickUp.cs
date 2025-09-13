using System;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float Length;
    [SerializeField] LayerMask InteractLayer;
    [SerializeField] ItemThrow itemThrow;
    public event Action CanPickUp;
    public event Action CantPickUp;
    public void PickUp()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Length, InteractLayer))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Hit object: " + hitObject.name);

            itemThrow.PutItem(hitObject);
            Destroy(hitObject);
        }
    }
    private void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, Length, InteractLayer))
        {
            Debug.Log("Can Pick Up");
            CanPickUp.Invoke();
        }
        else
        {
            CantPickUp.Invoke();
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(cam.transform.position, cam.transform.forward * Length);
    }
}