using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedKnight : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer=Mathf.Infinity;

    [SerializeField] private BoxCollider2D bc;
    private Animator anim;
    private EnemyPatrol ep;
    
    [SerializeField]private Transform firepoint;
    [SerializeField]private GameObject[] fireballs;
    private void Awake()
    {
        anim=GetComponent<Animator>();
        ep=GetComponentInParent<EnemyPatrol>();
    }

     private void Update()
    {
        coolDownTimer+=Time.deltaTime;

        if(PlayerInSight()){
            if(coolDownTimer>=attackCoolDown){
                coolDownTimer=0;
                anim.SetTrigger("rangedAttack");
            }
        }
        if(ep!=null){
            ep.enabled=!PlayerInSight();
        }
    }

    private void RangedAttack(){
        coolDownTimer=0;
        fireballs[FindFireBall()].transform.position=firepoint.position;
        fireballs[FindFireBall()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireBall(){
        for(int i=0;i<fireballs.Length;i++){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit=Physics2D.BoxCast(bc.bounds.center+transform.right*range*
        transform.localScale.x*colliderDistance,new Vector3(bc.bounds.size.x*range,bc.bounds.size.y,bc.bounds.size.z),0,Vector2.left,0,playerLayer);
        return hit.collider!=null;
    }

    private void OnDrawGizmos(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(bc.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,new Vector3(bc.bounds.size.x*range,bc.bounds.size.y,bc.bounds.size.z));
    }
}
