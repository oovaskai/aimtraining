using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    public Transform brokenGlass;

    public bool bulletproof;

    public void Break()
    {
        Destroy(gameObject);
        Transform broken = Instantiate(brokenGlass, transform.position, transform.rotation);
        broken.localScale = transform.localScale;
    }
}
