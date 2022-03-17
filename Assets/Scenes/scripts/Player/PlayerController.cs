using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    float speedX, speedY;
    Rigidbody2D rb;
    public Transform groundcheck;
    public bool isGrounded = true;
    public float groundCheckRadius;
    public LayerMask GroundLayer;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.localScale = new Vector3(1, 1, 1);
    }


    // Update is called once per frame
    void Update()
    {
        Attack();
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, GroundLayer);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }
        FlipPlayer();

    }


    private void FixedUpdate()
    {
        Movement();
        JumpPlayer();
    }

    public void Movement()
    {
        speedX = Input.GetAxisRaw("Horizontal");
        speedY = rb.velocity.y;
        rb.velocity = new Vector2(speedX * speed, speedY);

        if (rb.velocity.x != 0)
        {
            anim.SetBool("Run", true);
        } else {

            anim.SetBool("Run", false);
        }
    }

    public void JumpPlayer()
    {
        if (Input.GetButton("Jump")  && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }


    public void FlipPlayer()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Attack()
    {
        if (Input.GetButton("Fire1"))
        {
            anim.SetBool("Attack", true);
        } else
        {
            anim.SetBool("Attack", false);
        }
    }
}
