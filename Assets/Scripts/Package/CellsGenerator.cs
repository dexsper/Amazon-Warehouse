using UnityEngine;

[RequireComponent(typeof(PackageContainer))]
public class CellsGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    [SerializeField] private Vector2 _offset;
    [SerializeField] private int _horizontalCount = 0;
    [SerializeField] private int _verticalCount = 0;
    [SerializeField] private Transform _cellsParent;

    private PackageContainer _container;
    private void Awake()
    {
        _container = GetComponent<PackageContainer>();

        for (int i = 0; i < _verticalCount; i++)
        {
            for (int j = 0; j < _horizontalCount; j++)
            {
                GameObject go = new GameObject($"Cell {i}.{j}");
                go.transform.parent = _cellsParent;

                Vector3 offset = new Vector3(0f, i * _offset.y, -j * _offset.x);

                go.transform.localPosition = offset;
                go.transform.localEulerAngles = Vector3.zero; 

                _container.AddCell(go.transform);
            }
        }
    }
}
