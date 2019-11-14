using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialEndTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TimeTrial.EndTrial();
        }
    }
}
