using UnityEngine;

public class EnemyRemover : MonoBehaviour
{
    [SerializeField] LevelRestart restart;
    void Awake()
    {
        if (restart != null)
        {
            restart.Cleaning += Die;
            restart.Restarted += Die;
        }
    }
    void OnDestroy()
    {
        restart.Cleaning -= Die;
        restart.Restarted -= Die;
    }
    void Die()
    {
        restart.Cleaning -= Die;
        restart.Restarted -= Die;
        Destroy(gameObject);
    }
}
