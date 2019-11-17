using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillTextAnimation : MonoBehaviour
{
    public float lifeTime = 2;
    public Vector3 scale = new Vector3(2, 2, 2);
    public float fadeTime = 1;
    public float moveSpeed = 2;

    Text text;
    Vector3 initPos;
    float timer;
    bool playing;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        transform.localScale = Vector3.zero;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            timer += Time.deltaTime;

            transform.localScale = scale * timer/lifeTime;

            transform.position = Camera.main.WorldToScreenPoint(initPos + Vector3.up * timer * moveSpeed);

            if (transform.position.z < 0)
                Destroy(gameObject);

            if (timer >= lifeTime - fadeTime)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, (lifeTime - timer) / fadeTime );
            }

        }
    }

    public void Play(Vector3 initPosition)
    {
        initPos = initPosition;
        playing = true;
        Destroy(gameObject, lifeTime);
    }
}
