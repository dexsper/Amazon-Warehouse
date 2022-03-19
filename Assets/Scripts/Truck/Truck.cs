using UnityEngine;

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
    private TruckDoor _targetDoor;

    private bool _reachDoor = false;

    public void SetTargetDoor(TruckDoor target)
    {
        _targetDoor = target;
        target.SetTruck(this);
    }


    private void Update()
    {
        if(_targetDoor != null && _reachDoor == false)
        {
            MoveToDoor();
        }
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
