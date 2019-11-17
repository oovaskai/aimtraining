using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Text weaponName;
    public Text weaponAmmo;
    public HandController hand;

    public Text weaponStats;
    public Text weaponValues;

    public GameObject inventoryItem;

    Canvas canvas;
    List<GameObject> items;
    
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        items = new List<GameObject>();

        foreach(GameObject weapon in hand.Inventory)
            if (weapon != null)
                CreateInventoryItem(weapon.name);
    }

    void Update()
    {
        if (hand != null && hand.selectedWeapon != null)
        {   
            Weapon weapon = hand.selectedWeapon;

            if (weaponName.text != weapon.name)
            {
                weaponName.text = weapon.name;

                weaponStats.text = weapon.weaponStats;
                weaponValues.text = weapon.weaponValues;

                UpdateInventory();
            }

            weaponAmmo.text = weapon.weaponAmmo;
        }
        else
        {
            weaponStats.text = "";
            weaponValues.text = "";
            weaponName.text = "";
            weaponAmmo.text = "";
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
