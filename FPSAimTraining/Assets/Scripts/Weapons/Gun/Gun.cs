using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Gun : Weapon
{
    public float damage;
    public float range;
    public float fireRate;
    public bool scanTarget = true;

    public int maxAmmo;
    public int ammoCount;
    public bool reloading;

    public Vector2 kickback;

    public ParticleSystem muzzleEffect;

    public System.Action OnShoot;

    bool emptyShot;



    void Update()
    {
        if (scanTarget)
            ScanTarget();

        if (Input.GetButtonDown("Fire1") && ammoCount <= 0 && !emptyShot)
        {
            sound.PlaySound(4);
            emptyShot = true;
        }

        if (Input.GetButtonUp("Fire1"))
            emptyShot = false;

        player.canRun = !reloading;
    }

    public void Shoot()
    {
        if (ammoCount > 0 && !reloading && !hand.switchingWeapon)
        {
            ammoCount--;

            if (ammoCount == 0)
                anim.SetBool("MagazineEmpty", true);

            anim.Play("Shoot");

            if (muzzleEffect)
                muzzleEffect.Play();

            PlayerStats.shots++;

            OnShoot.Invoke();
            hand.Kickback(kickback);
        }
        else if (ammoCount <= 0 && !emptyShot)
        {
            sound.PlaySound(4);
            emptyShot = true;
        }
    }

    public void Reload()
    {
        if (!reloading && !hand.switchingWeapon)
        {
            hand.ReleaseAim();

            if (ammoCount == maxAmmo)
            {
                Equip();
                return;
            }

            reloading = true;
            anim.Play("Reload");
        }
    }

    void ScanTarget()
    {
        RaycastHit hit;

        if (Physics.Raycast(hand.transform.parent.position, hand.transform.parent.forward, out hit, range))
        {
            if (hit.collider.tag == "TargetHead" || hit.collider.tag == "TargetBody")
            {
                crosshair.targetInRange = true;
                return;
            }
        }

        crosshair.targetInRange = false;

    }

    public override void InitInventory()
    {
        weaponStats = "Damage" + "\n" +
                "Range" + "\n" +
                "Firerate" + "\n" +
                "Weight";

        weaponValues = damage + "\n" +
                        range + "\n" +
                        fireRate + "\n" +
                        weight;
    }

    public override void Ready()
    {
        base.Ready();
        reloading = false;
    }
}
