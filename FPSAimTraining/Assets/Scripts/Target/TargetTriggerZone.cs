using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTriggerZone : MonoBehaviour
{
    public MoveTarget[] targets;
    bool triggered;

    void Start()
    {
        SetMovement(false);
        triggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !triggered)
        {
            SetMovement(true);
            triggered = true;
        }
    }

    void SetMovement(bool value)
    {
        foreach (MoveTarget target in targets)
        {
            if (!target.target.GetComponent<Target>().dead)
            {
                target.moving = value;

                if (value)
                    target.target.GetComponent<SoundManager>().PlaySound(0);
            }
        }
    }
}