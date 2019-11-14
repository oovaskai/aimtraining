using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Text weaponName;
    public Text ammoCount;
    public HandController hand;

    public Text weaponStats;
    public Text weaponValues;

    public GameObject inventoryItem;

    Canvas canvas;
    List<GameObject> items;
    

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        items = new List<GameObject>();

        foreach(GameObject weapon in hand.Inventory)
            if (weapon != null)
                CreateInventoryItem(weapon.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (hand != null && hand.selectedWeapon != null)
        { 
            if (weaponName.text != hand.selectedWeapon.name)
            {
                weaponName.text = hand.selectedWeapon.name;

                weaponStats.text = hand.selectedWeapon.weaponStats;
                weaponValues.text = hand.selectedWeapon.weaponValues;

                UpdateInventory();
            }

            Gun gun = hand.selectedWeapon.GetComponent<Gun>();
            ThrowableWeapon throwable = hand.selectedWeapon.GetComponent<ThrowableWeapon>();

            if (gun)
            {
                ammoCount.text = gun.ammoCount + " / " + gun.maxAmmo;
            }
            else if (throwable)
            {
                ammoCount.text = "∞";
            }
            else
            {
                ammoCount.text = "";
            }
            

        }
        else
        {
            weaponName.text = "";
            ammoCount.text = "";
        }
    }

    void UpdateInventory()
    {
        for (int i = 0; i < hand.Inventory.Length; i++)
        {
            if (hand.Inventory[i] != null)
            {
                if (i > items.Count -1)
                    CreateInventoryItem(hand.Inventory[i].name);

                else if (hand.Inventory[i].name != items[i].GetComponent<Text>().text)
                {
                    items[i].GetComponent<Text>().text = hand.Inventory[i].name;
                }
            }
        }

        foreach (GameObject item in items)
        {
            Text t = item.GetComponent<Text>();
            if (hand.selectedWeapon.name == t.text)
                t.color = new Color(1, 0.45f, 0.45f);
            else
                t.color = Color.gray;
        }
    }

    void CreateInventoryItem(string item)
    {
        float y = -26 * items.Count;
        Vector3 pos = new Vector3(0, y, 0);

        GameObject weaponText = Instantiate(inventoryItem, canvas.transform.Find("Inventory").Find("Items"));
        weaponText.transform.localPosition = pos;

        weaponText.GetComponent<Text>().text = item;
        weaponText.transform.Find("Slot").GetComponent<Text>().text = "[" + (items.Count + 1) + "]";

        items.Add(weaponText);
    }
}
