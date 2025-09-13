using TMPro;
using UnityEngine;

public class FPSMeter : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI t;
    void Update()
    {
        t.text = (1f / Time.deltaTime).ToString();   
    }
}
