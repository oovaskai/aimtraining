using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Gun))]

public class TriggerSemiauto : MonoBehaviour
{
    float shotTimer;
    Gun gun;

    void Start()
    {
        gun = GetComponent<Gun>();
        shotTimer = 0;
    }

    void Update()
    {
        if (PauseMenu.paused)
            return;

        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && shotTimer <= 0)
        {
            gun.Shoot();
            shotTimer = 1/gun.fireRate;
        }
    }
}
