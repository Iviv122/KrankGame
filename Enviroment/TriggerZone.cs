using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] EnviromentEvent[] events;
    [SerializeField] LevelRestart restart;
    private bool IsWorking = true;
    private void Awake() {
        restart.Restarted += () =>
        {
            IsWorking = true;
        };
    }
    private void Triggered()
    {
        if (IsWorking)
        {
            foreach (EnviromentEvent i in events)
            {
                i.Trigger();
            }
            IsWorking = false;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        Player p = other.gameObject.GetComponentInParent<Player>();
        if (p != null)
        {
            Triggered();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponentInParent<Player>();
        if (p != null)
        {
            Triggered();
        }
    }
}
