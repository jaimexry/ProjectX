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

    private Rigidbody2D _playerRb;

    public Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(gun.GunReload());
        }
    }
    
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        _playerRb.velocity = new Vector2(horizontalInput * speed, _playerRb.velocity.y);
        if (_playerRb.velocity.x > 0)
        {
            this.transform.localScale = new Vector3(1, this.transform.localScale.y, 1);
        }
        else if (_playerRb.velocity.x < 0)
        {
            this.transform.localScale = new Vector3(-1, this.transform.localScale.y, 1);
        }
        if (Input.GetButton("Jump") && isGrounded)
        {
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
