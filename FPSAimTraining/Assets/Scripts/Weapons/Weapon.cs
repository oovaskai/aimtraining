using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SoundManager))]

public abstract class Weapon : MonoBehaviour
{
    public float weight = 1;

    internal UnityStandardAssets.Characters.FirstPerson.FirstPersonController player;
    internal HandController hand;
    internal Animator anim;
    internal SoundManager sound;
    internal CrosshairController crosshair;

    internal string weaponStats;
    internal string weaponValues;

    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        hand = transform.parent.GetComponent<HandController>();
        anim = GetComponent<Animator>();
        sound = GetComponent<SoundManager>();
        crosshair = GameObject.Find("HUD").GetComponent<CrosshairController>();
        InitInventory();
    }

    public virtual void Equip()
    {
        hand.switchingWeapon = true;
        hand.ReleaseAim();
        anim.Play("Equip");
    }

    public virtual void Stop()
    {
        anim.StopPlayback();
        anim.Play("Idle");
    }

    public virtual void Ready()
    {
        hand.switchingWeapon = false;
    }

    public virtual void InitInventory()
    {
        weaponStats = "";
        weaponValues = "";
    }
}
