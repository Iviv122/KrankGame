using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] LevelRestart restart;

    private void Awake()
    {
        restart.Restarted += Reset; 
        Spawn();
    }
    void Reset()
    {
        Spawn();
    }
    void OnDestroy()
    {
        restart.Restarted -= Reset;
    }
    void Spawn() {
        Instantiate(Player,transform.position,Quaternion.identity,null);
    }
}
