using UnityEngine;

public class TruckDoor : MonoBehaviour
{
    private Truck _truck;

    public bool HasTruck
    {
        get { return _truck != null; }
    }

    public void SetTruck(Truck truck)
    {
        _truck = truck;
    }
}
