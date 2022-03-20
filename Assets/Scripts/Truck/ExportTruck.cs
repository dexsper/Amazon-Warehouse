using UnityEngine;

public class ExportTruck : TruckBase
{
    public override void AddPackage(Package package)
    {
        var cell = Cells[_packages.Count];

        base.AddPackage(package);

        StartCoroutine(package.MoveTo(cell.transform, cell, 1f));

        cell.gameObject.SetActive(true);
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
        else if (_reachDoor && Equipped && Release == false)
        {
            float distance = Vector3.Distance(transform.position, _startPos);

            if (distance <= _stopDistance)
            {
                Release = true;
                return;
            }

            MoveTo(_startPos);
        }
    }
}

