using UnityEngine;

public class ThrowableCheckPoint : MonoBehaviour
{
    [SerializeField] LevelRestart restart;
    [SerializeField] GameObject ToSpawn;
    private void Awake()
    {
        GameObject n = new();
        ThrowableSpawn thro = n.AddComponent<ThrowableSpawn>();

        n.transform.position = transform.position;
        thro.Construct(ToSpawn, restart);

        Destroy(gameObject);

        thro.Spawn();
    }

}
