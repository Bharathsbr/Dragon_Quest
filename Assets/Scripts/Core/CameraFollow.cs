using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private float speed;
    private float currentPosx;
    private Vector3 velocity=Vector3.zero;
    [SerializeField] private Transform player;
    [SerializeField]private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookahead;

    private void Update()
    {
        //transform.position=Vector3.SmoothDamp(transform.position,new Vector3(currentPosx,transform.position.y,transform.position.z),ref velocity,speed*Time.deltaTime);
        transform.position=new Vector3(player.position.x+lookahead,transform.position.y,transform.position.z);
        lookahead=Mathf.Lerp(lookahead,(aheadDistance*player.localScale.x),(cameraSpeed*Time.deltaTime));
    }

    
}
