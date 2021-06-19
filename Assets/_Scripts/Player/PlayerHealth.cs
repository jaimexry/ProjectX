using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public Image healthImage;
    private PlayerController player;

    private Blink playerBlink;
    private Rigidbody2D _playerRb;
    public float knockbackForceX, knockbackForceY;

    private bool immnune;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthImage.fillAmount = health / maxHealth;
        player = GetComponentInParent<PlayerController>();
        playerBlink = GetComponentInParent<Blink>();
        _playerRb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !immnune)
        {
            health -= other.GetComponent<Enemy>().damageToPlayer;
            healthImage.fillAmount = health / maxHealth;
            StartCoroutine(Immunity());
            StartCoroutine(player.PlayerCantMove());
            StartCoroutine(player.PlayerCantAttack());
            if (other.transform.position.x < transform.position.x)
            {
                _playerRb.velocity = Vector2.zero;
                _playerRb.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                _playerRb.velocity = Vector2.zero;
                _playerRb.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            if (health <= 0)
            {
                Destroy(player.gameObject);
            }
            else
            {
                StartCoroutine(playerBlink.Blinking(0.3f));
            }
        }
    }

    private IEnumerator Immunity()
    {
        immnune = true;
        yield return new WaitForSeconds(0.3f);
        immnune = false;
    }
}
