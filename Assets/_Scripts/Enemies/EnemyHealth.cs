using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Enemy enemy;

    public GameObject enemyDeath;

    private Blink enemyBlink;

    private Rigidbody2D enemyRb;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyBlink = GetComponentInParent<Blink>();
        enemyRb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            enemy.enemyHealth -= 2.0f;
            if (other.transform.position.x < transform.position.x)
            {
                enemyRb.AddForce(new Vector2(enemy.knockbackForceX, enemy.knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                enemyRb.AddForce(new Vector2(-enemy.knockbackForceX, enemy.knockbackForceY), ForceMode2D.Force);
            }

            if (enemy.enemyHealth <= 0)
            {
                Instantiate(enemyDeath, this.transform.position, Quaternion.identity);
                Destroy(enemy.gameObject);
            }
            else
            {
                StartCoroutine(enemyBlink.Blinking(0.5f));
            }
        }
    }
}
