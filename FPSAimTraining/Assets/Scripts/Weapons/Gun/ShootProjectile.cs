using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectile;
    public float initialSpeed;

    Transform cam;
    Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        gun = GetComponent<Gun>();

        gun.OnShoot += Shoot;
    }

    void Shoot()
    {
        Projectile p = Instantiate(projectile, cam.position, transform.rotation).GetComponent<Projectile>();
        if (p)
            p.Launch(cam.forward * initialSpeed, gun.damage);
    }
}
