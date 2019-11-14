using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Gun))]
[RequireComponent(typeof(Animator))]

public class ReloadClip : MonoBehaviour
{
    public GameObject emptyClip;
    public Transform clip;

    Gun gun;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.paused)
            return;

        if (Input.GetButtonDown("Reload"))
        {
            gun.Reload();
        }
    }

    public void ReleaseClip()
    {
        GameObject emptyMag = Instantiate(emptyClip, clip.position, clip.rotation);
        emptyMag.GetComponent<Rigidbody>().AddForce(-emptyMag.transform.up * 3f, ForceMode.Impulse);
        Destroy(emptyMag, 5);
    }

    public void InsertClip()
    {
        gun.ammoCount = gun.maxAmmo;
        anim.SetBool("MagazineEmpty", false);
    }
}
