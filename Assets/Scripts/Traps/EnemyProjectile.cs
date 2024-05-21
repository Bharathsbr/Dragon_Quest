using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float coolTime;

    public void ActivateProjectile()
    {
        coolTime=0;
        gameObject.SetActive(true);
    }

    private void Update(){
        float movespeed=speed*Time.deltaTime;
        transform.Translate(movespeed,0,0);

        coolTime+=Time.deltaTime;
        if(coolTime>resetTime){
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
