using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField]private RectTransform[] options;
    [SerializeField]private AudioClip changeSound;
    [SerializeField]private AudioClip interact;
    private RectTransform rect;
    private int currentPosition;
    
    private void Awake()
    {
        rect=GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            ChangePosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            ChangePosition(1);
        }

        if(Input.GetKeyDown(KeyCode.E)){
            Interactt();
        }
    }
    public void ChangePosition(int change)
    {
        currentPosition+=change;
        if(change!=0){
            SoundManager.instance.PlaySound(changeSound);
        }

        if(currentPosition<0){
            currentPosition=options.Length-1;
        }
        else if(currentPosition>options.Length-1){
            currentPosition=0;
        }
        rect.position=new Vector3(rect.position.x,options[currentPosition].position.y,0);
    }

    public void Interactt(){
        SoundManager.instance.PlaySound(interact);
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
