using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Vector3 positionOffset;
    public float showTime = 2;
    public GameObject killAnim;

    public Transform target;

    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + positionOffset);

        if (transform.position.z < 0)
            gameObject.SetActive(false);

        timer += Time.deltaTime;
        if (timer >= showTime)
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowHealth(float ratio)
    {
        gameObject.SetActive(true);
        timer = 0;

        var barImage = new List<Image>(GetComponentsInChildren<Image>()).Find(img => img != GetComponent<Image>());
        barImage.fillAmount = ratio;
    }

    public void KillAnimation(string text, Color color)
    {
        KillTextAnimation anim = Instantiate(killAnim, FindObjectOfType<Canvas>().transform).GetComponent<KillTextAnimation>();

        Text animText = anim.GetComponent<Text>();

        animText.text = text;
        animText.color = color;

        anim.Play(target.position + positionOffset);
        gameObject.SetActive(false);
    }
}
