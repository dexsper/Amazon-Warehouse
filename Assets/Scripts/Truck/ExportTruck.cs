using System;
using UnityEngine;
using Zenject;

public class ExportTruck : TruckBase
{
    [Header("Export Settings")]
    [SerializeField]
    private int _moneyPerPackage = 20;

    [Inject]
    private Player _player;

    private bool _moneyGived = false;

    private void Start()
    {
        _moneyGived = false;
    }

    private void Update()
    {
        if (_targetDoor != null && _reachDoor == false)
        {
            float distance = Vector3.Distance(transform.position, _targetDoor.transform.position);

            if (distance <= _stopDistance)
            {
                _reachDoor = true;
                return;
            }

            MoveTo(_targetDoor.transform.position);
        }
        else if (_reachDoor && _container.Equipped && Release == false)
        {
            if(_moneyGived == false)
            {
                GiveMoney();
            }

            float distance = Vector3.Distance(transform.position, _startPos);

            if (distance <= _stopDistance)
            {
                Release = true;
                _targetDoor.SetTruck(null);

                return;
            }

            MoveTo(_startPos);
        }
    }

    private void GiveMoney()
    {
        _player.Economics.Deposit(_container.PackagesCount * _moneyPerPackage);
        _moneyGived = true;
    }
}

