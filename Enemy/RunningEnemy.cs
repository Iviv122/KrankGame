using UnityEngine;
using UnityEngine.AI;

public class RunningEnemy : Enemy 
{
    [SerializeField] float Speed;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] PlayerTransform Player;
    [SerializeField] float UpdateSpeed;
    protected Coroutine FollowCoroutine;
    private CountdownTimer timer;
    private void Awake()
    {
        timer = new(UpdateSpeed);
        timer.OnTimerStop += () =>
        {
            timer.Reset();
            timer.Start();

            Agent.SetDestination(Player.transform.position);
        };

        timer.Start();

        Agent.updatePosition = false;
    }

    private void FixedUpdate() {
        timer.Tick(Time.deltaTime);

        if (Agent.path != null)
        {
            transform.position += (Agent.path.corners[1] - transform.position).normalized * Speed;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<Player>()?.Die();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.gameObject.GetComponentInParent<Player>()?.Die();
        }
    }
}