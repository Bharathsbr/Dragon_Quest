using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    [SerializeField] private AudioClip cp;
    [SerializeField] private Transform currentcp;
    private Health ph;
    [SerializeField]private Camera cam;
    private UIManager ui;

    private void Awake()
    {
        ph=GetComponent<Health>();
        ui=FindObjectOfType<UIManager>();
    }

    public void CheckRespawn(){
        if(currentcp==null){
            ui.GameOver();
            return;
        }
        transform.position=currentcp.position;
        ph.Respawn();
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag=="Checkpoint"){
            currentcp=collision.transform;
            SoundManager.instance.PlaySound(cp);
            collision.GetComponent<Collider2D>().enabled=false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
