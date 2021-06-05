using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public int maxAmmo;
    public int currentAmmo;
    public bool isReloading;
    public void Shoot()
    {
        if (currentAmmo > 0 && !isReloading)
        {
            Instantiate(bullet, this.transform.position, bullet.transform.rotation);
            currentAmmo--;
        }
        else if (!isReloading)
        {
            StartCoroutine(GunReload());
        }
    }

    public IEnumerator GunReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
