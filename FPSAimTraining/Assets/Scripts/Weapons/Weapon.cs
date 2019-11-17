using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SoundManager))]

public abstract class Weapon : MonoBehaviour
{
    public float weight = 1;

    internal FirstPersonController player;
    internal HandController hand;
    internal Animator anim;
    internal SoundManager sound;
    internal CrosshairController crosshair;

    public virtual string weaponStats { get => ""; }
    public virtual string weaponValues { get => ""; }
    public virtual string weaponAmmo { get => ""; }

    public virtual void Start()
    {
        player = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        hand = transform.parent.GetComponent<HandController>();
        anim = GetComponent<Animator>();
        sound = GetComponent<SoundManager>();
        crosshair = GameObject.Find("HUD").GetComponent<CrosshairController>();
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
}
