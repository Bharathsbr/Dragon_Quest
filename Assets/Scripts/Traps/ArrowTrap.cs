using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField]private float attackCoolDown;
    [SerializeField]private Transform firepoint;
    [SerializeField]private GameObject[] arrows;
    private float coolDownTimer;
    [SerializeField]private AudioClip at;

    private void Attack()
    {
        coolDownTimer=0;
        SoundManager.instance.PlaySound(at);
        arrows[FindArrow()].transform.position=firepoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrow(){
        for(int i=0;i<arrows.Length;i++){
            if(!arrows[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private void Update(){
        coolDownTimer+=Time.deltaTime;
        if(coolDownTimer>=attackCoolDown){
            Attack();
        }
    }
}
