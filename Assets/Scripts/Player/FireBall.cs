using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField]private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;


    private  BoxCollider2D bc;
    private  Animator anim;

    private void Awake()
    {
        bc=GetComponent<BoxCollider2D>();
        anim=GetComponent<Animator>();
    }

    private void Update()
    {
        if(hit) return;
        float movementSpeed=speed*Time.deltaTime*direction;
        transform.Translate(movementSpeed,0,0);
        lifeTime+=Time.deltaTime;
        if(lifeTime>5){
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        hit=true;
        bc.enabled=false;
        anim.SetTrigger("Explode");
        if(collision.tag=="Enemy"){
            collision.GetComponent<Health>().TakeDamage(1); 
        }
    }
        
    

    public void SetDirection(float _direction)
    {
        lifeTime=0;
        direction=_direction;
        gameObject.SetActive(true);
        hit=false;
        bc.enabled=true;

        float localScaleX=transform.localScale.x;
        if(Mathf.Sign(localScaleX)!=_direction)
        {
            localScaleX=-localScaleX;
        }

        transform.localScale=new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
