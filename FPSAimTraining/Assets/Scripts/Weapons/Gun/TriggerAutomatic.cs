using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAutomatic : MonoBehaviour
{
    bool trigger;

    float shotTimer;
    Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
        shotTimer = 0;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.paused)
            return;

        trigger = Input.GetButton("Fire1");

        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }

        if (trigger && shotTimer <= 0)
        {
                gun.Shoot();
                shotTimer = 1 / gun.fireRate;
        }
    }
}
