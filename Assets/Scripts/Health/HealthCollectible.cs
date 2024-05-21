using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private AudioClip hc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player"){
            SoundManager.instance.PlaySound(hc);
            collision.GetComponent<Health>().AddHealth(value);
            gameObject.SetActive(false);
        }
    }
}
