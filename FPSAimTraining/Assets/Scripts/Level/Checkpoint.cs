using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Target[] targets;
    Animator anim;

    bool roomClear;

    public GameObject targetHolder;

    // Start is called before the first frame update
    void Start()
    {
        targets = targetHolder.GetComponentsInChildren<Target>();
        anim = GetComponent<Animator>();
        roomClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!roomClear)
        {
            foreach (Target target in targets)
                if (!target.dead)
                    return;

            anim.Play("OpenDoor");
            roomClear = true;
        }
    }
}
