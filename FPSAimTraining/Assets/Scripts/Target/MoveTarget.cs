using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public Transform target;
    public bool moving = false;

    public Transform[] points;
    Vector3 currentPoint;

    public bool loop;
    public bool reverse;
    public float speed;

    int pointSelection;
    bool reversing;
    LineRenderer rail;
    HingeJoint hinge;

    void Start()
    {
        hinge = target.GetComponent<HingeJoint>();
        rail = GetComponent<LineRenderer>();
        rail.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 pos = points[i].position;
            pos.y += rail.startWidth / 2;
            rail.SetPosition(i, pos);
        }
        currentPoint = points[pointSelection].position;
        hinge.connectedAnchor = target.position;

        rail.loop = loop ? !reverse : false;
        reversing = false;
    }

    void Update()
    {
        if (moving)
        {
            Vector3 newPos = Vector3.MoveTowards(target.transform.position, currentPoint, Time.deltaTime * speed);
            hinge.connectedAnchor = newPos;
            target.transform.position = newPos;

            if (target.transform.position == currentPoint)
            {
                if (loop && reverse && reversing)
                    pointSelection--;
                else
                    pointSelection++;

                if (pointSelection == points.Length)
                {
                    if (loop)
                    {

                        if (reverse)
                        {
                            pointSelection = points.Length - 2;
                            reversing = true;
                        }
                        else
                        {
                            pointSelection = 0;
                        }
                    }
                    else
                    {

                        moving = false;
                    }

                }
                
                if (reversing && pointSelection < 0)
                {
                    pointSelection = points.Length > 1 ? 1 : 0;
                    reversing = false;
                }

                if (moving)
                    currentPoint = points[pointSelection].position;
            }
        }
    }
}
