using UnityEngine;

public class Shooter : Enemy 
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float AttackOffset;
    [SerializeField] float AttackDelay;
    [SerializeField] Vector3 offset;
    private CountdownTimer timer;
    private void Awake()
    {
        timer = new(AttackDelay+AttackOffset);
        timer.OnTimerStop += () =>
        {
            timer.Reset(AttackDelay);
            timer.Start();
            Shoot();
        };
        timer.Start();
    }
    private void Update()
    {
        timer.Tick(Time.deltaTime);
    }
    void Shoot()
    {
        Instantiate(Bullet,transform.position+transform.forward*2,transform.rotation,null);
    }
}
