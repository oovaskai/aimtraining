using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectShell : MonoBehaviour
{
    public GameObject shell;
    public Transform ejectPoint;
    public Vector3 ejectDirection;

    void Eject()
    {
        GameObject emptyShell = Instantiate(shell, ejectPoint.position, transform.rotation);

        Vector3 dir = -transform.right * ejectDirection.x + transform.up * ejectDirection.y + transform.forward * ejectDirection.z;

        Rigidbody shellRb = emptyShell.GetComponent<Rigidbody>();
        shellRb.AddForce(dir * Random.Range(0.7f, 1), ForceMode.Impulse);
        shellRb.AddRelativeTorque(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f) * 2, ForceMode.Impulse);
        Destroy(emptyShell, 5);
    }
}
