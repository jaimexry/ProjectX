using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpForce;
    private bool isGrounded;
    public float horizontalInput;
    public Transform _groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private Rigidbody2D _playerRb;
    private Animator _playerAnim;
    private bool isWalking;
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, whatIsGround);
        Attack();
    }

    private void FixedUpdate()
    {
        Movement();
        FlipPlayer();
        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void LateUpdate()
    {
        _playerAnim.SetBool("Walk", isWalking);
        _playerAnim.SetBool("Jump", !isGrounded);
        _playerAnim.SetBool("Attack", isAttacking);
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        _playerRb.velocity = new Vector2(horizontalInput * speed, _playerRb.velocity.y);
        isWalking = _playerRb.velocity.x != 0 ? isWalking = true : isWalking = false;
    }

    private void FlipPlayer()
    {
        if (_playerRb.velocity.x > 0)
        {
            this.transform.localScale = new Vector3(1, this.transform.localScale.y, 1);
        }
        else if (_playerRb.velocity.x < 0)
        {
            this.transform.localScale = new Vector3(-1, this.transform.localScale.y, 1);
        }
    }

    private void Jump()
    {
        _playerRb.velocity = new Vector2(_playerRb.velocity.x, jumpForce);
    }

    private void Attack()
    {
        isAttacking = Input.GetButtonDown("Fire1") ? isAttacking = true : isAttacking = false;
    }
}
