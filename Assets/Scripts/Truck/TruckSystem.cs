using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class TruckSystem : MonoBehaviour
{
    [Header("Truck Spawning Settings")]
    private float _importDelay = 14f;
    private float _exportDelay = 10f;

    private List<TruckDoor> _doors = new List<TruckDoor>();

    [Inject]
    private PoolManager _poolManager;

    [SerializeField]
    private GameObject _importTruckPrefab;

    [SerializeField]
    private GameObject _exportTruckPrefab;

    [SerializeField]
    private GameObject _packagePrefab;


    private List<TruckBase> _activeTrucks = new List<TruckBase>();

    private void Start()
    {
        if (_importTruckPrefab != null)
            _poolManager.WarmPool(_importTruckPrefab, 2);
        if (_exportTruckPrefab != null)
            _poolManager.WarmPool(_exportTruckPrefab, 2);
        if (_packagePrefab != null)
            _poolManager.WarmPool(_packagePrefab, 40);

        FindFreeDoor(TruckType.Importation);
    }

    public void AddDoor(TruckDoor door)
    {
        _doors.Add(door);
    }

    float _importTimer = 0;
    float _exportTimer = 0;

    private void Update()
    {
        _importTimer += Time.deltaTime;
        _exportTimer += Time.deltaTime;

        if (_importTimer >= _importDelay)
        {
            _importTimer = 0;

            FindFreeDoor(TruckType.Importation);
        }

        if(_exportTimer >= _exportDelay)
        {
            _exportTimer = 0;
            FindFreeDoor(TruckType.Exportation);
        }

        for (int i = 0; i < _activeTrucks.Count; i++)
        {
            if (_activeTrucks[i].Release)
            {
                var truck = _activeTrucks[i];
                truck.SetTargetDoor(null);

                while(truck.Container.HasPackages)
                {
                    var package = truck.Container.GetPackage();
                    package.SetState(PackageState.New);

                    _poolManager.ReleaseObject(package.gameObject);
                }

                _activeTrucks.Remove(truck);
                _poolManager.ReleaseObject(truck.gameObject);
            }
        }
    }

    private void FindFreeDoor(TruckType truckType)
    {
        for (int i = 0; i < _doors.Count; i++)
        {
            if (_doors[i].HasTruck) continue;
            if (_doors[i].DoorType != truckType) continue;

            CallTruck(_doors[i]);
            break;
        }
    }

    private void CallTruck(TruckDoor door)
    {
        if (door.HasTruck) return;

        var truckPrefab = door.DoorType == TruckType.Importation ? _importTruckPrefab : _exportTruckPrefab;

        Quaternion rotation = Quaternion.LookRotation(door.transform.forward);

        if (door.DoorType == TruckType.Importation)
            rotation = Quaternion.LookRotation(-door.transform.forward);

        var truck = _poolManager.
            SpawnObject(truckPrefab, door.TruckSpawnPoint, rotation, null).
            GetComponent<TruckBase>();

        truck.gameObject.SetActive(true);

        if (truck.Type == TruckType.Importation)
        {
            while (!truck.Container.Equipped)
            {
                truck.Container.AddPackage(_poolManager.SpawnObject(_packagePrefab).GetComponent<Package>());
            }
        }

        truck.SetTargetDoor(door);
        _activeTrucks.Add(truck);
    }
}
