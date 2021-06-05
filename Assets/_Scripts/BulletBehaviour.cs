using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D _bulletRb;

    public float speed;

    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _bulletRb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        SwitchSpeed();
    }

    private void SwitchSpeed()
    {
        if (playerTransform.localScale.x < 0)
        {
            speed *= (-1);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _bulletRb.velocity = new Vector2(speed, _bulletRb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
