using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D bc;
    private float horizontalInput;

    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;

    [SerializeField]private float speed=10f;
    [SerializeField]private float jumpPower=20f;
    private float wallJumpCoolDown;

    [SerializeField]private AudioClip jump;

    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [SerializeField]private int wallJumpX;
    [SerializeField]private int wallJumpY;

    private void Awake()
    {
        body=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        bc=GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        horizontalInput=Input.GetAxis("Horizontal");
        

        if(horizontalInput>0.01f){
            transform.localScale=new Vector3(2,2,2);
        }

        else if(horizontalInput<-0.01f){
            transform.localScale=new Vector3(-2,2,2);
        }

        

        anim.SetBool("run",horizontalInput!=0);
        anim.SetBool("grounded",isGrounded());
        
       if(Input.GetKeyDown(KeyCode.Space)){
        Jump();
       }
       if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y>0){
        body.velocity=new Vector2(body.velocity.x,body.velocity.y/2);
       }
       if(onWall()){
        body.gravityScale=0;
        body.velocity=Vector2.zero;
       }else{
        body.gravityScale=7;
        body.velocity=new Vector2(horizontalInput*speed,body.velocity.y);

        if(isGrounded()){
            coyoteCounter=coyoteTime;
            jumpCounter=extraJumps;
        }else{
            coyoteCounter-=Time.deltaTime;
        }
       }
    }

    private void Jump()
    {
        if(coyoteCounter<=0 && !onWall() && jumpCounter<=0){return;}
        SoundManager.instance.PlaySound(jump);
       if(onWall()){
        WallJump();
       }else{
        if(isGrounded()){
            body.velocity=new Vector2(body.velocity.x,jumpPower);
        }else{
            if(coyoteCounter>0){
                body.velocity=new Vector2(body.velocity.x,jumpPower);
            }else{
                if(jumpCounter>0){
                    body.velocity=new Vector2(body.velocity.x,jumpPower);
                    jumpCounter--;
                }
            }
        }
        coyoteCounter=0;
       }
    }

    private void WallJump(){
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*wallJumpX,wallJumpY));
        wallJumpCoolDown=0;
    }

    private bool isGrounded()
    {
        RaycastHit2D rc=Physics2D.BoxCast(bc.bounds.center,bc.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return rc.collider!=null;
    }

    private bool onWall()
    {
        RaycastHit2D rc=Physics2D.BoxCast(bc.bounds.center,bc.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f,wallLayer);
        return rc.collider!=null;
    }

    public bool canAttack()
    {
        return horizontalInput==0 && isGrounded() && !onWall();
    }

    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Endpoint")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
