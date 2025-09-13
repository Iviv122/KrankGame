using UnityEngine;

public class Turret : Enemy 
{
    [SerializeField] PlayerTransform Player;
    [SerializeField] float Radius;
    [SerializeField] float AttackDelay;
    [SerializeField] GameObject bullet;
    [SerializeField] Vector3 offset;
    private CountdownTimer timer;
    void Awake()
    {
        timer = new(AttackDelay);
        timer.OnTimerStop += () =>
        {
            timer.Start();
            Shoot();
        };
        timer.Start();
    }
    void Update()
    {
        transform.LookAt(Player.transform);
        timer.Tick(Time.deltaTime);
    }
    void Shoot()
    {
        Instantiate(bullet,transform.position,transform.rotation,null);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.DrawWireSphere(transform.position + offset, 0.1f);
    }
}
