using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PackageContainer : MonoBehaviour
{
    [Header("Container Settings")]
    [SerializeField]
    private List<Transform> _cells;

    [SerializeField] private float _packageMoveSpeed = 1f;

    public List<Transform> Cells
    {
        get { return _cells; }
    }

    public bool Equipped => _packages.Count == _cells.Count;
    public bool HasPackages => _packages.Count > 0;

    public void AddCell(Transform cell)
    {
        _cells.Add(cell);
    }

    public Package GetPackage()
    {
        if (HasPackages == false) return null;

        var package = _packages.LastOrDefault();

        _packages.Remove(package);

        return package;
    }

    public void AddPackage(Package package)
    {
        if (_packages.Count >= _cells.Count) return;

        _packages.Add(package);

        var cell = Cells[_packages.Count - 1];

        StartCoroutine(package.MoveTo(cell.transform, cell, _packageMoveSpeed));

        cell.gameObject.SetActive(true);
    }

    public bool HasPackage(PackageState state)
    {
        return (_packages.Count > 0 && _packages.ElementAt(0).State == state);
    }

    public int PackagesCount => _packages.Count;

    private List<Package> _packages = new List<Package>();
}
