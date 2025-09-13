using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] PlayerTransform t;
    [SerializeField] AudioSource s;
    [SerializeField] Settings settings;
    public static Music Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        settings.Updated += ChangeVolume;
        ChangeVolume();
    }
    void ChangeVolume()
    {
        settings.Load();
        s.volume = settings.Volume;
    }
    void Update()
    {
        if (t.transform != null)
        {
            transform.position = t.transform.position;
        }
    }
}
