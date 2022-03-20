using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum TruckType
{
    Importation,
    Exportation
}

public abstract class TruckBase : PackageContainer
{
    [Header("Movement Settings")]
    [SerializeField] protected float _moveSpeed = 6f;
    [SerializeField] protected float _stopDistance = 1f;

    [Header("Truck Settings")]
    [SerializeField] private TruckType _type;

   
    protected TruckDoor _targetDoor;
    protected bool _reachDoor = false;
    protected Vector3 _startPos;
    
    public bool Release { get; protected set; }

    public bool ReachDoor => _reachDoor;
    public TruckType Type => _type;


    private void Start()
    {
        _startPos = transform.position;
        Release = false;
    }

    public virtual void SetTargetDoor(TruckDoor target)
    {
        _targetDoor = target;
        target.SetTruck(this);
    }


    protected virtual void MoveTo(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _moveSpeed * Time.deltaTime);
    }
}
