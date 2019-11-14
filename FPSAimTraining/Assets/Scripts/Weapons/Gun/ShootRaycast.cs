using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]

public class ShootRaycast : MonoBehaviour
{
    public GameObject wallHitEffect;
    public GameObject bulletHole;

    Transform cam;
    Gun gun;

    void Start()
    {
        gun = GetComponent<Gun>();
        cam = Camera.main.transform;

        gun.OnShoot += Shoot;
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, gun.range))
        {
            Rigidbody hitRb = hit.transform.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                hitRb.AddForce(-hit.normal * gun.damage / 10, ForceMode.Impulse);
            }

            Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.SetParent(hit.transform);

            Target target = hit.transform.GetComponent<Target>();
            BreakGlass glass = hit.transform.GetComponent<BreakGlass>();

            if (target)
            {
                target.Hit(gun.damage, hit.collider.tag == "TargetHead");

                Destroy(Instantiate(target.hitEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)), 2);
            }
            else if (glass && !glass.bulletproof)
            {
                glass.Break();
            }

            else
            {
                Destroy(Instantiate(wallHitEffect, hit.point, Quaternion.identity), 2);
            }
        }
    }
}
