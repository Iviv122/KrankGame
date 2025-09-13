using UnityEngine;
using UnityProgressBar;

public class ItemThrow : MonoBehaviour
{
    [SerializeField] float MinThrowForce;
    [SerializeField] float MaxThrowForce;
    [SerializeField] float ChargeSpeed;
    [SerializeField] ProgressBar bar;
    [SerializeField] GameObject item;
    [SerializeField] Camera ThrowPivot;
    [SerializeField] GameObject TrailObject;
    bool IsCharging = false;
    private float ThrowForce;
    private float ChargeProgress;
    public bool HasItem
    {
        get
        {
            return item != null;
        }
    }
    private void Awake()
    {
        bar.MinValue = MinThrowForce;
        bar.MaxValue = MaxThrowForce;

        ThrowForce = MinThrowForce;
        bar.Value = ThrowForce;
    }
    private void FixedUpdate()
    {
        if (IsCharging)
        {
            ThrowForce = Mathf.Lerp(MinThrowForce, MaxThrowForce, ChargeProgress);
            ChargeProgress = Mathf.Min(ChargeProgress + ChargeSpeed, 1);
            bar.Value = ThrowForce;
        }

        bar.gameObject.SetActive(IsCharging);
    }
    public void PutItem(GameObject item)
    {
        this.item = Instantiate(item, transform.position, Quaternion.identity, transform);
        this.item.GetComponent<Collider>().enabled = false;
        if (this.item.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            rb.interpolation = RigidbodyInterpolation.None;
        }
        bar.Value = ThrowForce;
    }
    public void Throw()
    {
        if (IsCharging)
        {
            ThrowItem();
        }
        else
        {
            IsCharging = true;
        }
    }
    private void ThrowItem()
    {
        if (item == null)
        {
            return;
        }

        GameObject throwen = Instantiate(item, ThrowPivot.transform.position + ThrowPivot.transform.forward, Quaternion.identity, null);
        Destroy(item);

        item = null;
        Rigidbody rb = throwen.GetComponent<Rigidbody>();

        throwen.GetComponent<Collider>().enabled = true;

        if (rb == null)
        {
            rb = throwen.AddComponent<Rigidbody>();
        }

        throwen.AddComponent<Throwed>();
        Instantiate(TrailObject, throwen.transform.position, Quaternion.identity, throwen.transform);

        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        rb.linearVelocity = ThrowPivot.transform.forward * ThrowForce;
        rb.angularVelocity = ThrowPivot.transform.right * ThrowForce;

        ThrowForce = MinThrowForce;
        ChargeProgress = 0;
        IsCharging = false;
        bar.Value = ThrowForce;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(ThrowPivot.transform.position + ThrowPivot.transform.forward, 0.05f);
    }
}