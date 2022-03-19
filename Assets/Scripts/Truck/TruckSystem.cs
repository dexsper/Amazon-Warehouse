using UnityEngine;
using Zenject;

public class TruckSystem : MonoBehaviour
{
    [SerializeField]
    private Transform[] _truckSpawnPoints;

    [SerializeField]
    private Truck _truckPrefab;

    [Inject]
    private PoolManager _poolManager;

    private void Awake()
    {
        if (_truckSpawnPoints.Length == 0)
            throw new System.Exception("Spawn points array is empty.");

        _poolManager.WarmPool(_truckPrefab.gameObject, 4);
    }
}
