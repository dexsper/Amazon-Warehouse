using UnityEngine;

public enum PackageState
{
    New,
    Calculated,
    Sorted
}

public class Package : MonoBehaviour
{
    private PackageState _state;

    public PackageState State
    {
        get { return _state; }
    }
}
