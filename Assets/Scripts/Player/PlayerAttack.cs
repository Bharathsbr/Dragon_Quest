using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]private float attackCoolDown;
    private float coolDownTime=Mathf.Infinity;
    [SerializeField]private Transform firePoint;
    [SerializeField]private GameObject[] fireballs;
    [SerializeField]private AudioClip fireBall;

    private Animator anim;
    private PlayerMovement pm;

    private void Awake()
    {
        anim=GetComponent<Animator>();
        pm=GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && coolDownTime>attackCoolDown && pm.canAttack()){
            Attack();
        }
        coolDownTime+=Time.deltaTime;
    }

    private void Attack()
    {
        SoundManager.instance.PlaySound(fireBall);
        anim.SetTrigger("attack");
        coolDownTime=0;

        fireballs[findFireBall()].transform.position=firePoint.position;
        fireballs[findFireBall()].GetComponent<FireBall>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findFireBall()
    {
        for(int i=0;i<fireballs.Length;i++){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private void Deactivate()
   {
    gameObject.SetActive(false);
   }
}
