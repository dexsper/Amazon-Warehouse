using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum TruckType
{
    Importation,
    Exportation
}

[RequireComponent(typeof(PackageContainer))]
public abstract class TruckBase : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float _moveSpeed = 6f;
    [SerializeField] protected float _stopDistance = 1f;

    [Header("Truck Settings")]
    [SerializeField] private TruckType _type;

    protected PackageContainer _container;
   
    protected TruckDoor _targetDoor;
    protected bool _reachDoor = false;
    protected Vector3 _startPos;
    
    public bool Release { get; protected set; }
    public PackageContainer Container => _container;
    public bool ReachDoor => _reachDoor;
    public TruckType Type => _type;



    private void Awake()
    {
        _container = GetComponent<PackageContainer>();
    }

    protected virtual void Start()
    {
        _startPos = transform.position;
        Release = false;
    }

    public virtual void SetTargetDoor(TruckDoor target)
    {
        _targetDoor = target;

        if(target != null)
            target.SetTruck(this);
    }


    protected virtual void MoveTo(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _moveSpeed * Time.deltaTime);
    }
}
