using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableWeapon : Weapon
{
    public float throwDistance;
    public float throwForceY = 2;
    public float torque;
    public GameObject throwedWeapon;
    Throwable throwable;

    public bool armed;
    bool throwed;
    bool arming;

    void Awake()
    {
        throwable = throwedWeapon.GetComponent<Throwable>();
        throwable.SetStats();
    }

    void Update()
    {
        if (PauseMenu.paused)
            return;

        if (Input.GetButton("Fire1") && !arming)
        {
            Arm();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            throwed = true;
        }

        if (armed && throwed)
        {
            ReleaseWeapon();

            anim.SetTrigger("Throw");
            hand.PickUpAnimation();
            throwed = false;
            armed = false;
            arming = false;
        }
    }

    public void Arm()
    {
        arming = true;
        throwed = false;
        armed = false;
        anim.ResetTrigger("Throw");
        anim.Play("Arm");
    }

    void ReleaseWeapon()
    {
        Rigidbody weapon = Instantiate(throwedWeapon, transform.position, transform.rotation).GetComponent<Rigidbody>();

        Vector3 dir = Camera.main.transform.forward * throwDistance + new Vector3(0f, throwForceY, 0f);
        weapon.velocity = dir;

        Vector3 t = new Vector3(Random.Range(-torque, torque), Random.Range(-torque, torque), Random.Range(-torque, torque)) * 100;
        weapon.AddTorque(t);

        PlayerStats.shots++;
    }

    public override void Ready()
    {
        base.Ready();
        arming = false;
        armed = false;
        throwed = false;
    }

    public override string weaponStats => throwable.throwableStats + "\nWeight";

    public override string weaponValues => throwable.throwableValues + "\n" + weight.ToString();

    public override string weaponAmmo => "∞";
}
