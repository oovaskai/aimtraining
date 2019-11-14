using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public abstract class Projectile : MonoBehaviour
{
    internal float damage;
    internal Rigidbody rbody;

    public virtual void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }

    public virtual void FixedUpdate()
    {
        CheckHit();
    }

    public virtual void Launch(Vector3 velocity, float damage)
    {
        rbody.velocity = velocity;
        this.damage = damage;
    }

    public abstract void CheckHit();
}
