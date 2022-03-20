using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum TruckType
{
    Importation,
    Exportation
}

public abstract class TruckBase : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float _moveSpeed = 6f;
    [SerializeField] protected float _stopDistance = 1f;

    [Header("Truck Settings")]
    [SerializeField] private TruckType _type;
    [Range(1, 30)]
    [SerializeField] protected int _maxPackages = 10;

    protected Stack<Package> _packages = new Stack<Package>();
    protected TruckDoor _targetDoor;
    protected bool _reachDoor = false;
    
    protected PackageContainer _container;

    public TruckType Type { get { return _type; } }
    public bool Equipped => _packages.Count == _maxPackages;
    public bool HasPackages => _packages.Count > 0;

    public virtual void SetTargetDoor(TruckDoor target)
    {
        _targetDoor = target;
        target.SetTruck(this);
    }

    public virtual Package GetPackage()
    {
        if (HasPackages == false) return null;

        return _packages.Pop();
    }

    public virtual void AddPackage(Package package)
    {
        if (_packages.Count >= _maxPackages) return;

        _packages.Push(package);
    }

    protected virtual void MoveToDoor()
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
