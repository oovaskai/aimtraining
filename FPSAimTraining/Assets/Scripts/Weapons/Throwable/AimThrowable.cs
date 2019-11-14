using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrowableWeapon))]
[RequireComponent(typeof(LineRenderer))]

public class AimThrowable : MonoBehaviour
{
    public int maxLength;

    ThrowableWeapon weapon;
    LineRenderer line;
    float stepSize;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<ThrowableWeapon>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        stepSize = Time.fixedDeltaTime * 1f; //Less precise stepSize = speed up calculations, may cause rounding errors, More precise = more accuracity
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.armed)
            Aim();
        else
        {
            if (line.enabled)
                line.enabled = false;
        }
    }

    void Aim()
    {

        line.positionCount = maxLength;
        Vector3 currentVelocity = Camera.main.transform.forward * weapon.throwDistance + new Vector3(0f, weapon.throwForceY, 0f);
        Vector3 currentPosition = transform.position;

        Vector3 newVelocity = Vector3.zero;
        Vector3 newPosition = Vector3.zero;

        for (int i = 0; i < maxLength; i++)
        {
            line.SetPosition(i, currentPosition);

            newPosition = currentPosition + stepSize * currentVelocity;
            newVelocity = currentVelocity + stepSize * Physics.gravity;

            if (CheckHit(currentPosition, newPosition))
            {
                line.positionCount = i + 1;
                break;
            }


            currentVelocity = newVelocity;
            currentPosition = newPosition;
        }

        if (!line.enabled)
            line.enabled = true;
    }

    bool CheckHit(Vector3 currentPosition, Vector3 newPosition)
    {
        Vector3 direction = (newPosition - currentPosition).normalized;
        float distance = Vector3.Distance(newPosition, currentPosition);

        RaycastHit hit;

        return Physics.Raycast(currentPosition, direction, out hit, distance);

    }
}
