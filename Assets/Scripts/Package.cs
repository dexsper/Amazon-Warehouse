using UnityEngine;

public enum PackageState
{
    New,
    Calculated,
    Sorted
}

public class Package : MonoBehaviour
{
    [SerializeField]
    private PackageState _state;

    public PackageState State
    {
        get { return _state; }
    }

    public void SetState(PackageState state)
    {
        _state = state; 
    }
}
