using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float timeAfter;
    [SerializeField] private float timeItGoes;

    private Animator anim;
    private SpriteRenderer sr;

    private bool triggered;
    private bool active;
    private Health ph;

    [SerializeField] private AudioClip ft;

    private void Awake()
    {
        anim=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(ph!=null && active){
            ph.TakeDamage(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player"){
            ph=collision.GetComponent<Health>();
        }
        if(!triggered){
            StartCoroutine(ActivateFireTRap());
        }
        if(active){
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

     private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player"){
            ph=null;
        }
    }

    private IEnumerator ActivateFireTRap(){
        triggered=true;
        sr.color=Color.red;
        yield return new WaitForSeconds(timeAfter);
        SoundManager.instance.PlaySound(ft);
        sr.color=Color.white;
        active=true;
        anim.SetBool("activated",true);
        yield return new WaitForSeconds(timeItGoes);
        active=false;
        triggered=false;
        anim.SetBool("activated",false); 
    }
}
