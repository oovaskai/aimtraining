using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]

public class ShotgunRaycast : MonoBehaviour
{
    public int pellets;
    public float radius;

    public GameObject wallHitEffect;
    public GameObject pelletHole;

    Gun gun;
    Transform cam;

    void Start()
    {
        gun = GetComponent<Gun>();
        cam = Camera.main.transform;

        gun.OnShoot += Shoot;
    }

    void Shoot()
    {
        for (int i = 0; i < pellets; i++)
        {
            Vector3 dir = cam.forward * gun.range + cam.up * Random.Range(-radius, radius) + cam.right * Random.Range(-radius, radius);
            ShootRaycast(dir);
        }
    }

    void ShootRaycast(Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.position, direction, out hit, gun.range))
        {
            Rigidbody hitRb = hit.transform.GetComponent<Rigidbody>();
            if (hitRb != null)
            {
                hitRb.AddForce(-hit.normal * (gun.damage/pellets) / 10, ForceMode.Impulse);
            }

            Instantiate(pelletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.SetParent(hit.transform);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.Hit(gun.damage/pellets, hit.collider.tag == "TargetHead");

                Destroy(Instantiate(target.hitEffect, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal)), 2);
            }
            else
            {
                Destroy(Instantiate(wallHitEffect, hit.point, Quaternion.identity), 2);
            }
        }
    }
}
