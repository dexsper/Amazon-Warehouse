using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class PackageContainer : MonoBehaviour
{
    [Header("Container Settings")]
    [SerializeField]
    private List<Transform> _cells;

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

    public virtual Package GetPackage()
    {
        if (HasPackages == false) return null;

        var package = _packages.LastOrDefault();

        _packages.Remove(package);

        return package;
    }

    public virtual void AddPackage(Package package)
    {
        if (_packages.Count >= _cells.Count) return;

        _packages.Add(package);
    }

    protected List<Package> _packages = new List<Package>();
}
