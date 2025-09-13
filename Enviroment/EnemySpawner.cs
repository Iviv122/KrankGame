using UnityEngine;

public class EnemySpawner : EnviromentEvent
{
    [SerializeField] GameObject Enemy;
    [SerializeField] bool SpawnOnStart;
    [SerializeField] LevelRestart restart;
    private void Awake()
    {

        if (SpawnOnStart)
        {
            restart.Restarted += Spawn;
            Spawn();
        }
    }
    public override void Trigger()
    {
        Spawn();
    }
    void OnDestroy()
    {
        if (SpawnOnStart)
        {
            restart.Restarted -= Spawn;
        }
    }
    public void Spawn()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity, null);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1.4f);
    }
}
