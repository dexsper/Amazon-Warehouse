using UnityEngine;

[RequireComponent(typeof(PackageContainer))]
public class CellsGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private int _horizontalCount = 0;
    [SerializeField] private int _verticalCount = 0;
    [SerializeField] private Transform _cellsParent;

    private PackageContainer _container;
    private void Awake()
    {
        _container = GetComponent<PackageContainer>();

        for (int i = 0; i < _horizontalCount; i++)
        {
            GameObject go = new GameObject($"Cell {i}");
            go.transform.parent = _cellsParent;

            var offset = _offset * i;
            Vector3 pos = Vector3.zero + offset;

            go.transform.localPosition = new Vector3(pos.x, 0f, 0f);

            _container.AddCell(go.transform);
        }

        for (int i = 0; i < _verticalCount; i++)
        {
            GameObject go = new GameObject($"Cell {i}");
            go.transform.parent = _cellsParent;

            var offset = _offset * i;
            Vector3 pos = Vector3.zero + offset;

            go.transform.localPosition = new Vector3(0f, pos.y, 0f);

            _container.AddCell(go.transform);
        }
    }
}
