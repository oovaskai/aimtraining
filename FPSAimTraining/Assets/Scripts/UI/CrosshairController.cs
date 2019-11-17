using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Sprite crosshairDefault;
    public Sprite crosshairTarget;

    Image crosshair;

    public bool crosshairEnabled;
    public bool targetInRange;

    void Start()
    {
        crosshair = transform.Find("Crosshair").GetComponent<Image>();
        crosshairEnabled = true;
    }

    private void Update()
    {
        crosshair.enabled = crosshairEnabled;
        if (targetInRange)
        {
            crosshair.sprite = crosshairTarget;
        }
        else
        {
            crosshair.sprite = crosshairDefault;
        }
    }
}
