using UnityEngine;

public class SettingApllier : MonoBehaviour
{
    [SerializeField] Settings settings;
    private void Awake()
    {
        settings.GlobalSettings();
        settings.Updated += () => { settings.GlobalSettings(); };
    }
}
