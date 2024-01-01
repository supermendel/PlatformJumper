using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float maxSpeed = 9f;
    private float minSpeed = 6f;
    private float speed;
    private float accelerationPerSec = 1f;
    private float slowing = 6f;
    private float jumpPower = 8f;
    private bool isFacingRight = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Sprite runSprite;
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D _collider;
   
    private float collisionRadius = 0.2f;
    [SerializeField]
    private Vector3 collisionOffset = Vector3.zero;

    private Vector2 velocity;
    private Collider2D groundCollider;
    private Vector3 groundOffset; 



    public bool isGameOn = false;

    public Rigidbody2D rb;

    public bool isAlive = true;
    public float DeltaY { get; private set; }
    public bool IsGrounded => groundCollider;


    private void Start()
    {
        speed = minSpeed;
    }
    void Update()
    {
        Alive();
        CheckInput();
        
        if (IsGrounded)
        {

            
            ApplyPositionOnGround();          
           CheckIsGrounded();

        }
        else
        {
            ApplyPosition();
            if (velocity.y < 0)
            {
                CheckIsGrounded();
            }
            
        }
        SpriteUpdate();
       Flip();
    }


    private void CheckIsGrounded()
    {

        groundCollider = Physics2D.OverlapBox(this.transform.position + (Vector3)_collider.offset, _collider.size, 0, groundLayer);

        Debug.Log(groundCollider);
    }

   


    private void ApplyPositionOnGround()
    {

        velocity.y = 0;



        // Calculate the offset between the player and the platform
       

        // Calculate the new position of the player


           
        Vector2 platformPosition = groundCollider.transform.position;

        
        Vector2 offset = (Vector2)transform.position - platformPosition;

       

        Vector2 newPos = platformPosition + new Vector2(offset.x, 1f);
        newPos += velocity * Time.deltaTime;
        rb.MovePosition(newPos);
    }
   


    private void ApplyPosition()
    {

        velocity += Vector2.down * Time.deltaTime* 30f ; // this is the gravity
        var newPos = rb.transform.position + ((Vector3)velocity * Time.deltaTime);
        if (newPos.y > 3)
        {
            DeltaY = newPos.y - 3;
            newPos.y = 3;
            isGameOn = true;
        }
        else
        {
            DeltaY = 0;
        }
        rb.MovePosition(newPos);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    private void SpriteUpdate()
    {
        if (IsGrounded && horizontal != 0f) sr.sprite = runSprite;
        if (!IsGrounded) sr.sprite = jumpSprite;
        if (IsGrounded && horizontal == 0f) sr.sprite = idleSprite;
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0f && speed < maxSpeed)  //increase speed
        {
            speed += accelerationPerSec * Time.deltaTime;
        }
        if (horizontal == 0f && speed > minSpeed)   // if stopped lower speed
        {
            speed -= slowing * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            groundCollider = null;
            velocity = new Vector2(velocity.x, jumpPower * speed / 3f);


        }
        velocity = new Vector2(horizontal * speed, velocity.y);
    }

    private void Alive()
    {
        if (this.transform.position.y < -3f)
        {
            isAlive = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position + collisionOffset, collisionRadius);
    }

    

}
