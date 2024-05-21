using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float Timer;
    [SerializeField] private LayerMask playerMask;

    private float attackTimer;
    private Vector3 destination;
    private bool attacking;

    [SerializeField] private AudioClip impact;

    private Vector3[] directions=new Vector3[4];

    private void OnEnable(){
        Stop();
    }

    private void Update(){
        if(attacking){
            transform.Translate(destination*Time.deltaTime*speed);
        }else{
            
            attackTimer+=Time.deltaTime;

            if(attackTimer>Timer){
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        FindDirections();
        for(int i=0;i<directions.Length;i++){
            Debug.DrawRay(transform.position,directions[i],Color.red);
            RaycastHit2D hit=Physics2D.Raycast(transform.position,directions[i],range,playerMask);

            if(hit.collider!=null && !attacking){
                attacking=true;
                destination=directions[i];
                attackTimer=0;
            }
        }
    }

    private void FindDirections()
    {
        directions[0]=transform.right*range;
        directions[1]=-transform.right*range;
        directions[2]=transform.up*range;
        directions[3]=-transform.up*range;
    }

    private void Stop(){
        destination=transform.position;
        attacking=false;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        SoundManager.instance.PlaySound(impact);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
