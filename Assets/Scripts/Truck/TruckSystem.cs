using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class TruckSystem : MonoBehaviour
{
    [Header("Truck Spawning Settings")]
    private float _delay = 10f;

    private List<TruckDoor> _doors = new List<TruckDoor>();

    [Inject]
    private PoolManager _poolManager;

    [SerializeField]
    private GameObject _importPrefab;

    [SerializeField]
    private GameObject _exportPrefab;

    private void Awake()
    {
        _poolManager.WarmPool(_importPrefab, 2);

        FindFreeDoor(TruckType.Importation);
    }

    public void AddDoor(TruckDoor door)
    {
        _doors.Add(door);
    }

    float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _delay)
        {
            _timer = 0;

            FindFreeDoor(TruckType.Importation);
        }
    }

    private void FindFreeDoor(TruckType truckType)
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            if (_doors[i].HasTruck) continue;
            if(_doors[i].DoorType != truckType) continue;

            CallTruck(_doors[i]);
            break;
        }
    }

    private void CallTruck(TruckDoor door)
    {
        if (door.HasTruck) return;

        var truckPrefab = door.DoorType == TruckType.Importation ? _importPrefab : _exportPrefab;
        var truck = _poolManager.
            SpawnObject(truckPrefab, door.TruckSpawnPoint.position, Quaternion.LookRotation(-door.transform.forward), null).
            GetComponent<Truck>();

        truck.SetTargetDoor(door);
    }
}
