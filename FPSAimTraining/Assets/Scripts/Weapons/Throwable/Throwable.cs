using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Throwable : MonoBehaviour
{
    public string throwableStats = "";
    public string throwableValues = "";

    public abstract void SetStats();

}
