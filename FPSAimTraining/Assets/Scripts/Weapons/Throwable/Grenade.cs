using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Throwable
{
    public GameObject explosion;
    public float timer;
    public float damage;
    public float radius;

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Destroy(gameObject);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        List<Transform> targets = new List<Transform>();
            
        foreach (Collider hit in hits)
        {
            BreakGlass glass = hit.transform.GetComponent<BreakGlass>();
            if (glass)
            {
                glass.Break();
            }

            Target target = hit.transform.GetComponentInParent<Target>();
            if (target)
                targets.Add(hit.transform);
        }

        foreach (Transform target in targets)
        { 
            float range  = Vector3.Distance(target.position, transform.position);

            int layer = LayerMask.NameToLayer("Explosion");
            RaycastHit rayHit;
            Ray ray = new Ray(transform.position, target.position - transform.position);

            if (Physics.Raycast(ray, out rayHit, radius, layer))
            {
                Target t = rayHit.transform.GetComponent<Target>();
                if (t)
                {
                    float dmgMultiplier = 1 - (range/radius);
                    t.Hit(damage * dmgMultiplier, false);
                }
            }             
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BreakGlass glass = collision.transform.GetComponent<BreakGlass>();

        if(glass && !glass.bulletproof)
        {
            glass.Break();
        }
    }

    public override void SetStats()
    {
        throwableStats = "Damage:\n" +
                            "Radius:\n" +
                            "Timer:";
        throwableValues = damage + "\n" +
                            radius + "\n" +
                            timer;
    }
}
