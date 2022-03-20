using UnityEngine;

public class ImportTruck : TruckBase
{
    private void Awake()
    {
        _container = GetComponentInChildren<PackageContainer>();
    }

    private void Start()
    {
        
    }

    public override void AddPackage(Package package)
    {
        base.AddPackage(package);

        var cell = _container.Cells[_packages.Count - 1];
        StartCoroutine(package.MoveTo(cell.transform.position, cell, 0f));

        cell.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_targetDoor != null && _reachDoor == false)
        {
            MoveToDoor();
        }
    }
}

