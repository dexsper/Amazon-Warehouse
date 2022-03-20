using BezierSolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Conveyor : MonoBehaviour
{
    [Header("Work Settings")]
    [SerializeField] private float _delayPerPackage = .7f;

    [Header("Movement Settings")]
    [Range(0f, 10f)]
    [SerializeField] private int _moveTime = 3;


    private ConveyorOutput _output;
    private ConveyorInput _input;
    private BezierSpline _spline;

    private void Awake()
    {
        _output = GetComponentInChildren<ConveyorOutput>();
        _input = GetComponentInChildren<ConveyorInput>(); 
        _spline = GetComponentInChildren<BezierSpline>();
    }

    float timer = 0f;
    private void Update()
    {
        if (_input.Container.HasPackages && _output.Container.Equipped == false)
        {
            timer += Time.deltaTime;

            if (timer >= _delayPerPackage)
            {
                timer = 0f;

                var package = _input.Container.GetPackage();

                var walker = package.gameObject.AddComponent<BezierWalkerWithTime>();
                walker.travelTime = _moveTime;
                walker.spline = _spline;
                walker.onPathCompleted.AddListener(() =>
                {
                    package.SetState(PackageState.Sorted);
                    _output.Container.AddPackage(package);
                    Destroy(walker);
                });
            }
        }
    }



}
