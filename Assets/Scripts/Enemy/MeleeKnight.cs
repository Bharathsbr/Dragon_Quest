using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeKnight : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer=Mathf.Infinity;

    [SerializeField] private BoxCollider2D bc;
    private Animator anim;
    private Health ph;
    private EnemyPatrol ep;

    [SerializeField] private AudioClip melee;

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
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(melee);
            }
        }
        if(ep!=null){
            ep.enabled=!PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit=Physics2D.BoxCast(bc.bounds.center+transform.right*range*
        transform.localScale.x*colliderDistance,new Vector3(bc.bounds.size.x*range,bc.bounds.size.y,bc.bounds.size.z),0,Vector2.left,0,playerLayer);
        if(hit.collider!=null){
            ph=hit.transform.GetComponent<Health>();
        }
        return hit.collider!=null;
    }

    private void OnDrawGizmos(){
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(bc.bounds.center+transform.right*range*transform.localScale.x*colliderDistance,new Vector3(bc.bounds.size.x*range,bc.bounds.size.y,bc.bounds.size.z));
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            ph.TakeDamage(damage);
        }
    }

    private void Deactivate()
   {
    gameObject.SetActive(false);
   }
}
