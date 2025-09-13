using UnityEngine;

public class ThrowableSpawn : MonoBehaviour
{
    [SerializeField] GameObject spawn;
    [SerializeField] LevelRestart restart;
    public void Construct(GameObject spawn, LevelRestart r)
    {
        this.spawn = spawn;
        restart = r;

        restart.Restarted += Spawn;
    }
    public void Clean()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    void OnDestroy()
    {
        restart.Restarted -= Spawn;
    }
    public void Spawn()
    {
        Clean();
        Instantiate(spawn, transform.position, Quaternion.identity, transform);
    }
}
