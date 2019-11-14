using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class FadeInTime : MonoBehaviour
{
    public float lifeTime = 7;
    public float fadeTime = 2;

    Material mat;
    float timer;
    float initA;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        initA = mat.color.a;
        timer = 0;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime - fadeTime)
        {
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, Mathf.Lerp(initA, 0, (timer - (lifeTime - fadeTime)) / fadeTime));
        }
    }
}
