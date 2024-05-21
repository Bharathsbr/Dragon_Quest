using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header ("Enemy attributes")]
    [SerializeField] private float speed;
    private int direction;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private Animator anim;
    
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private void Awake(){
        initScale=enemy.localScale;
    }

    private void Update(){
        if(movingLeft){
            if(enemy.position.x>=leftEdge.position.x){
                MoveInDirection(-1);
            }else{
                ChangeDirection();
            }
        }else{
            if(enemy.position.x<=rightEdge.position.x){
                MoveInDirection(1);
            }else{
                ChangeDirection();
            }
        }
    }

    private void ChangeDirection()
    {
        anim.SetBool("moving",false);

        idleTimer+=Time.deltaTime;
        if(idleTimer>idleDuration){
            movingLeft=!movingLeft;
        }

    }

    private void OnDisable(){
        anim.SetBool("moving",false);
    }
    
    private void MoveInDirection(int direction)
    {
        idleTimer=0;
        anim.SetBool("moving",true);
        enemy.localScale=new Vector3(Mathf.Abs(initScale.x)*direction,initScale.y,initScale.z);
        enemy.position=new Vector3(enemy.position.x+Time.deltaTime*speed*direction,enemy.position.y,enemy.position.z);
    }
}
