using System;
using UnityEngine;

public class ImportTruck : TruckBase
{
    public new TruckType Type
    {
        get
        {
            return TruckType.Importation;
        }
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
        else if (_reachDoor && _container.HasPackages == false && Release == false)
        {
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
}

