using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class HandController : MonoBehaviour
{
    public float aimSpeed;
    public bool aiming;
    public System.Action OnAim;
    public System.Action OnReleaseAim;

    public Weapon selectedWeapon;
    public GameObject[] Inventory;

    public bool switchingWeapon;

    Vector3 initPos;
    Vector3 targetPos;

    FirstPersonController player;
    CrosshairController hud;
    Animator anim;
    Camera cam;

    int selectedItem;

    void Start()
    {
        player = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        hud = GameObject.Find("HUD").GetComponent<CrosshairController>();
        initPos = transform.localPosition;
        targetPos = initPos;
        anim = GetComponent<Animator>();
        cam = GetComponentInParent<Camera>();
    }

    void Update()
    {
        if (PauseMenu.paused)
            return;

        if (selectedWeapon)
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * (aimSpeed / selectedWeapon.weight));

        HandleInventory();
    }

    public void Kickback(Vector2 kick)
    {
        kick = aiming ? kick/2 : kick;

        player.LookKickback(kick);

        float maxZ = Mathf.Clamp (transform.localPosition.z - kick.magnitude, targetPos.z - 0.2f, 1);

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, maxZ);
    }

    public void Aim(Vector3 aimPosition)
    {
        if (!switchingWeapon)
        {
            aiming = true;
            targetPos = aimPosition;
            OnAim?.Invoke();
        }
    }

    public void ReleaseAim()
    {
        aiming = false;
        targetPos = initPos;
        OnReleaseAim?.Invoke();
    }

    void HandleInventory()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            NextItem(-1);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            NextItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectItem(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectItem(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectItem(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectItem(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectItem(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectItem(8);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectItem(9);
        }
    }

    void NextItem(int step)
    {
        for (int i = selectedItem + step; i > selectedItem - Inventory.Length && i < Inventory.Length + selectedItem; i += step)
        {
            int index = i;
            if (index > Inventory.Length - 1)
                index = i - Inventory.Length;

            if (index < 0)
                index = i + Inventory.Length;

            if (Inventory[index] != null)
            {
                SelectItem(index);
                return;
            }    
        }
    }

    public void SelectItem(int index)
    {
        if (Inventory.Length > index && (selectedWeapon == null || selectedItem != index) && Inventory[index] != null)
        {
            ReleaseAim();
            selectedItem = index;
            switchingWeapon = true;

            if (selectedWeapon != null)
            {
                selectedWeapon.Stop();
                anim.Play("PutWeaponDown");
            }
            else
                anim.Play("PickWeaponUp");
        }
    }

    public void DisableWeapon()
    {
        selectedWeapon.gameObject.SetActive(false);
    }

    public void EnableWeapon()
    {
        hud.targetInRange = false;
        transform.localPosition = initPos;
        selectedWeapon = Inventory[selectedItem].GetComponent<Weapon>();
        selectedWeapon.gameObject.SetActive(true);
        anim.SetFloat("PickUpSpeed", 1 / selectedWeapon.weight);
    }

    public void WeaponEquipAnimation()
    {
        selectedWeapon.Equip();
    }

    public void PickUpAnimation()
    {
        anim.Play("PickWeaponUp");
    }
}
