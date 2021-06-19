using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float speed;

    private Rigidbody2D _enemyRb;
    private Animator enemyAnim;
    
    public bool isStatic;
    public bool isWalker;
    public bool isPatrol;
    public bool shouldWait;

    public float timeToWait;
    public bool isWalkingRight;

    public Transform wallCheck, pitCheck, groundCheck;
    private bool wallDetected, pitDetected, isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Transform pointA, pointB;
    public bool goToA, goToB;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        speed = GetComponent<Enemy>().speed;
        enemyAnim = GetComponent<Animator>();
        goToA = true;
    }

    private void Update()
    {
        pitDetected = !Physics2D.OverlapCircle(pitCheck.position, checkRadius, whatIsGround);
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, checkRadius, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if ((pitDetected || wallDetected) && isGrounded)
        {
            FlipEnemy();
        }
    }

    private void LateUpdate()
    {
        enemyAnim.SetBool("Idle", isStatic);
    }

    private void FixedUpdate()
    {
        if (isStatic)
        {
            _enemyRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (isWalker)
        {
            _enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (!isWalkingRight)
            {
                _enemyRb.velocity = new Vector2(-speed, _enemyRb.velocity.y);
            }
            else
            {
                _enemyRb.velocity = new Vector2(speed, _enemyRb.velocity.y);
            }
        }

        if (isPatrol)
        {
            _enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (goToA)
            {
                if (!isStatic)
                {
                    _enemyRb.velocity = new Vector2(-speed, _enemyRb.velocity.y);
                    if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
                    {
                        if (shouldWait)
                        {
                            StartCoroutine(Waiting());
                        }
                        goToA = false;
                        goToB = true;
                        FlipEnemy();
                    }
                }
            }

            if (goToB)
            {
                if (!isStatic)
                {
                    _enemyRb.velocity = new Vector2(speed, _enemyRb.velocity.y);
                    if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                    {
                        if (shouldWait)
                        {
                            StartCoroutine(Waiting());
                        }
                        goToB = false;
                        goToA = true;
                        FlipEnemy();
                    }
                }
            }
        }
    }

    private void FlipEnemy()
    {
        isWalkingRight = !isWalkingRight;
        transform.localScale *= new Vector2(-1, transform.localScale.y);
    }

    private IEnumerator Waiting()
    {
        isStatic = true;
        FlipEnemy();
        yield return new WaitForSeconds(timeToWait);
        isStatic = false;
        FlipEnemy();
    }
}
