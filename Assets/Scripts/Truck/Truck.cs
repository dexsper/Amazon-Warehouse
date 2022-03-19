using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum TruckType
{
    Importation,
    Exportation
}

public class Truck : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _stopDistance = 1f;

    [Range(1, 20)]
    [SerializeField] private int _maxPackages = 10;

    [Inject]
    private PoolManager _poolManager;

    private Stack<Package> _packages = new Stack<Package>();

    private TruckDoor _targetDoor;
    private bool _reachDoor = false;

    private TruckType _type;

    public bool Equipped => _packages.Count == _maxPackages;
    public bool HasPackages => _packages.Count > 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(_targetDoor != null && _reachDoor == false)
        {
            MoveToDoor();
        }
    }
    public void SetTargetDoor(TruckDoor target)
    {
        _targetDoor = target;
        target.SetTruck(this);
    }

    public Package GetPackage()
    {
        if (HasPackages == false) return null;

        return _packages.Pop();
    }

    public void AddPackage(Package package)
    {
        if (_packages.Count >= _maxPackages) return;

        _packages.Push(package);
    }

    private void MoveToDoor()
    {
        float distance = Vector3.Distance(transform.position, _targetDoor.transform.position);

        if (distance <= _stopDistance)
        {
            _reachDoor = true;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetDoor.transform.position, _moveSpeed * Time.deltaTime);
    }
}
