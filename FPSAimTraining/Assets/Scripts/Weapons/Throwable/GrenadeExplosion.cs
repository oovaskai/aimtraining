using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    public float fadeInTime;

    Light explosionLight;
    float intens;

    // Start is called before the first frame update
    void Start()
    {
        explosionLight = GetComponent<Light>();
        intens = explosionLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        intens -= Time.deltaTime / fadeInTime;
        explosionLight.intensity = intens;
    }
}
