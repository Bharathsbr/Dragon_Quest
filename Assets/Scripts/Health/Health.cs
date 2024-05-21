using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    [SerializeField]private Animator anim;
    private bool dead;

    [Header ("IFrames")]
    [SerializeField] private float induration;
    [SerializeField] private int times;
    private SpriteRenderer sr;
    private bool invul;

    [SerializeField] private AudioClip die;
    [SerializeField] private AudioClip hurt;



    private void Awake()
    {
        anim=GetComponent<Animator>();
        sr=GetComponent<SpriteRenderer>();
        currentHealth=startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        if(invul){
            return;
        }
        currentHealth=Mathf.Clamp(currentHealth-_damage,0,startingHealth);

        if(currentHealth>0){
            anim.SetTrigger("hurt");
            StartCoroutine(InvulnerAbility());
            SoundManager.instance.PlaySound(hurt);
        }
        else{
            if(!dead){
               
                if(GetComponent<PlayerMovement>()!=null){
                    GetComponent<PlayerMovement>().enabled=false;
                }

                if(GetComponentInParent<EnemyPatrol>()!=null){
                    GetComponentInParent<EnemyPatrol>().enabled=false;
                }

                if(GetComponent<MeleeKnight>()!=null){
                     GetComponent<MeleeKnight>().enabled=false;
                }
                anim.SetBool("grounded",true);
                anim.SetTrigger("die");
                dead=true;
                SoundManager.instance.PlaySound(die);
            }
        }
    }

    public void AddHealth(float value){
        currentHealth=Mathf.Clamp(currentHealth+value,0,startingHealth);
    }

    public void Respawn()
    {
        dead=false;
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(InvulnerAbility());

        if(GetComponent<PlayerMovement>()!=null){
                    GetComponent<PlayerMovement>().enabled=true;
                }

                if(GetComponentInParent<EnemyPatrol>()!=null){
                    GetComponentInParent<EnemyPatrol>().enabled=true;
                }

                if(GetComponent<MeleeKnight>()!=null){
                     GetComponent<MeleeKnight>().enabled=true;
                }
    }

   private IEnumerator InvulnerAbility()
   {
        invul=true;
        Physics2D.IgnoreLayerCollision(9,10,true);
        for(int i=0;i<times;i++){
            sr.color=new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(induration/(times*2));
            sr.color=Color.white;
            yield return new WaitForSeconds(induration/(times*2));
        }
        Physics2D.IgnoreLayerCollision(9,10,false);
        invul=false;
   }

   private void Deactivate()
   {
    gameObject.SetActive(false);
   }
}
