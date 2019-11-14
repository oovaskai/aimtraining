using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
[RequireComponent(typeof(Animator))]

public class ReloadShell : MonoBehaviour
{
    Gun gun;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
        anim = GetComponent<Animator>();

        anim.SetBool("MagazineFull", true);
        gun.ammoCount = gun.maxAmmo;

        gun.OnShoot += Shoot;

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.paused)
            return;

        if (Input.GetButtonDown("Reload"))
        {
            gun.Reload();
        }
    }

    public void InsertShell()
    {
        gun.ammoCount++;
        if (gun.ammoCount > gun.maxAmmo)
            gun.ammoCount = gun.maxAmmo;

        if (gun.ammoCount == gun.maxAmmo)
        {
            anim.SetBool("MagazineFull", true);
            anim.SetBool("MagazineEmpty", false);
        }
    }

    void Shoot()
    {
        if (anim.GetBool("MagazineFull"))
            anim.SetBool("MagazineFull", false);
    }
}
