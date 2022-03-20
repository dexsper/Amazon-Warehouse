using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageContainer : MonoBehaviour
{
    [SerializeField]
    private Transform[] _cells;

    public Transform[] Cells
    {
        get { return _cells; }
    }
}
