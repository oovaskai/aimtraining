using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertToggle : MonoBehaviour
{
    Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponentInChildren<Toggle>();
        toggle.isOn = Settings.InvertMouse;  
    }

    public void SetInvert()
    {
        if (!toggle)
            return;

        Settings.InvertMouse = toggle.isOn;
        PlayerPrefs.SetString("InvertMouse", Settings.InvertMouse.ToString());
    }
}
