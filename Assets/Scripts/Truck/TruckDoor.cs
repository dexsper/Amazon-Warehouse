using UnityEngine;
using Zenject;

public class TruckDoor : MonoBehaviour
{
    private Truck _truck;

    [Inject]
    private TruckSystem _truckSystem;

    [SerializeField]
    private TruckType _doorType;

    public Transform TruckSpawnPoint;

    public TruckType DoorType
    {
        get { return _doorType; }
    }
    
    public bool HasTruck
    {
        get { return _truck != null; }
    }

    public void SetTruck(Truck truck)
    {
        _truck = truck;
    }

    private void Start()
    {
        _truckSystem.AddDoor(this);
    }
}
