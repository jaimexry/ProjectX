using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public Material original;
    public Material blink;
    private SpriteRenderer enemyRender;

    private void Start()
    {
        enemyRender = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Blinking(float timer)
    {
        ChangeMaterial(blink);
        yield return new WaitForSeconds(timer);
        ChangeMaterial(original);
    }

    private void ChangeMaterial(Material newMaterial)
    {
        enemyRender.material = newMaterial;
    }
}
