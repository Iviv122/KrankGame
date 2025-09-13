using System;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelRestart", menuName = "LevelRestart", order = 0)]
public class LevelRestart : ScriptableObject
{
    public event Action Restarted;
    public event Action Cleaning;
    [SerializeField] PlayerTransform t;
    public void Clear()
    {
        Destroy(t.transform.gameObject);
        Cleaning?.Invoke();
    }
    public void Reset()
    {
        Destroy(t.transform.gameObject);
        Cleaning?.Invoke();
        Restarted?.Invoke();
    }
}
