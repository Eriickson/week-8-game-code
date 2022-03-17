using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoviments : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    Animator anim;

    public bool isStatic;
    public bool isWalker;
    public bool isPatrol;
    public bool walksRight;
    public bool shouldWait;
    public float timeToWait;
    public bool isWaiting;


    public Transform pitCheck, walkCheck, groundCheck;

    public bool walkDetected, pitDetected, isGrounded;
    public float detationRadius;
    public LayerMask whatIsGround;


    public Transform pointA, pointB;
    private bool goToA, goToB;


    // Start is called before the first frame update
    void Start()
    {
        goToA = true;
        speed = GetComponent<EnemyScript>().Speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pitDetected = !Physics2D.OverlapCircle(pitCheck.position, detationRadius, whatIsGround);
        walkDetected = Physics2D.OverlapCircle(walkCheck.position, detationRadius, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, detationRadius, whatIsGround);


        if ((pitDetected || walkDetected) && isGrounded)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if(isStatic)
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if(isWalker)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetBool("Idle", false);
            if (!walksRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
        }

        if (isPatrol)
        {
            if (goToA)
            {
                if(!isWaiting)
                {
                    anim.SetBool("Idle", false);
                    rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                }


                if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {

                    if(shouldWait)
                    {
                        StartCoroutine(Waiting());
                    }

                    Flip();
                    goToA = false;
                    goToB = true;
                }

            }

            if (goToB)
            {

                if (!isWaiting)
                {
                    anim.SetBool("Idle", false);
                    rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
                }


                if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {

                    if (shouldWait)
                    {
                        StartCoroutine(Waiting());
                    }

                    Flip();
                    goToA = true;
                    goToB = false;
                }

            }
        }
    }

    private void Flip()
    {
        walksRight = !walksRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }

    IEnumerator Waiting()
    {
        anim.SetBool("Idle", true);
        isWaiting = true;
        Flip();
        yield return new WaitForSeconds(timeToWait);
        isWaiting = false;
        anim.SetBool("Idle", false);
        Flip();
    }
}
