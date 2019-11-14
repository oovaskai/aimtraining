using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]

public class AimDownSight : MonoBehaviour
{
    public Vector3 position;
    public float zoom;

    public bool disableCrosshair = true;

    public bool useScopeImage = false;
    public GameObject scopeImage;
    public float imageDelay;

    Gun gun;
    Camera cam;
    CrosshairController crosshair;
    HandController hand;
    MeshRenderer[] mesh;

    float timer;
    float initFov;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
        cam = Camera.main;
        crosshair = GameObject.Find("HUD").GetComponent<CrosshairController>();
        hand = GetComponentInParent<HandController>();
        mesh = GetComponentsInChildren<MeshRenderer>();

        timer = 0;
        initFov = cam.fieldOfView;

        hand.OnAim += Aim;
        hand.OnReleaseAim += ReleaseAim;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.paused)
            return;

        if (Input.GetButton("Fire2") && !gun.reloading)
        {
            hand.Aim(position);
        }

        if (Input.GetButtonUp("Fire2"))
        {
            hand.ReleaseAim();
        }

        if (hand.aiming)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, initFov - zoom, Time.deltaTime * (hand.aimSpeed / gun.weight));

            if (useScopeImage && timer >= 0)
            {
                timer += Time.deltaTime;

                if (timer >= imageDelay)
                {
                    scopeImage.SetActive(true);
                    MeshEnabled(false);
                    timer = -1;
                }
            }
        }
    }

    void Aim()
    {
        crosshair.crosshairEnabled = !disableCrosshair;
    }

    void ReleaseAim()
    {
        crosshair.crosshairEnabled = true;
        cam.fieldOfView = initFov;

        if (useScopeImage)
        {
            timer = 0;
            MeshEnabled(true);
            scopeImage.SetActive(false);
        }
    }

    void MeshEnabled(bool value)
    {
        foreach (MeshRenderer r in mesh)
        {
            r.enabled = value;
        }
    }
}
