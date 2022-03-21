using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PackageContainer : MonoBehaviour
{
    [Header("Visual")]
    [SerializeField] private bool _showText = false;
    [SerializeField] private Vector3 _textOffset = Vector3.up;

    [Header("Container Settings")]
    [SerializeField]private List<Transform> _cells;

    [Range(0, 2f)]
    [SerializeField]private float _packageMoveDuration = 1f;

    private List<Package> _packages = new List<Package>();

    public List<Transform> Cells
    {
        get { return _cells; }
    }
    public bool Equipped => _packages.Count == _cells.Count;
    public bool HasPackages => _packages.Count > 0;
    public int PackagesCount => _packages.Count;

    public  UnityEvent<Package> OnPackageAdded = new UnityEvent<Package>();
    public UnityEvent<Package> OnPackageRemoved  = new UnityEvent<Package>();

    [Inject]
    private WorldCanvas _worldCanvas;

    public void AddCell(Transform cell)
    {
        _cells.Add(cell);
    }
    public Package GetPackage()
    {
        if (HasPackages == false) return null;

        var package = _packages.LastOrDefault();

        _packages.Remove(package);

        OnPackageRemoved?.Invoke(package);

        return package;
    }
    public void AddPackage(Package package)
    {
        if (_packages.Count >= _cells.Count) return;

        _packages.Add(package);

        var cell = Cells[_packages.Count - 1];

        StartCoroutine(package.MoveTo(cell.transform, cell, _packageMoveDuration));

        cell.gameObject.SetActive(true);

        OnPackageAdded?.Invoke(package);

        if(_packages.Count == _cells.Count && _showText)
        {
            StartCoroutine(_worldCanvas.ShowText(transform, _textOffset, "MAX", 1f));
        }
    }
    public bool HasPackage(PackageState state)
    {
        return (_packages.Count > 0 && _packages.ElementAt(0).State == state);
    }


}
