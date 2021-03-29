using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharecterControl : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private float Horizontal;
    private float z;

    [Header("checkGorund")]
    public Vector3 checkGorundPoint;
    public float radius;
    public LayerMask groundLayer;

    [Header("checkWall")]
    public Vector3 checkWalldPoint;
    public float whWeight;
    public float whHeight;
    public LayerMask wallLayer;

    private bool isJump = false;

    private int faceDirection = 1;

    public GameObject normalBullet;

    public Animator animator;

    public SpriteRenderer spriteRenderer;


    public float speedX = 800f;
    public float speedY = 100f;

    public float jumpTime = 0.35f;
    private float jumpTimeCounter;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + checkGorundPoint, radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + checkWalldPoint,new Vector3(whWeight,whHeight,1));
    }

    void FixedUpdate()
    {

        Horizontal = Input.GetAxis("Horizontal");
        
        //z = Input.GetAxis("Vertical");

        var IsOnGround = Physics2D.OverlapCircle(transform.position + checkGorundPoint, radius, groundLayer);

        float move = Horizontal * speedX * Time.deltaTime;
        if (IsOnWall()&& !IsOnGround) move = 0;
        if (Horizontal > 0)
        {
            faceDirection = 1;
            spriteRenderer.flipX = false;

        }
        else if (Horizontal <0)
        {
            faceDirection = -1;
            spriteRenderer.flipX = true;
        }

        //if (Input.GetKeyUp(KeyCode.C))
        //{
        //    fireBullet();
        //}

        if (IsOnGround && rigidbody.velocity.y == 0.0f && Input.GetAxisRaw("Jump") == 1)
        {
            isJump = true;
            rigidbody.AddForce(Vector2.up * speedY, ForceMode2D.Impulse);
            jumpTimeCounter = jumpTime;

        }

        if(Input.GetAxisRaw("Jump") == 1 && isJump)
        {
            if(jumpTimeCounter>0)
            {
                rigidbody.AddForce(Vector2.up * speedY, ForceMode2D.Impulse);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJump = false;

            }
        }

        if(Input.GetAxisRaw("Jump") == 0)
        {
            isJump = false;
        }
        rigidbody.velocity = new Vector2(move, rigidbody.velocity.y);
        
        updateAnimatorValue();
        //Debug.Log(Horizontal);
    }


    bool IsOnWall()
    {
        var ground = Physics2D.OverlapBox(transform.position + checkWalldPoint, new Vector2(whWeight, whHeight),0,wallLayer);

        return ground;
    }


    private void updateAnimatorValue()
    {

            animator.SetFloat("xVelocity", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }
    //public void fireBullet()
    //{
    //    var bulletOJ = Instantiate(normalBullet, transform.position, Quaternion.identity);
    //    var bullet = bulletOJ.GetComponent<DirectMove>();
    //    bullet.direction = new Vector2(faceDirection, 0);

    //}

}
