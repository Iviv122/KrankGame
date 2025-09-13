using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    [SerializeField] ItemPickUp pickUp;
    [SerializeField] Texture crosshair;
    [SerializeField] Texture hand;
    [SerializeField] RawImage img;
    void Awake()
    {
        pickUp.CanPickUp += SpriteHand;
        pickUp.CantPickUp += SpriteCross; 
    }
    void SpriteCross()
    {
        img.texture = crosshair;    
    }
    void SpriteHand() {
        img.texture = hand;     
    } 
}
