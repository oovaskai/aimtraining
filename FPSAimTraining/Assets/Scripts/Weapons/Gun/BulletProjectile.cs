using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    public GameObject bulletHole;
    public GameObject wallHitEffect;
    public float startTrailTime = 0.5f;

    TrailRenderer trail;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();

        if (trail && startTrailTime > 0)
            trail.emitting = false;

    }

    void Update()
    {
        if (trail)
        {
            startTrailTime -= Time.deltaTime;
            if (startTrailTime <= 0)
                trail.emitting = true;
        }
    }

    public override void CheckHit()
    {
        Vector3 newPos = transform.position + rbody.velocity * Time.fixedDeltaTime;
        Vector3 dir = (newPos - transform.position).normalized;
        float distance = Vector3.Distance(newPos, transform.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            Rigidbody hitRb = hit.transform.GetComponent<Rigidbody>();
            if (hitRb)
            {
                hitRb.AddForce(-hit.normal * damage / 10, ForceMode.Impulse);
            }

            Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.SetParent(hit.transform);

            Target target = hit.transform.GetComponent<Target>();
            BreakGlass glass = hit.transform.GetComponent<BreakGlass>();

            if (target)
            {
                target.Hit(damage, hit.collider.tag == "TargetHead");

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

            Destroy(gameObject);
        }
    }
}
