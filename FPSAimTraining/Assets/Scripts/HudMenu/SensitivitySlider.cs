using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    public float defaultValue = 1;

    Slider slider;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        text = slider.GetComponentInChildren<Text>();

        slider.value = Settings.MouseSensitivity;
    }

    public void SetSensitivity()
    {
        text.text = slider.value.ToString("F1");
        Settings.MouseSensitivity = float.Parse(text.text);

        PlayerPrefs.SetFloat("Sensitivity", Settings.MouseSensitivity);
    }

    public void Default()
    {
        slider.value = defaultValue;
    }
}
