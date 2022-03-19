using UnityEngine;

public class Truck : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 6f;


    private TruckDoor _targetDoor;
    private Vector3 _startPoint;

    private bool _unloaded = false;

    private bool _reach = false;

    private void Start()
    {
        _startPoint = transform.position;
    }

    public void SetTargetDoor(TruckDoor target)
    {
        _targetDoor = target;
    }


    private void Update()
    {
        if(_targetDoor != null && _reach == false)
        {
            float distance = Vector3.Distance(transform.position, _targetDoor.transform.position);

            if(distance <= .5f)
            {
                _reach = true;
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _targetDoor.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
}
