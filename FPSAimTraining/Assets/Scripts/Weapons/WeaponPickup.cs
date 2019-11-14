using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public float rotationSpeed;

    public GameObject giveWeapon;
    public int inventorySlot;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            PickUp(giveWeapon.GetComponentInParent<HandController>());
        }
    }

    void PickUp(HandController hand)
    {
        hand.Inventory[inventorySlot] = giveWeapon;

        if (inventorySlot > 0)
            hand.SelectItem(inventorySlot - 1);
        else
            hand.SelectItem(inventorySlot + 1);

        hand.SelectItem(inventorySlot);
        Destroy(gameObject);
    }
}
