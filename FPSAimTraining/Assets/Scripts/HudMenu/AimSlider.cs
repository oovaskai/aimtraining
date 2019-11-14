using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimSlider : MonoBehaviour
{
    public float defaultValue = 0.5f;

    Slider slider;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        text = slider.GetComponentInChildren<Text>();

        slider.value = Settings.AimMultiplier;
    }

    public void SetAimMultiplier()
    {
        text.text = slider.value.ToString("F1");
        Settings.AimMultiplier = float.Parse(text.text);

        PlayerPrefs.SetFloat("AimMultiplier", Settings.AimMultiplier);
    }

    public void Default()
    {
        slider.value = defaultValue;
    }
}
